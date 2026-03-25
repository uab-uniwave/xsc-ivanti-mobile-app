using System.Text.Json.Serialization;

namespace Application.Common;

/// <summary>
/// Wrapper structure for Ivanti API responses.
/// The Ivanti API wraps the actual response data in a "d" property.
/// </summary>
public class DResponse<T> where T : class
{
    [JsonPropertyName("d")]
    public T? Data { get; set; }
}
