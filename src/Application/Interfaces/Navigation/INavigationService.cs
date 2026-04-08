namespace Application.Interfaces.Navigation;

/// <summary>
/// Service for managing application navigation and routing.
/// Provides centralized navigation logic and history management.
/// </summary>
public interface INavigationService
{
    /// <summary>
    /// Navigates to the login page.
    /// </summary>
    void NavigateToLogin();

    /// <summary>
    /// Navigates to the role selection page.
    /// </summary>
    void NavigateToSelectRole();

    /// <summary>
    /// Navigates to the first available workspace (typically Incidents).
    /// </summary>
    void NavigateToFirstWorkspace();

    /// <summary>
    /// Navigates to a specific workspace by name.
    /// </summary>
    /// <param name="workspaceName">The name of the workspace to navigate to</param>
    void NavigateToWorkspace(string workspaceName);

    /// <summary>
    /// Navigates to the specified path.
    /// </summary>
    /// <param name="path">The path to navigate to</param>
    /// <param name="forceLoad">Whether to force a full page reload</param>
    void NavigateTo(string path, bool forceLoad = false);

    /// <summary>
    /// Goes back to the previous page in navigation history.
    /// </summary>
    void GoBack();

    /// <summary>
    /// Gets the current relative URI.
    /// </summary>
    string GetCurrentUri();
}
