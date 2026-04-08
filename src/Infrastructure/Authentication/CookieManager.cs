using Application.Interfaces.Authentication;
using Infrastructure.Ivanti;

namespace Infrastructure.Authentication;

/// <summary>
/// Implementation of ICookieManager that delegates to CookieContainerManager.
/// Provides a DI-friendly wrapper around the static cookie management.
/// </summary>
public class CookieManager : ICookieManager
{
    /// <inheritdoc />
    public void RestoreCookies(IDictionary<string, string>? cookies)
    {
        CookieContainerManager.RestoreCookies(cookies);
    }

    /// <inheritdoc />
    public void ClearCookies()
    {
        CookieContainerManager.ClearCookies();
    }

    /// <inheritdoc />
    public int CookieCount => CookieContainerManager.CookieCount;
}
