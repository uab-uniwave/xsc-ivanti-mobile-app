using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.WorkspaceData;

public class WorkspaceSearchData
{
    [JsonPropertyName("previewGridName")]
    public string? PreviewGridName { get; set; }

    [JsonPropertyName("relatedObjects")]
    public List<List<string?>> RelatedObjects { get; set; } = new();

    [JsonPropertyName("favorites")]
    public List<WorkspaceFavorite> Favorites { get; set; } = new();

    [JsonPropertyName("allowFullTextSearch")]
    public bool AllowFullTextSearch { get; set; }

    [JsonPropertyName("canCreate")]
    public bool CanCreate { get; set; }

    [JsonPropertyName("fieldsTreeData")]
    public WorkspaceFieldsTreeData? FieldsTreeData { get; set; }
}
