using System.Collections.Generic;
using System.Text.Json.Serialization;
using Application.Features.Workspaces.Models.FormDefaultData;

namespace Application.Features.Workspaces.DTOs;

/// <summary>
/// Response  for FormDefaultData API endpoint.
/// Contains form definition, default values, and data model.
/// </summary>
public class GetFormDefaultDataResponse
{
    [JsonPropertyName("d")]
    public FormDefaultData D { get; set; } = new();
}
