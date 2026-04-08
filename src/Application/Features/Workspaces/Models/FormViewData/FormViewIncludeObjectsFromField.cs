using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.FormViewData;

public class FormViewIncludeObjectsFromField
{
    [JsonPropertyName("Field")]
    public string? Field { get; set; }
}
