using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.RoleWorkspaces;

public sealed class RoleWorkspaceLink
{
    [JsonPropertyName("DisplayName")]
    public string? DisplayName { get; set; }

    [JsonPropertyName("URL")]
    public string? Url { get; set; }
}
