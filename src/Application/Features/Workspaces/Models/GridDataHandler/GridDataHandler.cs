using System.Text.Json;
using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.GridDataHandler;

/// <summary>
/// Response for GridDataHandler API payloads that return metadata and rows directly.
/// </summary>
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

    [JsonPropertyName("Data")]
    public List<Dictionary<string, object>>? Data { get; set; }

    [JsonPropertyName("Columns")]
    public List<GridDataMetaColumn>? Columns { get; set; }

    [JsonPropertyName("metaData")]
    public GridDataMetaData? MetaData { get; set; }

    [JsonPropertyName("rows")]
    public List<Dictionary<string, JsonElement>>? Rows { get; set; }
}
