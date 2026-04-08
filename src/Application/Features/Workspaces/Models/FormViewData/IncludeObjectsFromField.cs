using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.FormViewData;

public class IncludeObjectsFromField
{
    [JsonPropertyName("Field")]
    public string? Field { get; set; }
}
