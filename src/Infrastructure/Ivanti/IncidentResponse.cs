using System.Text.Json.Serialization;

namespace Infrastructure.Ivanti;

/// <summary>
/// Response type for incident operations from the Ivanti API.
/// Used internally by IvantiClient for deserialization.
/// </summary>
public class IncidentResponse
{
    [JsonPropertyName("RecordID")]
    public string? RecordId { get; set; }

    [JsonPropertyName("Subject")]
    public string? Subject { get; set; }

    [JsonPropertyName("Status")]
    public string? Status { get; set; }

    [JsonPropertyName("Priority")]
    public int Priority { get; set; }

    [JsonPropertyName("Service")]
    public string? Service { get; set; }

    [JsonPropertyName("Category")]
    public string? Category { get; set; }

    [JsonPropertyName("Urgency")]
    public string? Urgency { get; set; }

    [JsonPropertyName("Impact")]
    public string? Impact { get; set; }

    [JsonPropertyName("Owner")]
    public string? Owner { get; set; }

    [JsonPropertyName("OwnerTeam")]
    public string? OwnerTeam { get; set; }

    [JsonPropertyName("Description")]
    public string? Description { get; set; }

    [JsonPropertyName("CreatedDate")]
    public string? CreatedDate { get; set; }

    [JsonPropertyName("ModifiedDate")]
    public string? ModifiedDate { get; set; }
}
