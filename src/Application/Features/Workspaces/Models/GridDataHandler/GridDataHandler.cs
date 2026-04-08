using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.GridDataHandler;

public class GridDataHandler
{
    [JsonPropertyName("TotalCount")]
    public int TotalCount { get; set; }

    [JsonPropertyName("Skip")]
    public int Skip { get; set; }

    [JsonPropertyName("Take")]
    public int Take { get; set; }

    [JsonPropertyName("Sort")]
    public string? Sort { get; set; }

    [JsonPropertyName("Filter")]
    public string? Filter { get; set; }
}
