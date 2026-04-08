using Application.Common;
using Application.Features.Authentication.Models;

namespace Application.Interfaces.Authentication;

/// <summary>
/// Authentication service interface for managing user authentication and session state.
/// Handles login, verification tokens, and session management.
/// </summary>
public interface IAuthenticationService
{
    /// <summary>
    /// Gets the verification token required for form-based authentication.
    /// This token is extracted from the login page HTML.
    /// </summary>
    Task<Result<VerificationToken>> GetVerificationTokenAsync(CancellationToken ct = default);

    /// <summary>
    /// Authenticates the user with username and password.
    /// Returns SelectRolePageData with available roles for selection.
    /// </summary>
    /// <param name="username">User's login name</param>
    /// <param name="password">User's password</param>
    /// <param name="verificationToken">Anti-forgery token from login page</param>
    /// <param name="ct">Cancellation token</param>
    Task<Result<SelectRolePageData>> LoginAsync(
        string username,
        string password,
        string verificationToken,
        CancellationToken ct = default);

    /// <summary>
    /// Selects a role for the authenticated user.
    /// Completes the authentication flow after role selection.
    /// </summary>
    /// <param name="roleId">The role ID to select</param>
    /// <param name="verificationToken">Anti-forgery token from SelectRole page</param>
    /// <param name="ct">Cancellation token</param>
    Task<Result<AuthenticationResult>> SelectRoleAsync(
        string roleId,
        string verificationToken,
        CancellationToken ct = default);

    /// <summary>
    /// Signs out the current user and clears session cookies.
    /// </summary>
    Task<Result> LogoutAsync(CancellationToken ct = default);

    /// <summary>
    /// Checks if the user is currently authenticated.
    /// </summary>
    bool IsAuthenticated { get; }

    /// <summary>
    /// Gets the current authentication result with session and user data.
    /// </summary>
    AuthenticationResult? CurrentAuthentication { get; }

    /// <summary>
    /// Gets the SelectRolePageData from the last login attempt.
    /// </summary>
    SelectRolePageData? SelectRoleData { get; }
}
