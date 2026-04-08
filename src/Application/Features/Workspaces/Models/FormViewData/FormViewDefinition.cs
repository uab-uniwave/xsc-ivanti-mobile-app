using System.Text.Json;
using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.FormViewData;

public class FormViewDefinition
{
    [JsonPropertyName("__type")]
    public string? Type { get; set; }

    [JsonPropertyName("TableMeta")]
    public FormViewTableMeta? TableMeta { get; set; }

    [JsonPropertyName("FormMeta")]
    public FormViewMeta? FormMeta { get; set; }

    [JsonPropertyName("RuleMeta")]
    public FormViewRuleMeta? RuleMeta { get; set; }

    [JsonPropertyName("BusObjectReadOnlyRules")]
    public List<string>? BusObjectReadOnlyRules { get; set; }

    [JsonPropertyName("BusObjectRequiredRules")]
    public List<string>? BusObjectRequiredRules { get; set; }

    [JsonPropertyName("LinkIdMap")]
    public Dictionary<string, string>? LinkIdMap { get; set; }

    [JsonPropertyName("LinkValidationFields")]
    public Dictionary<string, object>? LinkValidationFields { get; set; }

    [JsonPropertyName("FieldValidationTableRights")]
    public Dictionary<string, int>? FieldValidationTableRights { get; set; }

    [JsonPropertyName("FieldsNotEditable")]
    public Dictionary<string, object>? FieldsNotEditable { get; set; }

    [JsonPropertyName("FormAllowView")]
    public bool FormAllowView { get; set; }

    [JsonPropertyName("FormAllowInsert")]
    public bool FormAllowInsert { get; set; }

    [JsonPropertyName("FormAllowUpdate")]
    public bool FormAllowUpdate { get; set; }

    [JsonPropertyName("FormAllowEditInFinalState")]
    public bool FormAllowEditInFinalState { get; set; }

    [JsonPropertyName("ReferencedFields")]
    public Dictionary<string, FormViewFieldMeta>? ReferencedFields { get; set; }

    [JsonPropertyName("LastModified")]
    public string? LastModified { get; set; }

    [JsonPropertyName("LocalizedFields")]
    public object? LocalizedFields { get; set; }

    [JsonPropertyName("FormCellExpressions")]
    public Dictionary<string, object>? FormCellExpressions { get; set; }

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? AdditionalData { get; set; }
}
