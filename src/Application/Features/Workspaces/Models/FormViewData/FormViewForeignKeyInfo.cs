using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.FormViewData;

public class FormViewForeignKeyInfo
{
    [JsonPropertyName("PKField")]
    public string? PKField { get; set; }

    [JsonPropertyName("FKField")]
    public string? FKField { get; set; }

    [JsonPropertyName("FKCategoryField")]
    public string? FKCategoryField { get; set; }
}
