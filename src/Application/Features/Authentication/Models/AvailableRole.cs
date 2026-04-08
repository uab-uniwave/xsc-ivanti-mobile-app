using System.Text.Json.Serialization;

namespace Application.Features.Authentication.Models;

/// <summary>
/// Represents an available role that can be selected after login.
/// Extracted from the SelectRole HTML page.
/// </summary>
public class AvailableRole
{
    /// <summary>
    /// The role identifier (e.g., "Admin", "ResponsiveAnalyst").
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// The display name of the role (e.g., "Administrator", "Mobile Analyst").
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Whether this role is an analyst role.
    /// </summary>
    [JsonPropertyName("isAnalyst")]
    public bool IsAnalyst { get; set; }

    /// <summary>
    /// Whether this role is a Self Service Mobile role.
    /// </summary>
    [JsonPropertyName("isSelfServiceMobile")]
    public bool IsSelfServiceMobile { get; set; }

    /// <summary>
    /// The index of this role in the list.
    /// </summary>
    [JsonPropertyName("index")]
    public int Index { get; set; }

    /// <summary>
    /// Whether this role is currently selected (default selection).
    /// </summary>
    [JsonPropertyName("isSelected")]
    public bool IsSelected { get; set; }
}
