using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.RoleWorkspaces;

public sealed class RoleWorkspaceNotifications
{
    [JsonPropertyName("MaintenanceNotification")]
    public string? MaintenanceNotification { get; set; }

    [JsonPropertyName("ReleaseNotification")]
    public string? ReleaseNotification { get; set; }

    [JsonPropertyName("AllReleaseNotification")]
    public string? AllReleaseNotification { get; set; }
}
