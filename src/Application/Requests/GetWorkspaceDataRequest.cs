using System.Text.Json.Serialization;

namespace Application.Requests;

/// <summary>
/// Request DTO for GetWorkspaceData API endpoint.
/// Sent to Ivanti API to retrieve workspace data and search configuration.
/// </summary>
public class GetWorkspaceDataRequest
{
    [JsonPropertyName("LayoutName")]
    public string? LayoutName { get; init; }

    [JsonPropertyName("ObjectId")]
    public string? ObjectId { get; init; }

    [JsonPropertyName("_csrfToken")]
    public string? CsrfToken { get; init; }
}
