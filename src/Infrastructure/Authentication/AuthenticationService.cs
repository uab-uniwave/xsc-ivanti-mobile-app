using System.Net.Http;
using System.Text.RegularExpressions;
using Application.Common;
using Application.Interfaces.Authentication;
using Application.Features.Authentication.Models;
using Application.Common.Models.SessonData;
using Application.Features.Authentication.DTOs;
using Application.Services;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Authentication;

/// <summary>
/// Implementation of authentication service using Ivanti's form-based authentication.
/// Manages login, session cookies, and authentication state.
/// </summary>
public class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _httpClient;
    private readonly IIvantiClient _ivantiClient;
    private readonly ILogger<AuthenticationService> _logger;
    private AuthenticationResult? _currentAuthentication;
    private SelectRolePageData? _selectRoleData;
    private VerificationToken? _verificationToken;

    public AuthenticationService(
        HttpClient httpClient,
        IIvantiClient ivantiClient,
        ILogger<AuthenticationService> logger)
    {
        _httpClient = httpClient;
        _ivantiClient = ivantiClient;
        _logger = logger;
    }

    public bool IsAuthenticated => _currentAuthentication?.IsAuthenticated ?? false;

    public AuthenticationResult? CurrentAuthentication => _currentAuthentication;

    public SelectRolePageData? SelectRoleData => _selectRoleData;

    public async Task<Result<VerificationToken>> GetVerificationTokenAsync(CancellationToken ct = default)
    {
        try
        {
            _logger.LogInformation("Fetching verification token from login page...");

            var result = await _ivantiClient.GetVerificationToken(ct);
            if (result.IsSuccess && result.Value != null)
            {
                _verificationToken = result.Value;
                _logger.LogInformation("Successfully retrieved verification token");
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching verification token");
            return Result<VerificationToken>.Failure($"Error: {ex.Message}");
        }
    }

    public async Task<Result<SelectRolePageData>> LoginAsync(
        string username,
        string password,
        string verificationToken,
        CancellationToken ct = default)
    {
        try
        {
            _logger.LogInformation("Attempting login for user: {Username}", username);

            // Build login request with all required form fields
            var loginRequest = new LoginRequest
            {
                VerificationToken = verificationToken,
                Username = username,
                Password = password,
                EnableBiometric = "false",
                Tenant = _verificationToken?.Tenant ?? "",
                IsUrlSharedByTenants = _verificationToken?.IsUrlSharedByTenants.ToString() ?? "False",
                ClientTimeOffset = GetClientTimeOffset().ToString(),
                ClientTimezoneName = TimeZoneInfo.Local.Id,
                ReturnUrl = _verificationToken?.ReturnUrl ?? "",
                PreferredRole = _verificationToken?.PreferredRole ?? "",
                IsForgotPasswordAllowed = _verificationToken?.IsForgotPasswordAllowed.ToString() ?? "False",
                IsFrame = "false",
                OpenIdSignIn = _verificationToken?.OpenIdSignIn ?? "",
                SsoReturnUrl = _verificationToken?.SsoReturnUrl ?? ""
            };

            var result = await _ivantiClient.LoginAsync(loginRequest, ct);

            if (result.IsSuccess && result.Value != null)
            {
                _selectRoleData = result.Value;
                _logger.LogInformation("Login successful. {RoleCount} roles available", 
                    _selectRoleData.AvailableRoles.Count);
            }
            else
            {
                _logger.LogWarning("Login failed: {Error}", result.Error);
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login");
            return Result<SelectRolePageData>.Failure($"Login error: {ex.Message}");
        }
    }

    public async Task<Result<AuthenticationResult>> SelectRoleAsync(
        string roleName,
        string verificationToken,
        CancellationToken ct = default)
    {
        try
        {
            _logger.LogInformation("Selecting role: {RoleId}", roleName);

            // Select the role
            var selectResult = await _ivantiClient.SelectRoleAsync(roleName, verificationToken, ct);
            if (selectResult.IsFailure)
            {
                _logger.LogError("Failed to select role: {Error}", selectResult.Error);
                return Result<AuthenticationResult>.Failure($"Role selection failed: {selectResult.Error}");
            }

            // =====================================================================
            // Execute all required API calls in the correct sequence
            // =====================================================================

            // 1. POST InitializeSession
            _logger.LogInformation("Step 1/3: Initializing session...");
            var sessionResult = await _ivantiClient.InitializeSessionAsync(ct);
            if (sessionResult.IsFailure)
            {
                _logger.LogError("Failed to initialize session after role selection: {Error}", sessionResult.Error);
                return Result<AuthenticationResult>.Failure($"Session initialization failed: {sessionResult.Error}");
            }

            // 2. POST GetUserData
            _logger.LogInformation("Step 2/3: Getting user data...");
            var userDataResult = await _ivantiClient.GetUserDataAsync(ct);
            if (userDataResult.IsFailure)
            {
                _logger.LogError("Failed to get user data: {Error}", userDataResult.Error);
                return Result<AuthenticationResult>.Failure($"Failed to get user data: {userDataResult.Error}");
            }

            // 3. POST GetRoleWorkspaces (minimal data for navigation)
            _logger.LogInformation("Step 3/3: Getting role workspaces...");
            var roleWorkspacesResult = await _ivantiClient.GetRoleWorkspacesAsync(ct);
            if (roleWorkspacesResult.IsFailure)
            {
                _logger.LogError("Failed to get role workspaces: {Error}", roleWorkspacesResult.Error);
                return Result<AuthenticationResult>.Failure($"Failed to get role workspaces: {roleWorkspacesResult.Error}");
            }

            // Note: WorkspaceData, FormViewData, etc. are loaded lazily when user navigates to a workspace

            // =====================================================================
            // All data collected successfully - create authentication result
            // =====================================================================

            _currentAuthentication = new AuthenticationResult
            {
                SessionData = sessionResult.Value!,
                UserData = userDataResult.Value!,
                AuthenticatedAt = DateTime.UtcNow,
                SelectedRole = roleName
            };

            _logger.LogInformation("Authentication complete for user: {DisplayName} with role: {Role}. Workspace data will be lazy-loaded on navigation.", 
                _currentAuthentication.UserData.DisplayName, roleName);

            return Result<AuthenticationResult>.Success(_currentAuthentication);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during role selection");
            return Result<AuthenticationResult>.Failure($"Role selection error: {ex.Message}");
        }
    }

    public Task<Result> LogoutAsync(CancellationToken ct = default)
    {
        try
        {
            _logger.LogInformation("User logged out");
            _currentAuthentication = null;
            _selectRoleData = null;
            _verificationToken = null;

            // Clear cookies would happen here in a real implementation
            // For now, just clear local state

            return Task.FromResult(Result.Success());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during logout");
            return Task.FromResult(Result.Failure($"Logout error: {ex.Message}"));
        }
    }

    /// <summary>
    /// Gets the client timezone offset in minutes (negative for ahead of UTC).
    /// </summary>
    private static int GetClientTimeOffset()
    {
        var offset = TimeZoneInfo.Local.GetUtcOffset(DateTime.Now);
        return -(int)offset.TotalMinutes;
    }
}
