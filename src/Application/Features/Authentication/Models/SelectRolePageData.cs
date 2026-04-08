using System.Text.Json.Serialization;

namespace Application.Features.Authentication.Models;

/// <summary>
/// Represents the data extracted from the SelectRole HTML page.
/// Contains available roles and tokens for role selection.
/// </summary>
public class SelectRolePageData
{
    /// <summary>
    /// The __RequestVerificationToken for the SelectRole form.
    /// </summary>
    [JsonPropertyName("verificationToken")]
    public string VerificationToken { get; set; } = string.Empty;

    /// <summary>
    /// List of available roles for the authenticated user.
    /// </summary>
    [JsonPropertyName("availableRoles")]
    public List<AvailableRole> AvailableRoles { get; set; } = new();

    /// <summary>
    /// The currently selected role ID.
    /// </summary>
    [JsonPropertyName("selectedRoleId")]
    public string? SelectedRoleId { get; set; }

    /// <summary>
    /// Return URL after role selection.
    /// </summary>
    [JsonPropertyName("returnUrl")]
    public string? ReturnUrl { get; set; }

    /// <summary>
    /// Cookies received from the login response.
    /// </summary>
    [JsonPropertyName("cookies")]
    public Dictionary<string, string> Cookies { get; set; } = new();

    /// <summary>
    /// The form action URL for role selection.
    /// </summary>
    [JsonPropertyName("formActionUrl")]
    public string FormActionUrl { get; set; } = "/HEAT/Account/SelectRole";
}
