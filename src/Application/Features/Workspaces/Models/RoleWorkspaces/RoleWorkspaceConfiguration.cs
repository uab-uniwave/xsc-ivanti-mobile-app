using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.RoleWorkspaces;

public sealed class RoleWorkspaceConfiguration
{
    [JsonPropertyName("objectName")]
    public string? ObjectName { get; set; }

    [JsonPropertyName("objectRef")]
    public string? ObjectRef { get; set; }

    [JsonPropertyName("layout")]
    public string? Layout { get; set; }
}
