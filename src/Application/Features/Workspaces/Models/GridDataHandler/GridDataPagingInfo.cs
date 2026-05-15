using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.GridDataHandler;

/// <summary>
/// Paging information returned by the Ivanti GridDataHandler response.
/// </summary>
public class GridDataPagingInfo
{
    [JsonPropertyName("startRow")]
    public int StartRow { get; set; }

    [JsonPropertyName("numberRows")]
    public int NumberRows { get; set; }

    [JsonPropertyName("totalRows")]
    public int TotalRows { get; set; }

    /// <summary>
    /// Total number of rows available for paging.
    /// May differ from <see cref="TotalRows"/> when filters are applied.
    /// </summary>
    [JsonPropertyName("pagingRows")]
    public int PagingRows { get; set; }

    [JsonPropertyName("pageSize")]
    public int PageSize { get; set; }

    /// <summary>
    /// Whether the grid is using best-fit column sizing.
    /// </summary>
    [JsonPropertyName("bestFit")]
    public bool BestFit { get; set; }
}
