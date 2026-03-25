using System.Collections.Generic;
using System.Text.Json.Serialization;
using Application.DTOs;

namespace Application.Responses;

/// <summary>
/// Response  for Session.Initialize API endpoint.
/// Received from Ivanti API after session initialization.
/// Contains session metadata and authentication tokens.
/// </summary>
public class InitializeSessionResponse
{
    [JsonPropertyName("d")]
    public SessionDataDto D { get; set; } = new();
}




