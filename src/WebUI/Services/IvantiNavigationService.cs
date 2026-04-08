using Application.Common.Models.SessonData;
using Application.Common.Models.UserData;
using Application.Features.Authentication.Models;
using Application.Features.Workspaces.Models;
using Application.Features.Workspaces.Models.RoleWorkspaces;
using Application.Interfaces.State;
using Application.Services;
using MudBlazor;

namespace WebUI.Services;

/// <summary>
/// Manages Ivanti session state and navigation data for the application.
/// This service provides navigation menu items based on loaded workspaces.
/// </summary>
public class IvantiNavigationService
{
    private readonly IIvantiStateService _stateService;
    private readonly IIvantiClient _ivantiClient;
    private readonly ILogger<IvantiNavigationService> _logger;

    public IvantiNavigationService(
        IIvantiStateService stateService,
        IIvantiClient ivantiClient,
        ILogger<IvantiNavigationService> logger)
    {
        _stateService = stateService;
        _ivantiClient = ivantiClient;
        _logger = logger;
    }

    /// <summary>
    /// Gets session data from state service.
    /// </summary>
    public SessionData? SessionData => _stateService.SessionData;

    /// <summary>
    /// Gets user data from state service.
    /// </summary>
    public UserData? UserData => _stateService.UserData;

    /// <summary>
    /// Gets role workspaces from state service.
    /// </summary>
    public RoleWorkspaces? RoleWorkspaces => _stateService.RoleWorkspaces;

    /// <summary>
    /// Gets all workspaces with their data.
    /// </summary>
    public List<WorkspaceFullData> AllWorkspaces => _stateService.AllWorkspacesData;

    /// <summary>
    /// Gets the current/active workspace.
    /// </summary>
    public WorkspaceFullData? CurrentWorkspace => _stateService.CurrentWorkspace;

    /// <summary>
    /// Gets visible workspaces for navigation menu (only those with Visible=true).
    /// </summary>
    public IEnumerable<WorkspaceFullData> VisibleWorkspaces => 
        _stateService.AllWorkspacesData
            .Where(w => w.Workspace.Visible && w.Workspace.VisibleInMainMenu)
            .OrderBy(w => w.Workspace.Default ? 0 : 1); // Default workspace first

    /// <summary>
    /// Whether the session is initialized.
    /// </summary>
    public bool IsInitialized => _stateService.IsSessionInitialized && _stateService.IsUserDataLoaded;

    public bool IsLoading { get; private set; }
    public string? ErrorMessage { get; private set; }

    public event Action? OnStateChanged;

    /// <summary>
    /// Initializes the Ivanti session by calling the required APIs in sequence.
    /// </summary>
    public async Task<bool> InitializeAsync(CancellationToken cancellationToken = default)
    {
        if (IsInitialized)
        {
            _logger.LogInformation("Ivanti navigation already initialized");
            return true;
        }

        IsLoading = true;
        ErrorMessage = null;
        NotifyStateChanged();

        try
        {
            _logger.LogInformation("Starting Ivanti session initialization...");

            // Step 1: Initialize Session
            var sessionResult = await _ivantiClient.InitializeSessionAsync(cancellationToken);
            if (sessionResult.IsFailure)
            {
                ErrorMessage = $"Failed to initialize session: {sessionResult.Error}";
                _logger.LogError(ErrorMessage);
                return false;
            }
            _logger.LogInformation("Session initialized successfully");

            // Step 2: Get User Data
            var userDataResult = await _ivantiClient.GetUserDataAsync(cancellationToken);
            if (userDataResult.IsFailure)
            {
                ErrorMessage = $"Failed to get user data: {userDataResult.Error}";
                _logger.LogError(ErrorMessage);
                return false;
            }
            _logger.LogInformation("User data retrieved: {DisplayName}", UserData?.DisplayName);

            // Step 3: Get Role Workspaces
            var workspacesResult = await _ivantiClient.GetRoleWorkspacesAsync(cancellationToken);
            if (workspacesResult.IsFailure)
            {
                ErrorMessage = $"Failed to get workspaces: {workspacesResult.Error}";
                _logger.LogError(ErrorMessage);
                return false;
            }
            _logger.LogInformation("Loaded {Count} workspaces", RoleWorkspaces?.Workspaces?.Count ?? 0);

            // Step 4: Load WorkspaceData for ALL workspaces
            var allWorkspacesResult = await _ivantiClient.LoadAllWorkspacesBasicDataAsync(cancellationToken);
            if (allWorkspacesResult.IsFailure)
            {
                _logger.LogWarning("Failed to load all workspaces data: {Error}", allWorkspacesResult.Error);
                // Non-fatal - continue
            }
            else
            {
                var loadedCount = allWorkspacesResult.Value?.Count(w => w.WorkspaceData != null) ?? 0;
                _logger.LogInformation("Loaded WorkspaceData for {Loaded}/{Total} workspaces", 
                    loadedCount, RoleWorkspaces?.Workspaces?.Count ?? 0);
            }

            ErrorMessage = null;
            return true;
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Initialization error: {ex.Message}";
            _logger.LogError(ex, "Error during Ivanti navigation initialization");
            return false;
        }
        finally
        {
            IsLoading = false;
            NotifyStateChanged();
        }
    }

    /// <summary>
    /// Signs out the user and clears all session data.
    /// </summary>
    public Task SignOutAsync()
    {
        _stateService.Clear();
        ErrorMessage = null;

        _logger.LogInformation("User signed out");
        NotifyStateChanged();

        return Task.CompletedTask;
    }

    /// <summary>
    /// Gets the URL for a workspace.
    /// </summary>
    public string GetWorkspaceUrl(Workspace workspace)
    {
        // Map workspace names to URL-friendly paths
        var name = workspace.Name?.ToLower() ?? "";
        return name switch
        {
            "incident" => "/incidents",
            "task" => "/tasks",
            "service request" or "servicerequest" => "/service-requests",
            "change" => "/changes",
            "problem" => "/problems",
            "knowledge" => "/knowledge",
            "ci" or "configuration item" => "/configuration-items",
            "release" => "/releases",
            _ => $"/{name.Replace(" ", "-")}"
        };
    }

    /// <summary>
    /// Gets the icon for a workspace.
    /// </summary>
    public string GetWorkspaceIcon(Workspace workspace)
    {
        var name = workspace.Name?.ToLower() ?? "";
        return name switch
        {
            "incident" => Icons.Material.Filled.ReportProblem,
            "task" => Icons.Material.Filled.Task,
            "service request" or "servicerequest" => Icons.Material.Filled.RequestPage,
            "change" => Icons.Material.Filled.ChangeCircle,
            "problem" => Icons.Material.Filled.BugReport,
            "knowledge" => Icons.Material.Filled.MenuBook,
            "ci" or "configuration item" => Icons.Material.Filled.Dns,
            "release" => Icons.Material.Filled.RocketLaunch,
            "approval" => Icons.Material.Filled.ThumbUpAlt,
            "journal" => Icons.Material.Filled.Article,
            _ => Icons.Material.Filled.Workspaces
        };
    }

    /// <summary>
    /// Gets user display name.
    /// </summary>
    public string GetUserDisplayName()
    {
        if (UserData == null) return "User";

        if (!string.IsNullOrEmpty(UserData.DisplayName))
            return UserData.DisplayName;

        return $"{UserData.FirstName} {UserData.LastName}".Trim();
    }

    /// <summary>
    /// Gets user initials for avatar.
    /// </summary>
    public string GetUserInitials()
    {
        if (UserData == null) return "?";

        var first = UserData.FirstName?.FirstOrDefault() ?? ' ';
        var last = UserData.LastName?.FirstOrDefault() ?? ' ';

        return $"{first}{last}".Trim().ToUpper();
    }

    /// <summary>
    /// Loads form data for a specific workspace (when navigating to it).
    /// </summary>
    public async Task<WorkspaceFullData?> LoadWorkspaceFormDataAsync(string workspaceId, CancellationToken ct = default)
    {
        var result = await _ivantiClient.LoadWorkspaceFormDataAsync(workspaceId, ct);
        if (result.IsSuccess)
        {
            NotifyStateChanged();
            return result.Value;
        }

        _logger.LogError("Failed to load workspace form data: {Error}", result.Error);
        return null;
    }

    /// <summary>
    /// Loads form data for a specific workspace by name.
    /// </summary>
    public async Task<WorkspaceFullData?> LoadWorkspaceFormDataByNameAsync(string workspaceName, CancellationToken ct = default)
    {
        var result = await _ivantiClient.LoadWorkspaceFormDataByNameAsync(workspaceName, ct);
        if (result.IsSuccess)
        {
            NotifyStateChanged();
            return result.Value;
        }

        _logger.LogError("Failed to load workspace form data: {Error}", result.Error);
        return null;
    }

    private void NotifyStateChanged() => OnStateChanged?.Invoke();
}
