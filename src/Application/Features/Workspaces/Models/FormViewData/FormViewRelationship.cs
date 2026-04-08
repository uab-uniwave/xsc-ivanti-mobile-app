using System.Text.Json;
using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.FormViewData;

public class FormViewRelationship
{
    [JsonPropertyName("Binding")]
    public int Binding { get; set; }

    [JsonPropertyName("CompositeTypeFilter")]
    public List<string>? CompositeTypeFilter { get; set; }

    [JsonPropertyName("Condition")]
    public FormViewExpression? Condition { get; set; }

    [JsonPropertyName("DesignerName")]
    public string? DesignerName { get; set; }

    [JsonPropertyName("Direction")]
    public int Direction { get; set; }

    [JsonPropertyName("DisplayName")]
    public string? DisplayName { get; set; }

    [JsonPropertyName("ForeignKey")]
    public FormViewForeignKeyInfo? ForeignKey { get; set; }

    [JsonPropertyName("FullTextSearchIndex")]
    public bool FullTextSearchIndex { get; set; }

    [JsonPropertyName("HereCardinality")]
    public int HereCardinality { get; set; }

    [JsonPropertyName("HerePreventDelete")]
    public bool HerePreventDelete { get; set; }

    [JsonPropertyName("HereRequired")]
    public bool HereRequired { get; set; }

    [JsonPropertyName("Implementation")]
    public int Implementation { get; set; }

    [JsonPropertyName("IncludeObjectsFromFields")]
    public List<FormViewIncludeObjectsFromField>? IncludeObjectsFromFields { get; set; }

    [JsonPropertyName("IsASymmetric")]
    public bool IsASymmetric { get; set; }

    [JsonPropertyName("IsCommonlyUsed")]
    public bool IsCommonlyUsed { get; set; }

    [JsonPropertyName("IsDeprecated")]
    public bool IsDeprecated { get; set; }

    [JsonPropertyName("LinkTableFieldMapping")]
    public object? LinkTableFieldMapping { get; set; }

    [JsonPropertyName("LinkTableName")]
    public string? LinkTableName { get; set; }

    [JsonPropertyName("OwnerTargetSubtypeFieldRef")]
    public string? OwnerTargetSubtypeFieldRef { get; set; }

    [JsonPropertyName("Permissions")]
    public int? Permissions { get; set; }

    [JsonPropertyName("Tag")]
    public string? Tag { get; set; }

    [JsonPropertyName("TargetDefaultSubtype")]
    public string? TargetDefaultSubtype { get; set; }

    [JsonPropertyName("ThereCardinality")]
    public int ThereCardinality { get; set; }

    [JsonPropertyName("ThereRequired")]
    public bool ThereRequired { get; set; }

    [JsonPropertyName("UpdateFields")]
    public List<FormViewUpdateField>? UpdateFields { get; set; }

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? AdditionalData { get; set; }
}
