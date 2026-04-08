using System.Text.Json;
using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.FormViewData;

public class FormViewTableMeta
{
    [JsonPropertyName("TableRef")]
    public string? TableRef { get; set; }

    [JsonPropertyName("DisplayName")]
    public string? DisplayName { get; set; }

    [JsonPropertyName("Fields")]
    public Dictionary<string, FormViewFieldMeta> Fields { get; set; } = new();

    [JsonPropertyName("IsNeuronsWorkflowExecutedFieldRef")]
    public string? IsNeuronsWorkflowExecutedFieldRef { get; set; }

    [JsonPropertyName("IsRecordLockingEnabled")]
    public bool IsRecordLockingEnabled { get; set; }

    [JsonPropertyName("DisplayFieldRef")]
    public string? DisplayFieldRef { get; set; }

    [JsonPropertyName("TypeSelectorFieldRef")]
    public string? TypeSelectorFieldRef { get; set; }

    [JsonPropertyName("IsInChangeControl")]
    public bool IsInChangeControl { get; set; }

    [JsonPropertyName("CalculatedRules")]
    public Dictionary<string, FormViewCalculatedRule>? CalculatedRules { get; set; }

    [JsonPropertyName("FieldsEncryption")]
    public Dictionary<string, object>? FieldsEncryption { get; set; }

    [JsonPropertyName("ValidatedFields")]
    public Dictionary<string, FormViewValidatedField>? ValidatedFields { get; set; }

    [JsonPropertyName("Relationships")]
    public Dictionary<string, FormViewRelationship>? Relationships { get; set; }

    [JsonPropertyName("Rel2")]
    public Dictionary<string, FormViewRel2Relationship>? Rel2 { get; set; }

    [JsonPropertyName("PushToRelationships")]
    public object? PushToRelationships { get; set; }

    [JsonPropertyName("DerivedTypesRefs")]
    public List<string> DerivedTypesRefs { get; set; } = new();

    [JsonPropertyName("CompositeContracts")]
    public List<object> CompositeContracts { get; set; } = new();

    [JsonPropertyName("SearchPreviewFields")]
    public List<FromViewSearchPreviewField>? SearchPreviewFields { get; set; }

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? AdditionalData { get; set; }
}
