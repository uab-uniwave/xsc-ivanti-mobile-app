using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.RoleWorkspaces;

/// <summary>
/// Represents the selector options for a role workspace.
/// </summary>
public sealed class RoleWorkspaceSelectorOptions
{
    [JsonPropertyName("NewWindow")]
    public bool NewWindow { get; set; }

    [JsonPropertyName("SelectorMenu")]
    public bool SelectorMenu { get; set; }

    [JsonPropertyName("SelectorMenuOther")]
    public bool SelectorMenuOther { get; set; }
}
