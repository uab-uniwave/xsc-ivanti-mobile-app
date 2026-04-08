using System.Text.Json;
using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.GridDataHandler;

public class GridDataClassExpresin
{
    [JsonPropertyName("Description")]
    public string? Description { get; set; }

    [JsonPropertyName("FieldRefs")]
    public List<string>? FieldRefs { get; set; }

    [JsonPropertyName("IsFullExpression")]
    public bool IsFullExpression { get; set; }

    [JsonPropertyName("Name")]
    public string? Name { get; set; }

    [JsonPropertyName("Tree")]
    public JsonElement? Tree { get; set; }

    [JsonPropertyName("Source")]
    public string? Source { get; set; }

    [JsonPropertyName("ValidationStatus")]
    public int? ValidationStatus { get; set; }

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? AdditionalData { get; set; }
}
