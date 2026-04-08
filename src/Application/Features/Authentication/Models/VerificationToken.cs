using System.Text.Json.Serialization;

namespace Application.Features.Authentication.Models;

/// <summary>
/// Represents an anti-forgery verification token extracted from the Ivanti login page.
/// This token is required for form-based authentication.
/// </summary>
public class VerificationToken
{
    /// <summary>
    /// The __RequestVerificationToken value from the login form.
    /// </summary>
    [JsonPropertyName("token")]
    public string Token { get; set; } = string.Empty;

    /// <summary>
    /// Cookies that should be stored for subsequent requests.
    /// </summary>
    [JsonPropertyName("cookies")]
    public Dictionary<string, string> Cookies { get; set; } = new();

    /// <summary>
    /// The tenant URL extracted from the login page.
    /// </summary>
    [JsonPropertyName("tenant")]
    public string? Tenant { get; set; }
}
