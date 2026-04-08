using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.WorkspaceData;

public class WorkspaceFieldsTreeData
{
    [JsonPropertyName("TableDef")]
    public WorkspaceTableDef? TableDef { get; set; }
}
