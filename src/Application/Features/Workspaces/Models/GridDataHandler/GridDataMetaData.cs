using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.GridDataHandler;

public class GridDataMetaData
{
    [JsonPropertyName("gridDefName")]
    public string? GridDefName { get; set; }

    [JsonPropertyName("pagingInfo")]
    public GridDataPagingInfo? PagingInfo { get; set; }

    [JsonPropertyName("sortInfo")]
    public GridDataSortInfo? SortInfo { get; set; }

    [JsonPropertyName("fields")]
    public List<GridDataField>? Fields { get; set; }

    [JsonPropertyName("columns")]
    public List<GridDataMetaColumn>? Columns { get; set; }
}
