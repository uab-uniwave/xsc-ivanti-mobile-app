using System.Text.Json;
using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.FormViewData;

public class RuleMeta
{
    [JsonPropertyName("TableRef")]
    public string? TableRef { get; set; }

    [JsonPropertyName("DefaultValues")]
    public List<RuleValue> DefaultValues { get; set; } = new();

    [JsonPropertyName("BeforeSaveValues")]
    public List<RuleValue> BeforeSaveValues { get; set; } = new();

    [JsonPropertyName("AutoFill")]
    public Dictionary<string, Dictionary<string, AutoFillRule>> AutoFill { get; set; } = new();

    [JsonPropertyName("ReadOnlyExpressions")]
    public Dictionary<string, ExpressionRule> ReadOnlyExpressions { get; set; } = new();

    [JsonPropertyName("RequiredExpressions")]
    public Dictionary<string, ExpressionRule> RequiredExpressions { get; set; } = new();

    [JsonPropertyName("FinalStateRule")]
    public FinalStateRule? FinalStateRule { get; set; }

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? AdditionalData { get; set; }
}
