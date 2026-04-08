using System.Text.Json.Serialization;
using Application.Features.Workspaces.Models.FormValidationListData;

namespace Application.Features.Workspaces.DTOs;

/// <summary>
/// Response for FormValidationListData API endpoint.
/// Contains form field validation data with dropdown/lookup information.
/// </summary>
public class GetFormValidationListDataResponse
{
    [JsonPropertyName("d")]
    public FormValidationListData D { get; set; } = new();
}
