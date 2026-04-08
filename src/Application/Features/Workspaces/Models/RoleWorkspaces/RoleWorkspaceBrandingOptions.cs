using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.RoleWorkspaces;

public sealed class RoleWorkspaceBrandingOptions
{
    [JsonPropertyName("BrandingID")]
    public string? BrandingId { get; set; }

    [JsonPropertyName("SkinCssName")]
    public string? SkinCssName { get; set; }

    [JsonPropertyName("SkinCssParameters")]
    public object? SkinCssParameters { get; set; }

    [JsonPropertyName("LogoImage")]
    public string? LogoImage { get; set; }

    [JsonPropertyName("SystemMenuOptions")]
    public RoleWorkspaceSystemMenuOptions? SystemMenuOptions { get; set; }

    [JsonPropertyName("TopLinks")]
    public List<RoleWorkspaceLink> TopLinks { get; set; } = new();

    [JsonPropertyName("BottomLinks")]
    public List<RoleWorkspaceLink> BottomLinks { get; set; } = new();

    [JsonPropertyName("SelectorOptions")]
    public RoleWorkspaceSelectorOptions? SelectorOptions { get; set; }

    [JsonPropertyName("EnableNewUI")]
    public bool EnableNewUI { get; set; }

    [JsonPropertyName("EnableNewSSLandingPage")]
    public bool EnableNewSSLandingPage { get; set; }

    [JsonPropertyName("SSExtendedRole")]
    public bool SsExtendedRole { get; set; }

    [JsonPropertyName("EnableMobileAnalystUI")]
    public bool EnableMobileAnalystUI { get; set; }

    [JsonPropertyName("SelfServiceRole")]
    public bool SelfServiceRole { get; set; }

    [JsonPropertyName("UiOptions")]
    public Dictionary<string, object> UiOptions { get; set; } = new();

    [JsonPropertyName("Id")]
    public string? Id { get; set; }

    [JsonPropertyName("LastModified")]
    public string? LastModified { get; set; }

    [JsonPropertyName("TenantMetaRevision")]
    public int TenantMetaRevision { get; set; }

    [JsonPropertyName("ModifiedBy")]
    public string? ModifiedBy { get; set; }

    [JsonPropertyName("Revision")]
    public string? Revision { get; set; }
}
