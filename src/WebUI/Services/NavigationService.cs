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

    public void NavigateToRoleSelection()
    {
        _logger.LogInformation("Navigating to Ivanti role selection page");
        _navigationManager.NavigateTo("/role-selection");
    }

    public void NavigateToSelectRole()
    {
        // After role selection, navigate to default workspace
        _logger.LogInformation("Navigating to default workspace page");
        _navigationManager.NavigateTo("/workspace/name/Incident");
    }

    public void NavigateToHome()
    {
        _logger.LogInformation("Navigating to home page");
        _navigationManager.NavigateTo("/");
    }

    public void NavigateToFirstWorkspace()
    {
        _logger.LogInformation("Navigating to first workspace (Incident)");
        _navigationManager.NavigateTo("/workspace/name/Incident");
    }

    public void NavigateToWorkspace(string workspaceName)
    {
        _logger.LogInformation("Navigating to workspace: {WorkspaceName}", workspaceName);
        var path = $"/workspace/name/{workspaceName}";
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
