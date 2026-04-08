using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.FormViewData;

public class FormViewFieldLink
{
    [JsonPropertyName("LinkIdFieldRef")]
    public string? LinkIdFieldRef { get; set; }

    [JsonPropertyName("LinkCategoryFieldRef")]
    public string? LinkCategoryFieldRef { get; set; }

    [JsonPropertyName("LinkTableRef")]
    public string? LinkTableRef { get; set; }

    [JsonPropertyName("TargetType")]
    public int TargetType { get; set; }
}
