using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.RoleWorkspaces;

/// <summary>
/// Represents the system menu options for a role workspace.
/// </summary>
public sealed class RoleWorkspaceSystemMenuOptions
{
    [JsonPropertyName("EnableLogout")]
    public bool EnableLogout { get; set; }

    [JsonPropertyName("EnableChangePassword")]
    public bool EnableChangePassword { get; set; }

    [JsonPropertyName("EnableChatAsAnalyst")]
    public bool EnableChatAsAnalyst { get; set; }

    [JsonPropertyName("EnableChatAsUser")]
    public bool EnableChatAsUser { get; set; }

    [JsonPropertyName("EnableEditProfile")]
    public bool EnableEditProfile { get; set; }

    [JsonPropertyName("EnableHelp")]
    public bool EnableHelp { get; set; }

    [JsonPropertyName("EnableChangeRole")]
    public bool EnableChangeRole { get; set; }
}
