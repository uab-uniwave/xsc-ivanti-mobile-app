using System.Text.Json.Serialization;
using Domain.Enums;

namespace Application.Features.Incidents.DTOs;

/// <summary>
/// DTO for incident update requests sent to the API.
/// Used when updating existing incident records.
/// </summary>
public class IncidentUpdateRequestDto
{
    [JsonPropertyName("Status")]
    public IncidentStatus? Status { get; set; }

    [JsonPropertyName("Priority")]
    public string? Priority { get; set; }

    [JsonPropertyName("Service")]
    public string? Service { get; set; }

    [JsonPropertyName("Category")]
    public string? Category { get; set; }

    [JsonPropertyName("Urgency")]
    public string? Urgency { get; set; }

    //[JsonPropertyName("Impact")]
    //public IncidentImpact? Impact { get; set; }

    [JsonPropertyName("Owner")]
    public string? Owner { get; set; }

    [JsonPropertyName("OwnerTeam")]
    public string? OwnerTeam { get; set; }

    [JsonPropertyName("Subject")]
    public string? Subject { get; set; }

    [JsonPropertyName("Description")]
    public string? Description { get; set; }
}
