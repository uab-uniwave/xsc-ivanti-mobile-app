using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.FormViewData;

public class FormViewToolbarDef
{
    [JsonPropertyName("Items")]
    public object? Items { get; set; }

    [JsonPropertyName("Expressions")]
    public List<object>? Expressions { get; set; }

    [JsonPropertyName("JsonItems")]
    public List<List<object?>>? JsonItems { get; set; }
}
