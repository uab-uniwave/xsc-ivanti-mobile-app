using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.FormViewData;

public class LinkTableInfo
{
    [JsonPropertyName("LinkTableName")]
    public string? LinkTableName { get; set; }

    [JsonPropertyName("LinkDataTableRef")]
    public string? LinkDataTableRef { get; set; }

    [JsonPropertyName("LinkDataUpNameField")]
    public string? LinkDataUpNameField { get; set; }

    [JsonPropertyName("LinkDataDownNameField")]
    public string? LinkDataDownNameField { get; set; }

    [JsonPropertyName("LinkConstraintsTableRef")]
    public string? LinkConstraintsTableRef { get; set; }
}
