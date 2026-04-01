using System.Text.Json.Serialization;
using Application.Models.FormValidationListData;

namespace Application.Responses;

/// <summary>
/// Response for FormValidationListData API endpoint.
/// Contains form field validation data with dropdown/lookup information.
/// </summary>
public class FormValidationListDataResponse
{
    [JsonPropertyName("d")]
    public FormValidationListData D { get; set; } = new();
}
