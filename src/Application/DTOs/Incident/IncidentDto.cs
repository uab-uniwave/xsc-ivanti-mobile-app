using System.Text.Json.Serialization;
using Domain.Enums;

namespace Application.DTOs.Incident;

/// <summary>
/// DTO for complete incident data.
/// Maps to Domain.Entities.Incident entity.
/// </summary>
public class IncidentDto
{
    [JsonPropertyName("RecordId")]
    public string? RecordId { get; set; }

    [JsonPropertyName("DisplayValue")]
    public string? DisplayValue { get; set; }

    [JsonPropertyName("Subject")]
    public string? Subject { get; set; }

    [JsonPropertyName("Status")]
    public string? Status { get; set; }

    [JsonPropertyName("Priority")]
    public string? Priority { get; set; }

    [JsonPropertyName("Category")]
    public string? Category { get; set; }

    [JsonPropertyName("Service")]
    public string? Service { get; set; }

    [JsonPropertyName("Urgency")]
    public string? Urgency { get; set; }

    [JsonPropertyName("Impact")]
    public string? Impact { get; set; }

    [JsonPropertyName("Owner")]
    public string? Owner { get; set; }

    [JsonPropertyName("OwnerTeam")]
    public string? OwnerTeam { get; set; }

    [JsonPropertyName("CreatedDate")]
    public DateTime? CreatedDate { get; set; }

    [JsonPropertyName("LastModified")]
    public DateTime? LastModified { get; set; }

    [JsonPropertyName("Description")]
    public string? Description { get; set; }
}
