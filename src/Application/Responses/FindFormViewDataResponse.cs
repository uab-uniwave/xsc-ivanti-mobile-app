using System.Collections.Generic;
using System.Text.Json.Serialization;
using Application.Dtos;
using Application.DTOs;

namespace Application.Responses;

/// <summary>
/// Response DTO for FindFormViewData API endpoint.
/// Contains form view structure, layout configuration, and form definitions.
/// </summary>
public class FindFormViewDataResponse
{
    [JsonPropertyName("d")]
    public FormViewDataDto D { get; set; } = new();
}
