using System.Collections.Generic;
using System.Text.Json.Serialization;
using Application.DTOs;

namespace Application.Responses;

/// <summary>
/// Response  for FormDefaultData API endpoint.
/// Contains form definition, default values, and data model.
/// </summary>
public class GetFormDefaultDataResponse
{
    [JsonPropertyName("d")]
    public FormDefaultDataDto D { get; set; } = new();
}
