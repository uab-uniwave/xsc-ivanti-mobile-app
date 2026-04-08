using Application.Interfaces.Storage;
using Microsoft.JSInterop;

namespace WebUI.Services;

/// <summary>
/// Service for interacting with browser localStorage.
/// Used to persist authentication tokens and cookies across page loads.
/// Mirrors Ivanti's original JavaScript client behavior.
/// </summary>
public class LocalStorageService : IClientStorageService
{
    private readonly IJSRuntime _jsRuntime;

    // Storage keys matching Ivanti's original naming
    private const string AftCookieKey = "ivanti_aft_cookie";
    private const string VerificationTokenKey = "ivanti_verification_token";
    private const string TenantKey = "ivanti_tenant";
    private const string VerificationDataKey = "ivanti_verification_data";

    public LocalStorageService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    /// <summary>
    /// Checks if JavaScript interop is available (not during prerendering).
    /// </summary>
    public async Task<bool> IsAvailableAsync()
    {
        try
        {
            await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", "_test");
            return true;
        }
        catch (InvalidOperationException)
        {
            return false;
        }
    }

    /// <summary>
    /// Gets a value from localStorage.
    /// </summary>
    public async Task<string?> GetItemAsync(string key)
    {
        try
        {
            return await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", key);
        }
        catch (InvalidOperationException)
        {
            // JS interop not available during prerendering
            return null;
        }
    }

    /// <summary>
    /// Sets a value in localStorage.
    /// </summary>
    public async Task SetItemAsync(string key, string value)
    {
        try
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, value);
        }
        catch (InvalidOperationException)
        {
            // JS interop not available during prerendering
        }
    }

    /// <summary>
    /// Removes a value from localStorage.
    /// </summary>
    public async Task RemoveItemAsync(string key)
    {
        try
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
        }
        catch (InvalidOperationException)
        {
            // JS interop not available during prerendering
        }
    }

    /// <summary>
    /// Clears all authentication-related items from localStorage.
    /// </summary>
    public async Task ClearAuthDataAsync()
    {
        await RemoveItemAsync(AftCookieKey);
        await RemoveItemAsync(VerificationTokenKey);
        await RemoveItemAsync(TenantKey);
        await RemoveItemAsync(VerificationDataKey);
    }
}
