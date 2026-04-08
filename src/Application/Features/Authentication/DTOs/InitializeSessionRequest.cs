using System.Text.Json.Serialization;

namespace Application.Features.Authentication.DTOs;

/// <summary>
/// Request DTO for Session.Initialize API endpoint.
/// Sent to Ivanti API to initialize a new session.
/// </summary>
public class InitializeSessionRequest
{


    [JsonPropertyName("_csrfToken")]
    public string? CsrfToken { get; set; } = null;
}
