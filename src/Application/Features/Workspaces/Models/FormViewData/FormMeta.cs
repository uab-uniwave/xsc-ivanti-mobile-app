using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.FormViewData;

public class FormMeta
{
    [JsonPropertyName("Cells")]
    public List<FormCell> Cells { get; set; } = new();

    [JsonPropertyName("Controls")]
    public Dictionary<string, FormControl> Controls { get; set; } = new();

    [JsonPropertyName("DisplayName")]
    public string? DisplayName { get; set; }

    [JsonPropertyName("Name")]
    public string? Name { get; set; }

    [JsonPropertyName("TableRef")]
    public string? TableRef { get; set; }
}
