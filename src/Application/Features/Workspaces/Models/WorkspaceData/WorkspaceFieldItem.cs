using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.WorkspaceData;

public class WorkspaceFieldItem
{
    [JsonPropertyName("MetaData")]
    public WorkspaceFieldMetaData? MetaData { get; set; }

    [JsonPropertyName("ReferenceKey")]
    public string? ReferenceKey { get; set; }

    [JsonPropertyName("DataType")]
    public string? DataType { get; set; }

    [JsonPropertyName("DataWidth")]
    public int DataWidth { get; set; }

    [JsonPropertyName("DropType")]
    public int DropType { get; set; }

    [JsonPropertyName("DisplayAs")]
    public string? DisplayAs { get; set; }
}
