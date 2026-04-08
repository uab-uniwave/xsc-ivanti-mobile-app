namespace Application.Interfaces.Authentication;

/// <summary>
/// Interface for managing HTTP cookies used in Ivanti authentication.
/// Allows restoring cookies from client storage (localStorage) to the server-side CookieContainer.
/// </summary>
public interface ICookieManager
{
    /// <summary>
    /// Restores cookies from a dictionary (typically from localStorage).
    /// Call this after restoring verification token from localStorage.
    /// </summary>
    /// <param name="cookies">Dictionary of cookie name/value pairs</param>
    void RestoreCookies(IDictionary<string, string>? cookies);

    /// <summary>
    /// Clears all authentication cookies.
    /// Call this before starting a fresh login flow.
    /// </summary>
    void ClearCookies();

    /// <summary>
    /// Gets the current cookie count for debugging.
    /// </summary>
    int CookieCount { get; }
}
