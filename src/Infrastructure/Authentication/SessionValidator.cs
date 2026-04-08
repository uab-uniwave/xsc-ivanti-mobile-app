using System.Net;
using Application.Common;
using Application.Interfaces.Authentication;
using Application.Features.Authentication.Models;
using Application.Services;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Authentication;

/// <summary>
/// Validates existing authentication sessions by attempting authenticated requests.
/// Used to check if stored cookies are still valid before requiring re-login.
/// </summary>
public class SessionValidator : ISessionValidator
{
    private readonly IIvantiClient _ivantiClient;
    private readonly IAuthenticationService _authenticationService;
    private readonly ILogger<SessionValidator> _logger;

    // Reference to the shared cookie container for debugging
    private static CookieContainer? _cookieContainerRef;

    public static void SetCookieContainerRef(CookieContainer container)
    {
        _cookieContainerRef = container;
    }

    public SessionValidator(
        IIvantiClient ivantiClient,
        IAuthenticationService authenticationService,
        ILogger<SessionValidator> logger)
    {
        _ivantiClient = ivantiClient;
        _authenticationService = authenticationService;
        _logger = logger;
    }

    /// <summary>
    /// Validates if the current session/cookies are still valid by attempting
    /// to initialize a session with the server.
    /// </summary>
    public async Task<Result<bool>> ValidateSessionAsync(CancellationToken ct = default)
    {
        try
        {
            _logger.LogInformation("Validating existing session...");
            LogCookieContainerState();

            // Try to initialize session - this requires valid auth cookies
            var sessionResult = await _ivantiClient.InitializeSessionAsync(ct);

            if (sessionResult.IsSuccess && sessionResult.Value != null)
            {
                _logger.LogInformation("Session is valid");
                return Result<bool>.Success(true);
            }

            _logger.LogInformation("Session validation failed: {Error}", sessionResult.Error);
            return Result<bool>.Success(false);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Session validation threw exception - session likely expired");
            return Result<bool>.Success(false);
        }
    }

    /// <summary>
    /// Attempts to restore a full session using existing cookies.
    /// Calls InitializeSession and GetUserData to rebuild AuthenticationResult.
    /// </summary>
    public async Task<Result<AuthenticationResult>> TryRestoreSessionAsync(CancellationToken ct = default)
    {
        try
        {
            _logger.LogInformation("Attempting to restore session from existing cookies...");
            LogCookieContainerState();

            // Try to initialize session with existing cookies
            var sessionResult = await _ivantiClient.InitializeSessionAsync(ct);
            if (sessionResult.IsFailure || sessionResult.Value == null)
            {
                _logger.LogInformation("Failed to restore session: {Error}", sessionResult.Error);
                return Result<AuthenticationResult>.Failure("Session expired or invalid");
            }

            _logger.LogInformation("Session initialized successfully, getting user data...");

            // Session is valid, get user data
            var userDataResult = await _ivantiClient.GetUserDataAsync(ct);
            if (userDataResult.IsFailure || userDataResult.Value == null)
            {
                _logger.LogWarning("Session valid but failed to get user data: {Error}", userDataResult.Error);
                return Result<AuthenticationResult>.Failure("Failed to get user data");
            }

            // Build authentication result
            var authResult = new AuthenticationResult
            {
                SessionData = sessionResult.Value,
                UserData = userDataResult.Value,
                AuthenticatedAt = DateTime.UtcNow,
                SelectedRole = userDataResult.Value.UserRole
            };

            _logger.LogInformation("Session restored successfully for user: {DisplayName}", 
                authResult.UserData.DisplayName);

            return Result<AuthenticationResult>.Success(authResult);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Exception while restoring session");
            return Result<AuthenticationResult>.Failure($"Session restore failed: {ex.Message}");
        }
    }

    /// <summary>
    /// Logs the current state of the cookie container for debugging.
    /// </summary>
    private void LogCookieContainerState()
    {
        if (_cookieContainerRef == null)
        {
            _logger.LogWarning("Cookie container reference not set");
            return;
        }

        var cookieCount = _cookieContainerRef.Count;
        _logger.LogInformation("CookieContainer has {Count} cookies", cookieCount);

        // Try to get cookies for common Ivanti paths
        try
        {
            var baseUri = new Uri("https://stg-heat20254.synergy.lt");
            var cookies = _cookieContainerRef.GetCookies(baseUri);
            foreach (Cookie cookie in cookies)
            {
                _logger.LogInformation("Cookie: {Name}={Value} (Expires: {Expires})", 
                    cookie.Name, 
                    cookie.Value.Length > 20 ? cookie.Value[..20] + "..." : cookie.Value,
                    cookie.Expires);
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to enumerate cookies");
        }
    }
}
