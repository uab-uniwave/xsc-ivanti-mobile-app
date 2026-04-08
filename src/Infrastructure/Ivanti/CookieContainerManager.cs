using System.Net;

namespace Infrastructure.Ivanti;

/// <summary>
/// Provides access to the shared cookie container for management operations.
/// Used to clear cookies when starting a fresh login flow and restore cookies from localStorage.
/// </summary>
public static class CookieContainerManager
{
    private static CookieContainer? _sharedContainer;
    private static Uri? _baseUri;

    /// <summary>
    /// Initializes the manager with the shared cookie container.
    /// </summary>
    public static void Initialize(CookieContainer container, string baseUrl)
    {
        _sharedContainer = container;
        _baseUri = new Uri(baseUrl);
    }

    /// <summary>
    /// Gets the count of cookies in the container.
    /// </summary>
    public static int CookieCount => _sharedContainer?.Count ?? 0;

    /// <summary>
    /// Adds a cookie to the shared container.
    /// Used to restore cookies from localStorage.
    /// </summary>
    /// <param name="name">Cookie name (e.g., "AFT")</param>
    /// <param name="value">Cookie value</param>
    public static void AddCookie(string name, string value)
    {
        if (_sharedContainer == null || _baseUri == null || string.IsNullOrEmpty(value))
            return;

        try
        {
            var cookie = new Cookie(name, value, "/", _baseUri.Host)
            {
                Secure = true,
                HttpOnly = true
            };
            _sharedContainer.Add(_baseUri, cookie);
        }
        catch
        {
            // Ignore errors during cookie addition
        }
    }

    /// <summary>
    /// Restores cookies from a dictionary (typically from localStorage).
    /// </summary>
    /// <param name="cookies">Dictionary of cookie name/value pairs</param>
    public static void RestoreCookies(IDictionary<string, string>? cookies)
    {
        if (cookies == null) return;

        foreach (var kvp in cookies)
        {
            AddCookie(kvp.Key, kvp.Value);
        }
    }

    /// <summary>
    /// Clears all cookies for the Ivanti base URL.
    /// Call this before starting a fresh login flow to avoid token/cookie mismatch.
    /// </summary>
    public static void ClearCookies()
    {
        if (_sharedContainer == null || _baseUri == null) return;

        try
        {
            // Get all cookies for the base URI
            var cookies = _sharedContainer.GetCookies(_baseUri);
            foreach (Cookie cookie in cookies)
            {
                // Mark cookies as expired to remove them
                cookie.Expired = true;
            }
        }
        catch
        {
            // Ignore errors during cleanup
        }
    }

    /// <summary>
    /// Gets cookie information for debugging.
    /// </summary>
    public static IEnumerable<string> GetCookieInfo()
    {
        if (_sharedContainer == null || _baseUri == null)
        {
            yield return "Cookie container not initialized";
            yield break;
        }

        var cookies = _sharedContainer.GetCookies(_baseUri);
        if (cookies.Count == 0)
        {
            yield return "No cookies in container";
            yield break;
        }

        foreach (Cookie cookie in cookies)
        {
            yield return $"{cookie.Name}={cookie.Value[..Math.Min(20, cookie.Value.Length)]}... (Expired: {cookie.Expired})";
        }
    }
}
