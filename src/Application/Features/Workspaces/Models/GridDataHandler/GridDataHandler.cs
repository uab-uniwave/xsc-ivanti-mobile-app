using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.GridDataHandler;

/// <summary>
/// Represents the grid data result from a search query.
/// Contains paging information and the actual data rows.
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
    public List<GridColumn>? Columns { get; set; }
}

/// <summary>
/// Represents a column definition in the grid.
/// </summary>
public class GridColumn
{
    [JsonPropertyName("Field")]
    public string? Field { get; set; }

    [JsonPropertyName("Title")]
    public string? Title { get; set; }

    [JsonPropertyName("Width")]
    public string? Width { get; set; }

    [JsonPropertyName("Type")]
    public string? Type { get; set; }

    [JsonPropertyName("Hidden")]
    public bool Hidden { get; set; }
}
