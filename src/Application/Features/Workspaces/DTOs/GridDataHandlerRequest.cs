using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.DTOs;

/// <summary>
/// Request DTO for GridDataHandler API endpoint.
/// Used to fetch grid data with search filters, sorting, and paging.
/// </summary>
public class GridDataHandlerRequest
{
    [JsonPropertyName("searchId")]
    public Guid? SearchId { get; set; }

    [JsonPropertyName("objectId")]
    public string? ObjectId { get; set; }

    [JsonPropertyName("layoutName")]
    public string? LayoutName { get; set; }

    [JsonPropertyName("skip")]
    public int Skip { get; set; } = 0;

    [JsonPropertyName("take")]
    public int Take { get; set; } = 50;

    [JsonPropertyName("sort")]
    public string? Sort { get; set; }

    [JsonPropertyName("filter")]
    public string? Filter { get; set; }

    [JsonPropertyName("_csrfToken")]
    public string? CsrfToken { get; set; }
}
