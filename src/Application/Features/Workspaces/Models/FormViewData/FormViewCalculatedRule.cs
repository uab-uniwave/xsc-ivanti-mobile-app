    using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.FormViewData;

public class FormViewCalculatedRule
{
    [JsonPropertyName("Calculated")]
    public int Calculated { get; set; }

    [JsonPropertyName("CalculatedExpression")]
    public FormViewExpression? CalculatedExpression { get; set; }

    [JsonPropertyName("Description")]
    public string? Description { get; set; }

    [JsonPropertyName("Name")]
    public string? Name { get; set; }

    [JsonPropertyName("RecalculateOnLoad")]
    public bool RecalculateOnLoad { get; set; }
}
