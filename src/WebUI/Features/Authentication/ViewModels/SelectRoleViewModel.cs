using Application.Interfaces.Navigation;
using Application.Features.Workspaces.Models.RoleWorkspaces;
using Application.Services;
using RoleWorkspace = Application.Features.Workspaces.Models.RoleWorkspaces.Workspace;

namespace WebUI.Features.Authentication.ViewModels;

/// <summary>
/// ViewModel for the SelectRole page.
/// Manages workspace selection after authentication.
/// </summary>
public class SelectRoleViewModel
{
    private readonly IIvantiClient _ivantiClient;
    private readonly INavigationService _navigationService;
    private readonly ILogger<SelectRoleViewModel> _logger;

    public SelectRoleViewModel(
        IIvantiClient ivantiClient,
        INavigationService navigationService,
        ILogger<SelectRoleViewModel> logger)
    {
        _ivantiClient = ivantiClient;
        _navigationService = navigationService;
        _logger = logger;
    }

    public List<RoleWorkspace> Workspaces { get; private set; } = new();
    public bool IsLoading { get; private set; }
    public bool HasError { get; private set; }
    public string ErrorMessage { get; private set; } = string.Empty;

    /// <summary>
    /// Initializes the page by calling the required API sequence and loading ALL workspaces basic data.
    /// </summary>
    public async Task InitializeAsync()
    {
        IsLoading = true;
        HasError = false;
        ErrorMessage = string.Empty;

        try
        {
            _logger.LogInformation("Initializing session and loading workspaces...");

            // 1. POST InitializeSession
            _logger.LogInformation("Step 1/4: Initializing session...");
            var sessionResult = await _ivantiClient.InitializeSessionAsync(CancellationToken.None);
            if (sessionResult.IsFailure)
            {
                _logger.LogError("Failed to initialize session: {Error}", sessionResult.Error);
                HasError = true;
                ErrorMessage = $"Failed to initialize session: {sessionResult.Error}";
                return;
            }

            // 2. POST GetUserData
            _logger.LogInformation("Step 2/4: Getting user data...");
            var userDataResult = await _ivantiClient.GetUserDataAsync(CancellationToken.None);
            if (userDataResult.IsFailure)
            {
                _logger.LogError("Failed to get user data: {Error}", userDataResult.Error);
                HasError = true;
                ErrorMessage = $"Failed to get user data: {userDataResult.Error}";
                return;
            }

            // 3. POST GetRoleWorkspaces
            _logger.LogInformation("Step 3/4: Getting role workspaces...");
            var roleWorkspacesResult = await _ivantiClient.GetRoleWorkspacesAsync(CancellationToken.None);
            if (roleWorkspacesResult.IsFailure)
            {
                _logger.LogError("Failed to get role workspaces: {Error}", roleWorkspacesResult.Error);
                HasError = true;
                ErrorMessage = $"Failed to get role workspaces: {roleWorkspacesResult.Error}";
                return;
            }

            Workspaces = roleWorkspacesResult.Value?.Workspaces ?? new();
            _logger.LogInformation("Loaded {Count} workspaces", Workspaces.Count);

            // 4. Load WorkspaceData for ALL workspaces
            _logger.LogInformation("Step 4/4: Loading WorkspaceData for all {Count} workspaces...", Workspaces.Count);
            var allWorkspacesResult = await _ivantiClient.LoadAllWorkspacesBasicDataAsync(CancellationToken.None);
            if (allWorkspacesResult.IsFailure)
            {
                _logger.LogWarning("Failed to load all workspaces data: {Error}", allWorkspacesResult.Error);
                // Non-fatal - continue with workspace selection
            }
            else
            {
                var loadedCount = allWorkspacesResult.Value?.Count(w => w.WorkspaceData != null) ?? 0;
                _logger.LogInformation("Successfully loaded WorkspaceData for {Loaded}/{Total} workspaces", 
                    loadedCount, Workspaces.Count);
            }

            // If only one workspace, select it automatically
            if (Workspaces.Count == 1)
            {
                _logger.LogInformation("Only one workspace available, selecting automatically");
                await SelectWorkspaceAsync(Workspaces[0]);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error initializing session and loading workspaces");
            HasError = true;
            ErrorMessage = $"An error occurred: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }

    /// <summary>
    /// Handles workspace selection - loads form data for the selected workspace and navigates.
    /// </summary>
    public async Task SelectWorkspaceAsync(RoleWorkspace workspace)
    {
        if (workspace == null)
        {
            _logger.LogWarning("Attempted to select null workspace");
            return;
        }

        IsLoading = true;
        HasError = false;
        ErrorMessage = string.Empty;

        try
        {
            _logger.LogInformation("Workspace selected: {WorkspaceName} (ID: {WorkspaceId}). Loading form data...", 
                workspace.Name, workspace.Id);

            // Load form data for this specific workspace
            // (FormViewData, FormDefaultData, FormValidationListData, ValidatedSearches)
            var loadResult = await _ivantiClient.LoadWorkspaceFormDataAsync(workspace.Id, CancellationToken.None);

            if (loadResult.IsFailure)
            {
                _logger.LogWarning("Failed to load form data for workspace: {Error}", loadResult.Error);
                // Non-fatal - continue with navigation
            }
            else
            {
                _logger.LogInformation("Successfully loaded form data for workspace: {WorkspaceName}. IsFullyLoaded: {IsFullyLoaded}",
                    workspace.Name, loadResult.Value?.IsFullyLoaded ?? false);
            }

            // Navigate to the selected workspace
            _logger.LogInformation("Navigating to workspace: {WorkspaceName}", workspace.Name);

            if (workspace.Name?.Equals("Incident", StringComparison.OrdinalIgnoreCase) == true)
            {
                _navigationService.NavigateToFirstWorkspace();
            }
            else
            {
                _navigationService.NavigateToWorkspace(workspace.Name ?? "incidents");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error selecting workspace");
            HasError = true;
            ErrorMessage = $"An error occurred: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
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
