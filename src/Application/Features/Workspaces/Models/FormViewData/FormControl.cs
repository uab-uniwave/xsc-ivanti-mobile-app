using System.Text.Json;
using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.FormViewData;

public class FormControl
{
    [JsonPropertyName("Name")]
    public string? Name { get; set; }

    [JsonPropertyName("FieldRef")]
    public string? FieldRef { get; set; }

    [JsonPropertyName("LinkFieldRef")]
    public string? LinkFieldRef { get; set; }

    [JsonPropertyName("Label")]
    public string? Label { get; set; }

    [JsonPropertyName("Category")]
    public int? Category { get; set; }

    [JsonPropertyName("Editable")]
    public bool? Editable { get; set; }

    [JsonPropertyName("Visible")]
    public bool? Visible { get; set; }

    [JsonPropertyName("Searchable")]
    public bool? Searchable { get; set; }

    [JsonPropertyName("Scannable")]
    public bool? Scannable { get; set; }

    [JsonPropertyName("Height")]
    public int? Height { get; set; }

    [JsonPropertyName("Width")]
    public int? Width { get; set; }

    [JsonPropertyName("TabIndex")]
    public int? TabIndex { get; set; }

    [JsonPropertyName("Style")]
    public string? Style { get; set; }

    [JsonPropertyName("StyleExpression")]
    public string? StyleExpression { get; set; }

    [JsonPropertyName("LabelStyleExpression")]
    public string? LabelStyleExpression { get; set; }

    [JsonPropertyName("VisibleExpression")]
    public string? VisibleExpression { get; set; }

    [JsonPropertyName("DisabledExpression")]
    public string? DisabledExpression { get; set; }

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? AdditionalData { get; set; }
}
