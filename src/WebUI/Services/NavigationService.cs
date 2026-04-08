using Application.Interfaces.Navigation;
using Microsoft.AspNetCore.Components;

namespace WebUI.Services;

/// <summary>
/// Implementation of navigation service for Blazor application.
/// Manages page navigation and routing using NavigationManager.
/// </summary>
public class NavigationService : INavigationService
{
    private readonly NavigationManager _navigationManager;
    private readonly ILogger<NavigationService> _logger;

    public NavigationService(
        NavigationManager navigationManager,
        ILogger<NavigationService> logger)
    {
        _navigationManager = navigationManager;
        _logger = logger;
    }

    public void NavigateToLogin()
    {
        _logger.LogInformation("Navigating to login page");
        _navigationManager.NavigateTo("/login");
    }

    public void NavigateToSelectRole()
    {
        _logger.LogInformation("Navigating to role selection page");
        _navigationManager.NavigateTo("/select-role");
    }

    public void NavigateToFirstWorkspace()
    {
        _logger.LogInformation("Navigating to first workspace (Incidents)");
        _navigationManager.NavigateTo("/incidents");
    }

    public void NavigateToWorkspace(string workspaceName)
    {
        _logger.LogInformation("Navigating to workspace: {WorkspaceName}", workspaceName);
        var path = $"/{workspaceName.ToLowerInvariant()}";
        _navigationManager.NavigateTo(path);
    }

    public void NavigateTo(string path, bool forceLoad = false)
    {
        _logger.LogInformation("Navigating to: {Path} (ForceLoad: {ForceLoad})", path, forceLoad);
        _navigationManager.NavigateTo(path, forceLoad);
    }

    public void GoBack()
    {
        _logger.LogInformation("Navigating back");
        // Blazor doesn't have a built-in back navigation
        // This would require JavaScript interop or maintaining navigation history
        // For now, navigate to home
        _navigationManager.NavigateTo("/");
    }

    public string GetCurrentUri()
    {
        return _navigationManager.Uri;
    }
}
