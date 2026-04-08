using System.Text.Json.Serialization;

namespace Application.Features.Authentication.DTOs;

/// <summary>
/// Request DTO for GetUserData API endpoint.
/// Sent to Ivanti API to retrieve user information.
/// </summary>
public class GetUserDataRequest
{
    [JsonPropertyName("tzoffset")]
    public int? TimeZoneOffset { get; init; }

    [JsonPropertyName("_csrfToken")]
    public string? CsrfToken { get; init; }
}
