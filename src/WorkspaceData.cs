using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;


public class WorkspaceDataResponse
{
    [JsonPropertyName("d")]
    public WorkspaceData? Data { get; set; }
}

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
}

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

public class WorkspaceFieldsTreeData
{
    [JsonPropertyName("TableDef")]
    public WorkspaceTableDef? TableDef { get; set; }
}

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
    public List<WorkspaceFieldDefinition> Fields { get; set; } = new();
}

public class WorkspaceFieldDefinition
{
    [JsonPropertyName("MetaData")]
    public FieldMetaData? MetaData { get; set; }

    [JsonPropertyName("ReferenceKey")]
    public string? ReferenceKey { get; set; }

    [JsonPropertyName("DataType")]
    public string? DataType { get; set; }

    [JsonPropertyName("DataWidth")]
    public int? DataWidth { get; set; }

    [JsonPropertyName("DropType")]
    public int? DropType { get; set; }

    [JsonPropertyName("DisplayAs")]
    public string? DisplayAs { get; set; }
}