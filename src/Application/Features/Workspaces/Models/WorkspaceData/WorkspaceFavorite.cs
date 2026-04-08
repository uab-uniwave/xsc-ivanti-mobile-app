using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.WorkspaceData;

public class WorkspaceFavorite
{
    [JsonPropertyName("Id")]
    public string? Id { get; set; }

    [JsonPropertyName("Name")]
    public string? Name { get; set; }

    [JsonPropertyName("isDefault")]
    public bool IsDefault { get; set; }

    [JsonPropertyName("CanEdit")]
    public bool CanEdit { get; set; }
}
