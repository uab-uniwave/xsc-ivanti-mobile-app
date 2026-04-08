namespace Application.Interfaces.Storage;

/// <summary>
/// Interface for browser storage operations.
/// Used to persist authentication tokens and session data.
/// </summary>
public interface IClientStorageService
{
    /// <summary>
    /// Gets a value from client storage.
    /// </summary>
    Task<string?> GetItemAsync(string key);

    /// <summary>
    /// Sets a value in client storage.
    /// </summary>
    Task SetItemAsync(string key, string value);

    /// <summary>
    /// Removes a value from client storage.
    /// </summary>
    Task RemoveItemAsync(string key);

    /// <summary>
    /// Clears all authentication-related items from storage.
    /// </summary>
    Task ClearAuthDataAsync();

    /// <summary>
    /// Checks if JavaScript interop is available (not during prerendering).
    /// </summary>
    Task<bool> IsAvailableAsync();
}
