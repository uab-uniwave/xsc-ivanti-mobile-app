using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Application.DTOs
{
    public sealed class RoleWorkspacesDto
    {
        [JsonPropertyName("__type")]
        public string? Type { get; set; }

        [JsonPropertyName("Workspaces")]
        public List<WorkspaceDto> Workspaces { get; set; } = new();

        [JsonPropertyName("RecentWorkspaces")]
        public List<string> RecentWorkspaces { get; set; } = new();

        [JsonPropertyName("AllWorkspaces")]
        public List<WorkspaceDto> AllWorkspaces { get; set; } = new();

        [JsonPropertyName("MobileWorkspaces")]
        public List<WorkspaceDto> MobileWorkspaces { get; set; } = new();

        [JsonPropertyName("Notifications")]
        public RoleWorkspaceNotificationsDto Notifications { get; set; }

        [JsonPropertyName("BrandingOptions")]
        public RoleWorkspaceBrandingOptionsDto? BrandingOptions { get; set; }

        [JsonPropertyName("AllowClick2Talk")]
        public bool AllowClick2Talk { get; set; }

        [JsonPropertyName("HelpLinks")]
        public List<RoleWorkspaceHelpLinkDto> HelpLinks { get; set; } = new();

        [JsonPropertyName("ChatEnabled")]
        public bool ChatEnabled { get; set; }

        [JsonPropertyName("MSTeamsIntegrationEnabled")]
        public bool MSTeamsIntegrationEnabled { get; set; }
    }

    public sealed class WorkspaceDto
    {
        [JsonPropertyName("ID")]
        public string Id { get; set; } = default!;

        [JsonPropertyName("LayoutName")]
        public string? LayoutName { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; } = default!;

        [JsonPropertyName("Behavior")]
        public string? Behavior { get; set; }

        [JsonPropertyName("Profile")]
        public string? Profile { get; set; }

        [JsonPropertyName("Link")]
        public string? Link { get; set; }

        [JsonPropertyName("Module")]
        public string? Module { get; set; }

        [JsonPropertyName("RecentList")]
        public bool RecentList { get; set; }

        [JsonPropertyName("Default")]
        public bool Default { get; set; }

        [JsonPropertyName("Visible")]
        public bool Visible { get; set; }

        [JsonPropertyName("VisibleInMainMenu")]
        public bool VisibleInMainMenu { get; set; }

        [JsonPropertyName("Closable")]
        public bool Closable { get; set; }

        [JsonPropertyName("Searchable")]
        public bool Searchable { get; set; }

        [JsonPropertyName("AdminRoleRequired")]
        public bool AdminRoleRequired { get; set; }

        [JsonPropertyName("Configuration")]
        public RoleWorkspaceConfigurationDto? Configuration { get; set; }

        [JsonPropertyName("HiddenExpression")]
        public string? HiddenExpression { get; set; }

        [JsonPropertyName("isUIV3")]
        public bool IsUiV3 { get; set; }
    }

    public sealed class RoleWorkspaceConfigurationDto
    {
        [JsonPropertyName("objectName")]
        public string? ObjectName { get; set; }

        [JsonPropertyName("objectRef")]
        public string? ObjectRef { get; set; }

        [JsonPropertyName("layout")]
        public string? Layout { get; set; }
    }

    public sealed class RoleWorkspaceNotificationsDto
    {
        [JsonPropertyName("MaintenanceNotification")]
        public string? MaintenanceNotification { get; set; }

        [JsonPropertyName("ReleaseNotification")]
        public string? ReleaseNotification { get; set; }

        [JsonPropertyName("AllReleaseNotification")]
        public string? AllReleaseNotification { get; set; }
    }

    public sealed class RoleWorkspaceBrandingOptionsDto
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
        public RoleWorkspaceSystemMenuOptionsDto? SystemMenuOptions { get; set; }

        [JsonPropertyName("TopLinks")]
        public List<RoleWorkspaceLinkDto> TopLinks { get; set; } = new();

        [JsonPropertyName("BottomLinks")]
        public List<RoleWorkspaceLinkDto> BottomLinks { get; set; } = new();

        [JsonPropertyName("SelectorOptions")]
        public RoleWorkspaceSelectorOptionsDto? SelectorOptions { get; set; }

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

    public sealed class RoleWorkspaceSystemMenuOptionsDto
    {
        [JsonPropertyName("EnableLogout")]
        public bool EnableLogout { get; set; }

        [JsonPropertyName("EnableChangePassword")]
        public bool EnableChangePassword { get; set; }

        [JsonPropertyName("EnableChatAsAnalyst")]
        public bool EnableChatAsAnalyst { get; set; }

        [JsonPropertyName("EnableChatAsUser")]
        public bool EnableChatAsUser { get; set; }

        [JsonPropertyName("EnableEditProfile")]
        public bool EnableEditProfile { get; set; }

        [JsonPropertyName("EnableHelp")]
        public bool EnableHelp { get; set; }

        [JsonPropertyName("EnableChangeRole")]
        public bool EnableChangeRole { get; set; }
    }

    public sealed class RoleWorkspaceSelectorOptionsDto
    {
        [JsonPropertyName("NewWindow")]
        public bool NewWindow { get; set; }

        [JsonPropertyName("SelectorMenu")]
        public bool SelectorMenu { get; set; }

        [JsonPropertyName("SelectorMenuOther")]
        public bool SelectorMenuOther { get; set; }
    }

    public sealed class RoleWorkspaceHelpLinkDto
    {
        [JsonPropertyName("Category")]
        public string? Category { get; set; }

        [JsonPropertyName("DisplayName")]
        public string? DisplayName { get; set; }

        [JsonPropertyName("Editable")]
        public bool Editable { get; set; }

        [JsonPropertyName("HelpEnable")]
        public bool HelpEnable { get; set; }

        [JsonPropertyName("Language")]
        public string? Language { get; set; }

        [JsonPropertyName("URL")]
        public string? Url { get; set; }
    }

    public sealed class RoleWorkspaceLinkDto
    {
        [JsonPropertyName("DisplayName")]
        public string? DisplayName { get; set; }

        [JsonPropertyName("URL")]
        public string? Url { get; set; }
    }
}
