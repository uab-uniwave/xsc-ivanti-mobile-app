using System.Text.Json.Serialization;

namespace Application.Features.Authentication.DTOs;

/// <summary>
/// Request DTO for authenticating a user.
/// Contains credentials and verification token for form-based authentication.
/// </summary>
public class LoginRequest
{
    [JsonPropertyName("UserName")]
    public string Username { get; set; } = string.Empty;

    [JsonPropertyName("Password")]
    public string Password { get; set; } = string.Empty;

    [JsonPropertyName("__RequestVerificationToken")]
    public string VerificationToken { get; set; } = string.Empty;

    [JsonPropertyName("Tenant")]
    public string? Tenant { get; set; }

    [JsonPropertyName("ClientTimeOffset")]
    public int ClientTimeOffset { get; set; } = 0;

    [JsonPropertyName("ClientTimezoneName")]
    public string? ClientTimezoneName { get; set; }
}
