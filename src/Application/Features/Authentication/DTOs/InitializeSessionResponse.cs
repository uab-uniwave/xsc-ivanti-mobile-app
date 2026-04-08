using System.Collections.Generic;
using System.Text.Json.Serialization;
using Application.Common.Models.SessonData;

namespace Application.Features.Authentication.DTOs;

/// <summary>
/// Response  for Session.Initialize API endpoint.
/// Received from Ivanti API after session initialization.
/// Contains session metadata and authentication tokens.
/// </summary>
public class InitializeSessionResponse
{
    [JsonPropertyName("d")]
    public SessionData D { get; set; } = new();
}




