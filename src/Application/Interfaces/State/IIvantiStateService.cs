using Application.Common.Models.SessonData;
using Application.Common.Models.UserData;
using Application.Features.Workspaces.Models;
using Application.Features.Workspaces.Models.FormDefaultData;
using Application.Features.Workspaces.Models.FormValidationListData;
using Application.Features.Workspaces.Models.FormViewData;
using Application.Features.Workspaces.Models.RoleWorkspaces;
using Application.Features.Workspaces.Models.WorkspaceData;

namespace Application.Interfaces.State;

/// <summary>
/// Service for maintaining Ivanti session state across the Blazor circuit.
/// This service holds authentication and workspace data that needs to persist
/// across page navigations within the same user session.
/// </summary>
public interface IIvantiStateService
{
    /// <summary>
    /// Gets or sets the current session data from InitializeSession.
    /// </summary>
    SessionData? SessionData { get; set; }

    /// <summary>
    /// Gets or sets the current user data from GetUserData.
    /// </summary>
    UserData? UserData { get; set; }

    /// <summary>
    /// Gets or sets the role workspaces data.
    /// </summary>
    RoleWorkspaces? RoleWorkspaces { get; set; }

    /// <summary>
    /// Gets or sets all workspace data for all workspaces.
    /// </summary>
    List<WorkspaceFullData> AllWorkspacesData { get; set; }

    /// <summary>
    /// Gets the current/active workspace data (convenience property).
    /// </summary>
    WorkspaceFullData? CurrentWorkspace { get; set; }

    /// <summary>
    /// Gets or sets the current workspace data (legacy - use AllWorkspacesData instead).
    /// </summary>
    [Obsolete("Use AllWorkspacesData or CurrentWorkspace instead")]
    WorkspaceData? WorkspaceData { get; set; }

    /// <summary>
    /// Gets or sets the form view data (legacy - use AllWorkspacesData instead).
    /// </summary>
    [Obsolete("Use AllWorkspacesData or CurrentWorkspace instead")]
    FormViewData? FormViewData { get; set; }

    /// <summary>
    /// Gets or sets the form default data (legacy - use AllWorkspacesData instead).
    /// </summary>
    [Obsolete("Use AllWorkspacesData or CurrentWorkspace instead")]
    FormDefaultData? FormDefaultData { get; set; }

    /// <summary>
    /// Gets or sets the form validation list data (legacy - use AllWorkspacesData instead).
    /// </summary>
    [Obsolete("Use AllWorkspacesData or CurrentWorkspace instead")]
    FormValidationListData? FormValidationListData { get; set; }

    /// <summary>
    /// Gets whether the session has been initialized.
    /// </summary>
    bool IsSessionInitialized { get; }

    /// <summary>
    /// Gets whether user data has been loaded.
    /// </summary>
    bool IsUserDataLoaded { get; }

    /// <summary>
    /// Gets workspace data by workspace ID.
    /// </summary>
    WorkspaceFullData? GetWorkspaceById(string workspaceId);

    /// <summary>
    /// Gets workspace data by workspace name.
    /// </summary>
    WorkspaceFullData? GetWorkspaceByName(string workspaceName);

    /// <summary>
    /// Clears all state data (used on logout).
    /// </summary>
    void Clear();
}
