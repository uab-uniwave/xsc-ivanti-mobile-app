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

    public async Task<Result<VerificationToken>> GetVerificationTokenAsync(CancellationToken ct = default)
    {
        try
        {
            _logger.LogInformation("Fetching verification token from login page...");

            // Get the login page HTML
            var response = await _httpClient.GetAsync("/", ct);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Failed to fetch login page: {StatusCode}", response.StatusCode);
                return Result<VerificationToken>.Failure($"Failed to fetch login page: {response.StatusCode}");
            }

            var html = await response.Content.ReadAsStringAsync(ct);

            // Extract verification token from HTML
            var tokenMatch = Regex.Match(html, @"<input[^>]*name=""__RequestVerificationToken""[^>]*value=""([^""]+)""");
            if (!tokenMatch.Success)
            {
                _logger.LogError("Verification token not found in login page HTML");
                return Result<VerificationToken>.Failure("Verification token not found in login page");
            }

            var token = tokenMatch.Groups[1].Value;

            // Extract tenant from hidden input
            var tenantMatch = Regex.Match(html, @"<input[^>]*id=""Tenant""[^>]*value=""([^""]+)""");
            var tenant = tenantMatch.Success ? tenantMatch.Groups[1].Value : null;

            // Extract cookies from response
            var cookies = new Dictionary<string, string>();
            if (response.Headers.TryGetValues("Set-Cookie", out var cookieValues))
            {
                foreach (var cookie in cookieValues)
                {
                    var parts = cookie.Split(';')[0].Split('=', 2);
                    if (parts.Length == 2)
                    {
                        cookies[parts[0].Trim()] = parts[1].Trim();
                    }
                }
            }

            var verificationToken = new VerificationToken
            {
                Token = token,
                Tenant = tenant,
                Cookies = cookies
            };

            _logger.LogInformation("Successfully retrieved verification token");
            return Result<VerificationToken>.Success(verificationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching verification token");
            return Result<VerificationToken>.Failure($"Error: {ex.Message}");
        }
    }

    public async Task<Result<AuthenticationResult>> LoginAsync(
        string username,
        string password,
        string verificationToken,
        CancellationToken ct = default)
    {
        try
        {
            _logger.LogInformation("Attempting login for user: {Username}", username);

            // Create form content for login
            var formData = new Dictionary<string, string>
            {
                ["UserName"] = username,
                ["Password"] = password,
                ["__RequestVerificationToken"] = verificationToken,
                ["ClientTimeOffset"] = "0",
                ["ClientTimezoneName"] = TimeZoneInfo.Local.Id
            };

            var content = new FormUrlEncodedContent(formData);

            // Post login form
            var response = await _httpClient.PostAsync("/", content, ct);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Login failed with status: {StatusCode}", response.StatusCode);
                return Result<AuthenticationResult>.Failure($"Login failed: {response.StatusCode}");
            }

            // Check if login was successful by looking for redirect or specific content
            var responseContent = await response.Content.ReadAsStringAsync(ct);

            // If we get the login page back, credentials were invalid
            if (responseContent.Contains("Login") && responseContent.Contains("Password"))
            {
                _logger.LogWarning("Login failed: Invalid credentials");
                return Result<AuthenticationResult>.Failure("Invalid username or password");
            }

            // Now initialize the Ivanti session using the authenticated cookies
            var sessionResult = await _ivantiClient.InitializeSessionAsync(ct);
            if (sessionResult.IsFailure)
            {
                _logger.LogError("Failed to initialize session after login: {Error}", sessionResult.Error);
                return Result<AuthenticationResult>.Failure($"Session initialization failed: {sessionResult.Error}");
            }

            // Get user data
            var userDataResult = await _ivantiClient.GetUserDataAsync(ct);
            if (userDataResult.IsFailure)
            {
                _logger.LogError("Failed to get user data: {Error}", userDataResult.Error);
                return Result<AuthenticationResult>.Failure($"Failed to get user data: {userDataResult.Error}");
            }

            // Create authentication result
            _currentAuthentication = new AuthenticationResult
            {
                SessionData = sessionResult.Value!,
                UserData = userDataResult.Value!,
                AuthenticatedAt = DateTime.UtcNow
            };

            _logger.LogInformation("Login successful for user: {Username} ({DisplayName})", 
                username, _currentAuthentication.UserData.DisplayName);

            return Result<AuthenticationResult>.Success(_currentAuthentication);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login");
            return Result<AuthenticationResult>.Failure($"Login error: {ex.Message}");
        }
    }

    public Task<Result> LogoutAsync(CancellationToken ct = default)
    {
        try
        {
            _logger.LogInformation("User logged out");
            _currentAuthentication = null;

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
}
