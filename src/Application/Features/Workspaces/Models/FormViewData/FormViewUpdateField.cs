using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.FormViewData;

public class FormViewUpdateField
{
    [JsonPropertyName("LeftField")]
    public string? LeftField { get; set; }

    [JsonPropertyName("RightField")]
    public string? RightField { get; set; }

    [JsonPropertyName("RightValue")]
    public string? RightValue { get; set; }

    [JsonPropertyName("PushValueOnlyWhenTargetEmpty")]
    public bool PushValueOnlyWhenTargetEmpty { get; set; }

    [JsonPropertyName("PreserveUserEdits")]
    public bool PreserveUserEdits { get; set; }
}
