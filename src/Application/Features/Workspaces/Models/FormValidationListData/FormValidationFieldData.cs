using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.FormValidationListData;

/// <summary>
/// Represents field data structure containing dropdown/lookup information for form fields
/// </summary>
public class FormValidationFieldData
{
    /// <summary>
    /// Maps field property names to their column indices in the Data array
    /// </summary>
    [JsonPropertyName("FieldMap")]
    public Dictionary<string, int>? FieldMap { get; set; }

    /// <summary>
    /// The actual data rows for this field. Each array element is a row with values corresponding to FieldMap indices
    /// </summary>
    [JsonPropertyName("Data")]
    public List<List<object>>? Data { get; set; }

    /// <summary>
    /// If not null, indicates this field uses the same data as another field (referenced by this property name)
    /// </summary>
    [JsonPropertyName("SameAs")]
    public string? SameAs { get; set; }

    /// <summary>
    /// Indicates if there are more records available beyond what's returned
    /// </summary>
    [JsonPropertyName("More")]
    public bool More { get; set; }

    /// <summary>
    /// If true, a single value in the dropdown should be automatically selected
    /// </summary>
    [JsonPropertyName("AutoSelectSingleValue")]
    public bool AutoSelectSingleValue { get; set; }
}
