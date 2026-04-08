using Application.Features.Workspaces.Models.RoleWorkspaces;
using FormDefaultDataModel = Application.Features.Workspaces.Models.FormDefaultData.FormDefaultData;
using FormValidationListDataModel = Application.Features.Workspaces.Models.FormValidationListData.FormValidationListData;
using FormViewDataModel = Application.Features.Workspaces.Models.FormViewData.FormViewData;
using ValidatedSearchModel = Application.Features.Workspaces.Models.ValidatedSearch.ValidatedSearch;
using WorkspaceDataModel = Application.Features.Workspaces.Models.WorkspaceData.WorkspaceData;

namespace Application.Features.Workspaces.Models.WorkspaceData;

/// <summary>
/// Contains all data for a single workspace including workspace data,
/// form view data, form defaults, validation lists, and saved searches.
/// </summary>
public class WorkspaceFullData
{
    /// <summary>
    /// The workspace definition from RoleWorkspaces.
    /// </summary>
    public Workspace Workspace { get; set; } = null!;

    /// <summary>
    /// The workspace data including layout, search configuration, etc.
    /// </summary>
    public WorkspaceDataModel? WorkspaceData { get; set; }

    /// <summary>
    /// The form view data for creating/editing records.
    /// </summary>
    public FormViewDataModel? FormViewData { get; set; }

    /// <summary>
    /// Default values for new form records.
    /// </summary>
    public FormDefaultDataModel? FormDefaultData { get; set; }

    /// <summary>
    /// Validation list data for form fields.
    /// </summary>
    public FormValidationListDataModel? FormValidationListData { get; set; }

    /// <summary>
    /// List of validated/saved searches for this workspace.
    /// </summary>
    public List<ValidatedSearchModel>? ValidatedSearches { get; set; }

    /// <summary>
    /// Indicates if all data was loaded successfully.
    /// </summary>
    public bool IsFullyLoaded => 
        WorkspaceData != null && 
        FormViewData != null && 
        FormDefaultData != null;

    /// <summary>
    /// Any error message if loading failed.
    /// </summary>
    public string? ErrorMessage { get; set; }
}
