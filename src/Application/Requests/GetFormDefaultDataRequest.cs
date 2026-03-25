using System.Text.Json.Serialization;

namespace Application.Requests;

/// <summary>
/// Request DTO for FormDefaultData API endpoint.
/// Sent to Ivanti API to retrieve default form values and configuration.
/// </summary>
public class GetFormDefaultDataRequest
{
    [JsonPropertyName("dependentInfo")]
    public object? DependentInfo { get; set; }

    [JsonPropertyName("formName")]
    public string? FormName { get; set; }

    [JsonPropertyName("layoutName")]
    public string? LayoutName { get; set; }

    [JsonPropertyName("objectId")]
    public string? ObjectId { get; set; }

    [JsonPropertyName("objectType")]
    public string? ObjectType { get; set; }



    [JsonPropertyName("masterData")]
    public string? MasterData { get; set; } = null;

    [JsonPropertyName("overridings")]
    public object? Overridings { get; set; } = null;

    [JsonPropertyName("viewName")]
    public string? ViewName { get; set; }

    [JsonPropertyName("_csrfToken")]
    public string? CsrfToken { get; set; }
}
