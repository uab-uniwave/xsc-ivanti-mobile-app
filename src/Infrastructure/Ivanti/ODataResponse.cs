using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Infrastructure.Ivanti;

/// <summary>
/// Generic OData response wrapper for API responses.
/// </summary>
public class ODataResponse<T>
{
    [JsonPropertyName("value")]
    public List<T> Value { get; set; } = new();

    [JsonPropertyName("@odata.count")]
    public int Count { get; set; }

    [JsonPropertyName("@odata.nextLink")]
    public string? NextLink { get; set; }
}
