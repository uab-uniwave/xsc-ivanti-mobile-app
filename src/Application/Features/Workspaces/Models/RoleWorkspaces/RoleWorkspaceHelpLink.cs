using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.RoleWorkspaces;

public sealed class RoleWorkspaceHelpLink
{
    [JsonPropertyName("Category")]
    public string? Category { get; set; }

    [JsonPropertyName("DisplayName")]
    public string? DisplayName { get; set; }

    [JsonPropertyName("Editable")]
    public bool Editable { get; set; }

    [JsonPropertyName("HelpEnable")]
    public bool HelpEnable { get; set; }

    [JsonPropertyName("Language")]
    public string? Language { get; set; }

    [JsonPropertyName("URL")]
    public string? Url { get; set; }
}
