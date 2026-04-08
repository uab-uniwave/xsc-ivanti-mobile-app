using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.FormViewData;

public class ValidatedField
{
    [JsonPropertyName("Condition")]
    public Expression? Condition { get; set; }

    [JsonPropertyName("IsStateTransition")]
    public bool IsStateTransition { get; set; }

    [JsonPropertyName("ValidatedIdFieldRef")]
    public string? ValidatedIdFieldRef { get; set; }

    [JsonPropertyName("ValidatorRef")]
    public string? ValidatorRef { get; set; }
}
