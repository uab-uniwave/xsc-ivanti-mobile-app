using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.DTOs;

/// <summary>
/// Request DTO for GetRoleWorkspaces API endpoint.
/// Sent to Ivanti API to retrieve workspace configuration for user's role.
/// </summary>
public class RoleWorkspacesRequest
{
    [JsonPropertyName("sRole")]
    public string? SRole { get; set; }

    [JsonPropertyName("_csrfToken")]
    public string? CsrfToken { get; set; }
}
