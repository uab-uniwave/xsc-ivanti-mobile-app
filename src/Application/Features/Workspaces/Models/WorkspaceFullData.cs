using Application.Features.Workspaces.Models.RoleWorkspaces;

namespace Application.Features.Workspaces.Models;

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
    public WorkspaceData.WorkspaceData? WorkspaceData { get; set; }

    /// <summary>
    /// The form view data for creating/editing records.
    /// </summary>
    public FormViewData.FormViewData? FormViewData { get; set; }

    /// <summary>
    /// Default values for new form records.
    /// </summary>
    public FormDefaultData.FormDefaultData? FormDefaultData { get; set; }

    /// <summary>
    /// Validation list data for form fields.
    /// </summary>
    public FormValidationListData.FormValidationListData? FormValidationListData { get; set; }

    /// <summary>
    /// List of validated/saved searches for this workspace.
    /// </summary>
    public List<ValidatedSearch.ValidatedSearch>? ValidatedSearches { get; set; }

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
