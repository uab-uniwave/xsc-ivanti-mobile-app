using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.DTOs;

/// <summary>
/// Request DTO for GetWorkspaceData API endpoint.
/// Sent to Ivanti API to retrieve workspace data and search configuration.
/// </summary>
public class GetWorkspaceDataRequest
{
    [JsonPropertyName("SearchId")]
    public Guid? SearchId { get; init; }

    [JsonPropertyName("_csrfToken")]
    public string? CsrfToken { get; init; }
}
