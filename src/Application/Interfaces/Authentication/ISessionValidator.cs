using Application.Common;
using Application.Features.Authentication.Models;

namespace Application.Interfaces.Authentication;

/// <summary>
/// Interface for validating existing authentication sessions.
/// Used to check if stored cookies are still valid before requiring re-login.
/// </summary>
public interface ISessionValidator
{
    /// <summary>
    /// Validates if the current session/cookies are still valid by attempting
    /// an authenticated request (like GetRoleWorkspaces or InitializeSession).
    /// </summary>
    /// <returns>True if session is valid, false if expired or invalid.</returns>
    Task<Result<bool>> ValidateSessionAsync(CancellationToken ct = default);

    /// <summary>
    /// Attempts to restore a full session using existing cookies.
    /// If successful, returns the AuthenticationResult with user data.
    /// </summary>
    /// <returns>AuthenticationResult if session is valid and restored, failure otherwise.</returns>
    Task<Result<AuthenticationResult>> TryRestoreSessionAsync(CancellationToken ct = default);
}
