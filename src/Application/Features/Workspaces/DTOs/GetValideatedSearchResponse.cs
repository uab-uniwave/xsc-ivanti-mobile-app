using Application.Features.Workspaces.Models.ValidatedSearch;
using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.DTOs;

/// <summary>
/// Response for GetValidatedSearch API endpoint.
/// Received from Ivanti API containing validated search definition and conditions.
/// </summary>
public class GetValideatedSearchResponse    
{
    [JsonPropertyName("d")]
    public ValidatedSearch D { get; set; } = new();
}
