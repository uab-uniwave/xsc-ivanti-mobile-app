using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.FormViewData;

public class ForeignKeyInfo
{
    [JsonPropertyName("PKField")]
    public string? PKField { get; set; }

    [JsonPropertyName("FKField")]
    public string? FKField { get; set; }

    [JsonPropertyName("FKCategoryField")]
    public string? FKCategoryField { get; set; }
}
