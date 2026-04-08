using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.FormViewData;

public class FormViewMeta
{
    [JsonPropertyName("Cells")]
    public List<FormViewCell> Cells { get; set; } = new();

    [JsonPropertyName("Controls")]
    public Dictionary<string, FormViewControl> Controls { get; set; } = new();

    [JsonPropertyName("DisplayName")]
    public string? DisplayName { get; set; }

    [JsonPropertyName("Name")]
    public string? Name { get; set; }

    [JsonPropertyName("TableRef")]
    public string? TableRef { get; set; }
}
