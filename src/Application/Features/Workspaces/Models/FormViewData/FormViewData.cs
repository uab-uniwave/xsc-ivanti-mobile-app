using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.FormViewData;

/// <summary>
/// Represents form view structure and metadata for a specific workspace.
/// Includes the form definition and metadata required for form rendering.
/// </summary>
public class FormViewData
{
    [JsonPropertyName("__type")]
    public string? Type { get; set; }

    [JsonPropertyName("viewName")]
    public string? ViewName { get; set; }

    [JsonPropertyName("formDef")]
    public FormViewDefinition? FormDef { get; set; }

    /// <summary>
    /// Indicates the mode in which this form view data was loaded.
    /// Used to distinguish between Create (OneNewRecordView) and Edit (OneEditRecordView) modes.
    /// Not serialized from API; set by IvantiClient based on request parameters.
    /// </summary>
    [JsonIgnore]
    public FormViewMode ViewMode { get; set; } = FormViewMode.Edit;
}
