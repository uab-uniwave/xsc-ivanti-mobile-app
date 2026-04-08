using System.Text.Json;
using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.FormViewData;

public class ExpressionTree
{
    public string? FunctionName { get; set; }

    public bool? IsMethod { get; set; }

    public int? Kind { get; set; }

    public object? Value { get; set; }

    public List<ExpressionTree>? Arguments { get; set; }

    public ExpressionTree? Left { get; set; }

    public ExpressionTree? Right { get; set; }

    public ExpressionTree? WhenTrue { get; set; }

    public ExpressionTree? WhenFalse { get; set; }

    public ExpressionTree? Condition { get; set; }

    public int? Op { get; set; }

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? AdditionalData { get; set; }
}
