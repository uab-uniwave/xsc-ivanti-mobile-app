using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.GridDataHandler;

public class GridDataSortInfo
{
    [JsonPropertyName("field")]
    public string? Field { get; set; }

    [JsonPropertyName("direction")]
    public string? Direction { get; set; }
}
