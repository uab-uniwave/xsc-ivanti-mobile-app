using Application.Interfaces.Navigation;
using Application.Interfaces.Workspaces;
using Application.Features.Workspaces.Models.RoleWorkspaces;

namespace WebUI.Features.Authentication.ViewModels;

/// <summary>
/// ViewModel for the SelectRole page.
/// Manages role/workspace selection after authentication.
/// </summary>
public class SelectRoleViewModel
{
    private readonly IWorkspaceService _workspaceService;
    private readonly INavigationService _navigationService;
    private readonly ILogger<SelectRoleViewModel> _logger;

    public SelectRoleViewModel(
        IWorkspaceService workspaceService,
        INavigationService navigationService,
        ILogger<SelectRoleViewModel> logger)
    {
        _workspaceService = workspaceService;
        _navigationService = navigationService;
        _logger = logger;
    }

    public List<Workspace> Workspaces { get; private set; } = new();
    public bool IsLoading { get; private set; }
    public bool HasError { get; private set; }
    public string ErrorMessage { get; private set; } = string.Empty;

    /// <summary>
    /// Initializes the role selection page by loading available workspaces.
    /// </summary>
    public async Task InitializeAsync()
    {
        IsLoading = true;
        HasError = false;
        ErrorMessage = string.Empty;

        try
        {
            _logger.LogInformation("Loading workspaces for role selection...");

            var result = await _workspaceService.GetRoleWorkspacesAsync();

            if (result.IsSuccess && result.Value != null)
            {
                Workspaces = result.Value.Workspaces;
                _logger.LogInformation("Loaded {Count} workspaces", Workspaces.Count);

                // If only one workspace, navigate directly
                if (Workspaces.Count == 1)
                {
                    _logger.LogInformation("Only one workspace available, navigating directly");
                    await SelectWorkspaceAsync(Workspaces[0]);
                }
            }
            else
            {
                _logger.LogError("Failed to load workspaces: {Error}", result.Error);
                HasError = true;
                ErrorMessage = result.Error ?? "Failed to load workspaces.";
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading workspaces");
            HasError = true;
            ErrorMessage = "An error occurred while loading workspaces.";
        }
        finally
        {
            IsLoading = false;
        }
    }

    /// <summary>
    /// Handles workspace selection and navigates to the selected workspace.
    /// </summary>
    public async Task SelectWorkspaceAsync(Workspace workspace)
    {
        if (workspace == null)
        {
            _logger.LogWarning("Attempted to select null workspace");
            return;
        }

        _logger.LogInformation("Workspace selected: {WorkspaceName}", workspace.Name);

        // Navigate to the selected workspace
        // Default to Incidents if workspace name matches
        if (workspace.Name?.Equals("Incident", StringComparison.OrdinalIgnoreCase) == true)
        {
            _navigationService.NavigateToFirstWorkspace();
        }
        else
        {
            _navigationService.NavigateToWorkspace(workspace.Name ?? "incidents");
        }

        await Task.CompletedTask;
    }

    /// <summary>
    /// Handles logout and returns to login page.
    /// </summary>
    public void Logout()
    {
        _logger.LogInformation("User initiated logout from role selection");
        _navigationService.NavigateToLogin();
    }
}
