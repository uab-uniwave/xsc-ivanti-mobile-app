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
    /// The tenant identifier extracted from the login page.
    /// </summary>
    [JsonPropertyName("tenant")]
    public string? Tenant { get; set; }

    /// <summary>
    /// The tenant URL extracted from the login page.
    /// </summary>
    [JsonPropertyName("tenantUrl")]
    public string? TenantUrl { get; set; }

    /// <summary>
    /// URL for selecting role after login.
    /// </summary>
    [JsonPropertyName("selectRoleUrl")]
    public string? SelectRoleUrl { get; set; }

    /// <summary>
    /// URL for login form submission.
    /// </summary>
    [JsonPropertyName("loginUrl")]
    public string? LoginUrl { get; set; }

    /// <summary>
    /// URL for password reset.
    /// </summary>
    [JsonPropertyName("resetPasswordUrl")]
    public string? ResetPasswordUrl { get; set; }

    /// <summary>
    /// URL for biometric authentication.
    /// </summary>
    [JsonPropertyName("authenticateBioMetricUrl")]
    public string? AuthenticateBioMetricUrl { get; set; }

    /// <summary>
    /// Return URL from login model.
    /// </summary>
    [JsonPropertyName("modelReturnUrl")]
    public string? ModelReturnUrl { get; set; }

    /// <summary>
    /// Whether the URL is shared by multiple tenants.
    /// </summary>
    [JsonPropertyName("isUrlSharedByTenants")]
    public bool IsUrlSharedByTenants { get; set; }

    /// <summary>
    /// Client timezone offset in minutes.
    /// </summary>
    [JsonPropertyName("clientTimeOffset")]
    public int ClientTimeOffset { get; set; }

    /// <summary>
    /// Client timezone name (e.g., "Europe/Kiev").
    /// </summary>
    [JsonPropertyName("clientTimezoneName")]
    public string? ClientTimezoneName { get; set; }

    /// <summary>
    /// Return URL after authentication.
    /// </summary>
    [JsonPropertyName("returnUrl")]
    public string? ReturnUrl { get; set; }

    /// <summary>
    /// Preferred role for automatic selection.
    /// </summary>
    [JsonPropertyName("preferredRole")]
    public string? PreferredRole { get; set; }

    /// <summary>
    /// Whether forgot password functionality is allowed.
    /// </summary>
    [JsonPropertyName("isForgotPasswordAllowed")]
    public bool IsForgotPasswordAllowed { get; set; }

    /// <summary>
    /// Whether the login is rendered in an iframe.
    /// </summary>
    [JsonPropertyName("isFrame")]
    public bool IsFrame { get; set; }

    /// <summary>
    /// OpenID sign-in configuration.
    /// </summary>
    [JsonPropertyName("openIdSignIn")]
    public string? OpenIdSignIn { get; set; }

    /// <summary>
    /// SSO return URL.
    /// </summary>
    [JsonPropertyName("ssoReturnUrl")]
    public string? SsoReturnUrl { get; set; }

    /// <summary>
    /// URL for login styles generator.
    /// </summary>
    [JsonPropertyName("loginStylesGeneratorUrl")]
    public string? LoginStylesGeneratorUrl { get; set; }
}
