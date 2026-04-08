using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.WorkspaceData;

public class WorkspaceData
{
    [JsonPropertyName("__type")]
    public string? Type { get; set; }

    [JsonPropertyName("ObjectId")]
    public string? ObjectId { get; set; }

    [JsonPropertyName("ObjectDisplay")]
    public string? ObjectDisplay { get; set; }

    [JsonPropertyName("AllowDesign")]
    public bool AllowDesign { get; set; }

    [JsonPropertyName("SearchData")]
    public WorkspaceSearchData? SearchData { get; set; }


    [JsonPropertyName("LayoutData")]
    public WorkspaceLayoutData? LayoutData { get; set; }
}
