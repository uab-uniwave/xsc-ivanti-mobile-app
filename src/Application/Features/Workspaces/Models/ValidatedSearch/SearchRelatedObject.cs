using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.ValidatedSearch;

public class SearchRelatedObject
{
    [JsonPropertyName("ID")]
    public string? ID { get; set; }

    [JsonPropertyName("ObjectId")]
    public string? ObjectId { get; set; }

    [JsonPropertyName("Name")]
    public string? Name { get; set; }

    [JsonPropertyName("Style")]
    public string? Style { get; set; }

    [JsonPropertyName("ThereCardinality")]
    public string? ThereCardinality { get; set; }
}
