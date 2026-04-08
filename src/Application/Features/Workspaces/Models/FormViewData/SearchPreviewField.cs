using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.FormViewData;

public class SearchPreviewField
{
    [JsonPropertyName("FieldRef")]
    public string? FieldRef { get; set; }

    [JsonPropertyName("DisplayName")]
    public string? DisplayName { get; set; }
}
