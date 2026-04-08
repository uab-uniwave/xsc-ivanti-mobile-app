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
