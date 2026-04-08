using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.FormViewData;

public class FormViewData
{
    [JsonPropertyName("__type")]
    public string? Type { get; set; }

    [JsonPropertyName("viewName")]
    public string? ViewName { get; set; }

    [JsonPropertyName("formDef")]
    public FormDefinition? FormDef { get; set; }
}
