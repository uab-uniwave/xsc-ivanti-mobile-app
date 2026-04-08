using Application.Common;
using Application.Common.Models.SessonData;
using Application.Common.Models.UserData;
using Application.Features.Authentication.DTOs;
using Application.Features.Workspaces.Models;
using Application.Features.Workspaces.Models.FormDefaultData;
using Application.Features.Workspaces.Models.FormValidationListData;
using Application.Features.Workspaces.Models.FormViewData;
using Application.Features.Workspaces.Models.RoleWorkspaces;
using Application.Features.Workspaces.Models.WorkspaceData;
using Application.Features.Workspaces.Models.ValidatedSearch;
using Application.Features.Workspaces.Models.GridDataHandler;
using Application.Features.Authentication.Models;

namespace Application.Services;

/// <summary>
/// Ivanti API client interface for communicating with Ivanti ServiceDesk.
/// </summary>
public interface IIvantiClient
{
    //=====================================================================
    // Authentication
    //=====================================================================

    /// <summary>
    /// Gets the verification token and form data from the login page.
    /// </summary>
    Task<Result<VerificationToken>> GetVerificationToken(CancellationToken ct);

    /// <summary>
    /// Posts login credentials and returns the SelectRole page data with available roles.
    /// </summary>
    Task<Result<SelectRolePageData>> LoginAsync(LoginRequest request, CancellationToken ct);

    /// <summary>
    /// Selects a role and completes the authentication flow.
    /// </summary>
    Task<Result<string>> SelectRoleAsync(string roleId, string verificationToken, CancellationToken ct);

    //=====================================================================
    // Session & User Data
    //=====================================================================

    /// <summary>
    /// Initializes the Ivanti session and retrieves CSRF token.
    /// </summary>
    Task<Result<SessionData>> InitializeSessionAsync(CancellationToken ct);

    /// <summary>
    /// Gets user data including role and permissions.
    /// </summary>
    Task<Result<UserData>> GetUserDataAsync(CancellationToken ct);

    /// <summary>
    /// Gets all workspaces available for the user's role.
    /// </summary>
    Task<Result<RoleWorkspaces>> GetRoleWorkspacesAsync(CancellationToken ct);

    //=====================================================================
    // Workspace Data - Load for ALL workspaces
    //=====================================================================

    /// <summary>
    /// Loads WorkspaceData for ALL workspaces.
    /// Called after GetRoleWorkspaces to get basic data for each workspace.
    /// </summary>
    Task<Result<List<WorkspaceFullData>>> LoadAllWorkspacesBasicDataAsync(CancellationToken ct);

    //=====================================================================
    // Workspace Full Data - Load for SPECIFIC workspace when navigating
    //=====================================================================

    /// <summary>
    /// Loads complete form data for a specific workspace.
    /// Called when user navigates to a workspace (e.g., Incidents).
    /// Loads: FormViewData, FormDefaultData, FormValidationListData, ValidatedSearches
    /// </summary>
    Task<Result<WorkspaceFullData>> LoadWorkspaceFormDataAsync(string workspaceId, CancellationToken ct);

    /// <summary>
    /// Loads complete form data for a specific workspace by name.
    /// Called when user navigates to a workspace (e.g., Incidents).
    /// </summary>
    Task<Result<WorkspaceFullData>> LoadWorkspaceFormDataByNameAsync(string workspaceName, CancellationToken ct);

    //=====================================================================
    // Individual Workspace Data Methods (with explicit parameters)
    //=====================================================================

    /// <summary>
    /// Gets workspace data for a specific workspace.
    /// </summary>
    Task<Result<WorkspaceData>> GetWorkspaceDataAsync(Workspace workspace, CancellationToken ct);

    /// <summary>
    /// Gets form view data for a specific workspace.
    /// </summary>
    Task<Result<FormViewData>> FindFormViewDataAsync(Workspace workspace, WorkspaceData workspaceData, CancellationToken ct);

    /// <summary>
    /// Gets form default data for a specific workspace.
    /// </summary>
    Task<Result<FormDefaultData>> GetFormDefaultDataAsync(Workspace workspace, WorkspaceData workspaceData, FormViewData formViewData, CancellationToken ct);

    /// <summary>
    /// Gets form validation list data for a specific workspace.
    /// </summary>
    Task<Result<FormValidationListData>> GetFormValidationListDataAsync(Workspace workspace, FormViewData formViewData, CancellationToken ct);

    /// <summary>
    /// Gets validated searches for a specific workspace.
    /// </summary>
    Task<Result<List<ValidatedSearch>>> GetValidatedSearchAsync(Workspace workspace, WorkspaceData workspaceData, CancellationToken ct);

    //=====================================================================
    // Grid Data Handler
    //=====================================================================

    /// <summary>
    /// Fetches grid data based on a saved search (favorite).
    /// Called when user selects a saved search from the dropdown.
    /// </summary>
    Task<Result<GridDataHandler>> GetGridDataAsync(string workspaceId, Guid searchId, int skip = 0, int take = 50, CancellationToken ct = default);

    //=====================================================================
    // Legacy Methods (deprecated - for backward compatibility)
    //=====================================================================

    [Obsolete("Use GetWorkspaceDataAsync(Workspace, CancellationToken) instead")]
    Task<Result<WorkspaceData>> GetWorkspaceDataAsync(CancellationToken ct);

    [Obsolete("Use FindFormViewDataAsync(Workspace, WorkspaceData, CancellationToken) instead")]
    Task<Result<FormViewData>> FindFormViewDataAsync(CancellationToken ct);

    [Obsolete("Use GetFormDefaultDataAsync(Workspace, WorkspaceData, FormViewData, CancellationToken) instead")]
    Task<Result<FormDefaultData>> GetFormDefaultDataAsync(CancellationToken ct);

    [Obsolete("Use GetFormValidationListDataAsync(Workspace, FormViewData, CancellationToken) instead")]
    Task<Result<FormValidationListData>> GetFormValidationListDataAsync(CancellationToken ct);

    [Obsolete("Use GetValidatedSearchAsync(Workspace, WorkspaceData, CancellationToken) instead")]
    Task<Result<List<ValidatedSearch>>> GetValidatedSearchAsync(CancellationToken ct);

    [Obsolete("Use GetGridDataAsync with workspaceId parameter")]
    Task<Result<GridDataHandler>> GetGridDataAsync(Guid searchId, int skip = 0, int take = 50, CancellationToken ct = default);
}

