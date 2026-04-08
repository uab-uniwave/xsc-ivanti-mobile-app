using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.GridDataHandler;

public class GridDataPagingInfo
{
    [JsonPropertyName("startRow")]
    public int StartRow { get; set; }

    [JsonPropertyName("numberRows")]
    public int NumberRows { get; set; }

    [JsonPropertyName("totalRows")]
    public int TotalRows { get; set; }

    [JsonPropertyName("pageSize")]
    public int PageSize { get; set; }
}
