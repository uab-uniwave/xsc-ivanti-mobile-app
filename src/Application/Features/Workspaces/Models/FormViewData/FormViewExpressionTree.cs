using System.Text.Json;
using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.FormViewData;

public class FormViewExpressionTree
{
    public string? FunctionName { get; set; }

    public bool? IsMethod { get; set; }

    public int? Kind { get; set; }

    public object? Value { get; set; }

    public List<FormViewExpressionTree>? Arguments { get; set; }

    public FormViewExpressionTree? Left { get; set; }

    public FormViewExpressionTree? Right { get; set; }

    public FormViewExpressionTree? WhenTrue { get; set; }

    public FormViewExpressionTree? WhenFalse { get; set; }

    public FormViewExpressionTree? Condition { get; set; }

    public int? Op { get; set; }

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? AdditionalData { get; set; }
}
