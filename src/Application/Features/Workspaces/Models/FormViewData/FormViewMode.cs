namespace Application.Features.Workspaces.Models.FormViewData;

/// <summary>
/// Distinguishes between different form view modes based on the operation being performed.
/// Determines which LayoutData view name should be used when loading FormViewData.
/// </summary>
public enum FormViewMode
{
    /// <summary>
    /// For creating new records (entities).
    /// Uses: workspaceData.LayoutData?.OneNewRecordView
    /// Typically used for: New Incident, New Request, etc.
    /// </summary>
    Create = 0,

    /// <summary>
    /// For editing existing records (entities).
    /// Uses: workspaceData.LayoutData?.OneEditRecordView
    /// Typically used for: Edit Incident, Update Request, etc.
    /// Default mode for form data loading.
    /// </summary>
    Edit = 1
}
