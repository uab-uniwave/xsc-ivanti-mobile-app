using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.WorkspaceData;

public class WorkspaceTableDef
{
    [JsonPropertyName("DesignerName")]
    public string? DesignerName { get; set; }

    [JsonPropertyName("DisplayName")]
    public string? DisplayName { get; set; }

    [JsonPropertyName("TableRef")]
    public string? TableRef { get; set; }

    [JsonPropertyName("ReferenceKey")]
    public string? ReferenceKey { get; set; }

    [JsonPropertyName("Fields")]
    public List<WorkspaceFieldItem> Fields { get; set; } = new();
}
