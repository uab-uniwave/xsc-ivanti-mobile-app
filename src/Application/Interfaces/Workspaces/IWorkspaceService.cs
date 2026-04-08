using Application.Common;
using Application.Features.Workspaces.Models.FormDefaultData;
using Application.Features.Workspaces.Models.FormValidationListData;
using Application.Features.Workspaces.Models.FormViewData;
using Application.Features.Workspaces.Models.RoleWorkspaces;
using Application.Features.Workspaces.Models.ValidatedSearch;
using Application.Features.Workspaces.Models.WorkspaceData;

namespace Application.Interfaces.Workspaces;

/// <summary>
/// Service for managing Ivanti workspaces and their associated data.
/// Provides access to workspace configurations, forms, and search functionality.
/// </summary>
public interface IWorkspaceService
{
    /// <summary>
    /// Gets all available workspaces for the current user's role.
    /// </summary>
    Task<Result<RoleWorkspaces>> GetRoleWorkspacesAsync(CancellationToken ct = default);

    /// <summary>
    /// Gets workspace data including layout and search configurations.
    /// </summary>
    Task<Result<WorkspaceData>> GetWorkspaceDataAsync(
        string? searchId = null,
        CancellationToken ct = default);

    /// <summary>
    /// Finds form view data for creating new records in a workspace.
    /// </summary>
    Task<Result<FormViewData>> FindFormViewDataAsync(
        string layoutName,
        string objectId,
        string viewName,
        bool isNewRecord = true,
        CancellationToken ct = default);

    /// <summary>
    /// Gets default form data for a new record.
    /// </summary>
    Task<Result<FormDefaultData>> GetFormDefaultDataAsync(
        string formName,
        string layoutName,
        string objectId,
        string objectType,
        string viewName,
        CancellationToken ct = default);

    /// <summary>
    /// Gets validation list data for form fields.
    /// </summary>
    Task<Result<FormValidationListData>> GetFormValidationListDataAsync(
        string namedValidators,
        object masterFormValues,
        string objectId,
        CancellationToken ct = default);

    /// <summary>
    /// Gets all validated searches (favorite searches) for the current workspace.
    /// </summary>
    Task<Result<List<ValidatedSearch>>> GetValidatedSearchesAsync(
        WorkspaceData workspaceData,
        string layoutName,
        CancellationToken ct = default);
}
