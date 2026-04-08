using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.RoleWorkspaces;

public sealed class RoleWorkspaces
{
    [JsonPropertyName("__type")]
    public string? Type { get; set; }

    [JsonPropertyName("Workspaces")]
    public List<Workspace> Workspaces { get; set; } = new();

    [JsonPropertyName("RecentWorkspaces")]
    public List<string> RecentWorkspaces { get; set; } = new();

    [JsonPropertyName("AllWorkspaces")]
    public List<Workspace> AllWorkspaces { get; set; } = new();

    [JsonPropertyName("MobileWorkspaces")]
    public List<Workspace> MobileWorkspaces { get; set; } = new();

    [JsonPropertyName("Notifications")]
    public RoleWorkspaceNotifications? Notifications { get; set; }

    [JsonPropertyName("BrandingOptions")]
    public RoleWorkspaceBrandingOptions? BrandingOptions { get; set; }

    [JsonPropertyName("AllowClick2Talk")]
    public bool AllowClick2Talk { get; set; }

    [JsonPropertyName("HelpLinks")]
    public List<RoleWorkspaceHelpLink> HelpLinks { get; set; } = new();

    [JsonPropertyName("ChatEnabled")]
    public bool ChatEnabled { get; set; }

    [JsonPropertyName("MSTeamsIntegrationEnabled")]
    public bool MSTeamsIntegrationEnabled { get; set; }
}
