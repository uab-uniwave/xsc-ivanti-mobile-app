using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.GridDataHandler;

public class GridDataField
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("mapping")]
    public string? Mapping { get; set; }
}
