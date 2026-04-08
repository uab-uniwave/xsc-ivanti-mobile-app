using System.Text.Json;
using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.FormViewData;

public class FormViewRuleMeta
{
    [JsonPropertyName("TableRef")]
    public string? TableRef { get; set; }

    [JsonPropertyName("DefaultValues")]
    public List<FormViewRuleValue> DefaultValues { get; set; } = new();

    [JsonPropertyName("BeforeSaveValues")]
    public List<FormViewRuleValue> BeforeSaveValues { get; set; } = new();

    [JsonPropertyName("AutoFill")]
    public Dictionary<string, Dictionary<string, FormViewAutoFillRule>> AutoFill { get; set; } = new();

    [JsonPropertyName("ReadOnlyExpressions")]
    public Dictionary<string, FormViewExpressionRule> ReadOnlyExpressions { get; set; } = new();

    [JsonPropertyName("RequiredExpressions")]
    public Dictionary<string, FormViewExpressionRule> RequiredExpressions { get; set; } = new();

    [JsonPropertyName("FinalStateRule")]
    public FormViewFinalStateRule? FinalStateRule { get; set; }

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? AdditionalData { get; set; }
}
