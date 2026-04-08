using System.Text.Json.Serialization;

namespace Application.Features.Authentication.DTOs;

/// <summary>
/// Request DTO for authenticating a user.
/// Contains credentials and verification token for form-based authentication.
/// </summary>
public class LoginRequest
{
    [JsonPropertyName("__RequestVerificationToken")]
    public string VerificationToken { get; set; } = string.Empty;

    [JsonPropertyName("UserName")]
    public string Username { get; set; } = string.Empty;

    [JsonPropertyName("Password")]
    public string Password { get; set; } = string.Empty;

    [JsonPropertyName("EnableBiometric")]
    public string EnableBiometric { get; set; } = "false";

    [JsonPropertyName("Tenant")]
    public string? Tenant { get; set; }

    [JsonPropertyName("IsUrlSharedByTenants")]
    public string IsUrlSharedByTenants { get; set; } = "False";

    [JsonPropertyName("ClientTimeOffset")]
    public string ClientTimeOffset { get; set; } = "0";

    [JsonPropertyName("ClientTimezoneName")]
    public string? ClientTimezoneName { get; set; }

    [JsonPropertyName("ReturnUrl")]
    public string ReturnUrl { get; set; } = string.Empty;

    [JsonPropertyName("PrefferedRole")]
    public string PreferredRole { get; set; } = string.Empty;

    [JsonPropertyName("IsForgotPasswordAllowed")]
    public string IsForgotPasswordAllowed { get; set; } = "False";

    [JsonPropertyName("IsFrame")]
    public string IsFrame { get; set; } = "False";

    [JsonPropertyName("OpenIDSignIn")]
    public string OpenIdSignIn { get; set; } = string.Empty;

    [JsonPropertyName("SsoReturnUrl")]
    public string SsoReturnUrl { get; set; } = string.Empty;
}
