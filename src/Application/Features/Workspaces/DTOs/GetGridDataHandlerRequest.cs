using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.DTOs;

/// <summary>
/// Request DTO for GridDataHandler API endpoint.
/// Used to fetch grid data with search filters, sorting, and paging.
/// </summary>
public class GetGridDataHandlerRequest
{

    [JsonPropertyName("gridDefName")]
    public string? GridDefName { get; set; }

    
    [JsonPropertyName("startRow")]
    public int StartRow { get; set; } = 0;

    [JsonPropertyName("pageSize")]
    public int PageSize { get; set; } = 24;

    [JsonPropertyName("bestFit")]
    public bool BestFit { get; set; } = false;

    [JsonPropertyName("searchInfo")]
    public string? SearchInfo { get; set; } 

}
