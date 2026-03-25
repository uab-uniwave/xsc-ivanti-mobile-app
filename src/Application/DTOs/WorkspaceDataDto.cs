using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Application.DTOs;

/// <summary>
/// DTO for workspace data returned from Ivanti API.
/// Maps to Application.Models.WorkspaceData.
/// </summary>
public class WorkspaceDataDto
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
    public WorkspaceSearchDataDto? SearchData { get; set; }
}

/// <summary>
/// DTO for workspace search data.
/// </summary>
public class WorkspaceSearchDataDto
{
    [JsonPropertyName("previewGridName")]
    public string? PreviewGridName { get; set; }

    [JsonPropertyName("relatedObjects")]
    public List<List<string?>> RelatedObjects { get; set; } = new();

    [JsonPropertyName("favorites")]
    public List<WorkspaceFavoriteDto> Favorites { get; set; } = new();

    [JsonPropertyName("allowFullTextSearch")]
    public bool AllowFullTextSearch { get; set; }

    [JsonPropertyName("canCreate")]
    public bool CanCreate { get; set; }

    [JsonPropertyName("fieldsTreeData")]
    public WorkspaceFieldsTreeDataDto? FieldsTreeData { get; set; }
}

/// <summary>
/// DTO for workspace favorite.
/// </summary>
public class WorkspaceFavoriteDto
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

/// <summary>
/// DTO for workspace fields tree data.
/// </summary>
public class WorkspaceFieldsTreeDataDto
{
    [JsonPropertyName("TableDef")]
    public WorkspaceTableDefDto? TableDef { get; set; }
}

/// <summary>
/// DTO for workspace table definition.
/// </summary>
public class WorkspaceTableDefDto
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
    public List<WorkspaceFieldItemDto> Fields { get; set; } = new();
}

/// <summary>
/// DTO for workspace field item.
/// </summary>
public class WorkspaceFieldItemDto
{
    [JsonPropertyName("MetaData")]
    public WorkspaceFieldMetaDataDto? MetaData { get; set; }

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

/// <summary>
/// DTO for workspace field metadata.
/// </summary>
public class WorkspaceFieldMetaDataDto
{
    [JsonPropertyName("DisplayName")]
    public string? DisplayName { get; set; }

    [JsonPropertyName("FieldName")]
    public string? FieldName { get; set; }

    [JsonPropertyName("DataType")]
    public int DataType { get; set; }

    [JsonPropertyName("IsRequired")]
    public bool IsRequired { get; set; }
}
