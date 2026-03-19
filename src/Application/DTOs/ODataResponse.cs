using System.Text.Json.Serialization;

public class ODataResponse<T>
{
    [JsonPropertyName("@odata.count")]
    public int Count { get; set; }

    [JsonPropertyName("value")]
    public List<T> Value { get; set; } = new();
}