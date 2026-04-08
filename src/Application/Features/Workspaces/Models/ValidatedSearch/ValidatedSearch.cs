using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.ValidatedSearch;

public class ValidatedSearch
{
    [JsonPropertyName("__type")]
    public string? Type { get; set; }

    [JsonPropertyName("ExtType")]
    public string? ExtType { get; set; }

    [JsonPropertyName("IsValid")]
    public bool IsValid { get; set; }

    [JsonPropertyName("Id")]
    public string? Id { get; set; }

    [JsonPropertyName("CategoryName")]
    public string? CategoryName { get; set; }

    [JsonPropertyName("Name")]
    public string? Name { get; set; }

    [JsonPropertyName("isFavorite")]
    public bool IsFavorite { get; set; }

    [JsonPropertyName("RolesDefault")]
    public List<string> RolesDefault { get; set; } = new();

    [JsonPropertyName("isDefaultMy")]
    public bool IsDefaultMy { get; set; }

    [JsonPropertyName("ObjectId")]
    public string? ObjectId { get; set; }

    [JsonPropertyName("Description")]
    public string? Description { get; set; }

    [JsonPropertyName("OwnerId")]
    public string? OwnerId { get; set; }

    [JsonPropertyName("Query")]
    public string? Query { get; set; }

    [JsonPropertyName("Conditions")]
    public List<SearchCondition> Conditions { get; set; } = new();

    [JsonPropertyName("RelatedObjects")]
    public List<SearchRelatedObject> RelatedObjects { get; set; } = new();

    [JsonPropertyName("Rights")]
    public SearchRights? Rights { get; set; }
}

public class SearchCondition
{
    [JsonPropertyName("__type")]
    public string? Type { get; set; }

    [JsonPropertyName("ObjectId")]
    public string? ObjectId { get; set; }

    [JsonPropertyName("ObjectDisplay")]
    public string? ObjectDisplay { get; set; }

    [JsonPropertyName("JoinRule")]
    public string? JoinRule { get; set; }

    [JsonPropertyName("Condition")]
    public string? Condition { get; set; }

    [JsonPropertyName("ConditionType")]
    public int ConditionType { get; set; }

    [JsonPropertyName("FieldName")]
    public string? FieldName { get; set; }

    [JsonPropertyName("FieldDisplay")]
    public string? FieldDisplay { get; set; }

    [JsonPropertyName("FieldType")]
    public string? FieldType { get; set; }

    [JsonPropertyName("FieldValue")]
    public string? FieldValue { get; set; }

    [JsonPropertyName("FieldValueDisplay")]
    public string? FieldValueDisplay { get; set; }
}

public class SearchRelatedObject
{
    [JsonPropertyName("ID")]
    public string? ID { get; set; }

    [JsonPropertyName("ObjectId")]
    public string? ObjectId { get; set; }

    [JsonPropertyName("Name")]
    public string? Name { get; set; }

    [JsonPropertyName("Style")]
    public string? Style { get; set; }

    [JsonPropertyName("ThereCardinality")]
    public string? ThereCardinality { get; set; }
}

public class SearchRights
{
    [JsonPropertyName("CanEdit")]
    public bool CanEdit { get; set; }

    [JsonPropertyName("CanDelete")]
    public bool CanDelete { get; set; }

    [JsonPropertyName("CanShare")]
    public bool CanShare { get; set; }
}
