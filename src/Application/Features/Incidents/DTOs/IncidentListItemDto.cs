using System.Text.Json.Serialization;

namespace Application.Features.Incidents.DTOs;

/// <summary>
/// DTO for incident list item in paginated responses.
/// Contains summary information for display in lists.
/// </summary>
public class IncidentListItemDto
{
    [JsonPropertyName("RecordId")]
    public string? RecordId { get; set; }

    [JsonPropertyName("DisplayValue")]
    public string? DisplayValue { get; set; }

    [JsonPropertyName("Status")]
    public string? Status { get; set; }

    [JsonPropertyName("Priority")]
    public string? Priority { get; set; }

    [JsonPropertyName("Subject")]
    public string? Subject { get; set; }

    [JsonPropertyName("CreatedDate")]
    public DateTime? CreatedDate { get; set; }

    [JsonPropertyName("LastModified")]
    public DateTime? LastModified { get; set; }
}
