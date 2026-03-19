using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

public class SessionResponse
{
    [JsonPropertyName("d")]
    public SessionData? Data { get; set; }
}

public class SessionData
{
    [JsonPropertyName("__type")]
    public string? Type { get; set; }

    [JsonPropertyName("ActiveRole")]
    public string? ActiveRole { get; set; }

    [JsonPropertyName("ActiveRoleDisplayName")]
    public string? ActiveRoleDisplayName { get; set; }

    [JsonPropertyName("AppName")]
    public string? AppName { get; set; }

    [JsonPropertyName("Authorized")]
    public bool Authorized { get; set; }

    [JsonPropertyName("ConnectionName")]
    public string? ConnectionName { get; set; }

    [JsonPropertyName("RoleAssigned")]
    public bool RoleAssigned { get; set; }

    [JsonPropertyName("SessionLength")]
    public int SessionLength { get; set; }

    [JsonPropertyName("SessionStarted")]
    public string? SessionStarted { get; set; }

    [JsonPropertyName("UserName")]
    public string? UserName { get; set; }

    [JsonPropertyName("IsSuperAdmin")]
    public bool IsSuperAdmin { get; set; }

    [JsonPropertyName("ImageManagerUseDB")]
    public bool ImageManagerUseDB { get; set; }

    [JsonPropertyName("Actions")]
    public List<string> Actions { get; set; } = new();

    [JsonPropertyName("IsFRSAdmin")]
    public bool IsFRSAdmin { get; set; }

    [JsonPropertyName("IPCMKey1")]
    public string? IPCMKey1 { get; set; }

    [JsonPropertyName("IPCMKey2")]
    public string? IPCMKey2 { get; set; }

    [JsonPropertyName("SessionCsrfToken")]
    public string? SessionCsrfToken { get; set; }

    [JsonPropertyName("BuildVersion")]
    public string? BuildVersion { get; set; }

    [JsonPropertyName("IsOnPremise")]
    public bool IsOnPremise { get; set; }

    [JsonPropertyName("IsMobileUI")]
    public bool IsMobileUI { get; set; }

    [JsonPropertyName("IsResponsiveAnalystUI")]
    public bool IsResponsiveAnalystUI { get; set; }

    [JsonPropertyName("IsAnonymous")]
    public bool IsAnonymous { get; set; }

    [JsonPropertyName("SelfServiceSocialIT")]
    public bool SelfServiceSocialIT { get; set; }

    [JsonPropertyName("HasMultipleRoles")]
    public bool HasMultipleRoles { get; set; }

    [JsonPropertyName("IsMetaReadOnly")]
    public bool IsMetaReadOnly { get; set; }

    [JsonPropertyName("MetaReadOnlyExclusionList")]
    public List<string> MetaReadOnlyExclusionList { get; set; } = new();

    [JsonPropertyName("AvailableLanguages")]
    public List<AvailableLanguage> AvailableLanguages { get; set; } = new();

    [JsonPropertyName("AvailableLanguagePacks")]
    public JsonElement? AvailableLanguagePacks { get; set; }

    [JsonPropertyName("TimezoneName")]
    public string? TimezoneName { get; set; }

    [JsonPropertyName("SessionKey")]
    public string? SessionKey { get; set; }

    [JsonPropertyName("ClientLogLevel")]
    public string? ClientLogLevel { get; set; }

    [JsonPropertyName("updateObjectMaxRow")]
    public string? UpdateObjectMaxRow { get; set; }

    [JsonPropertyName("LogoutOnSessionExpiry")]
    public bool LogoutOnSessionExpiry { get; set; }

    [JsonPropertyName("IsFedRamp")]
    public bool IsFedRamp { get; set; }

    [JsonPropertyName("UseRoleCloningFeature")]
    public bool UseRoleCloningFeature { get; set; }

    [JsonPropertyName("AppSettings")]
    public Dictionary<string, string?> AppSettings { get; set; } = new();

    [JsonPropertyName("UnoTenantUrl")]
    public string? UnoTenantUrl { get; set; }

    [JsonPropertyName("LinkedTenants")]
    public JsonElement? LinkedTenants { get; set; }

    [JsonPropertyName("AccessToken")]
    public string? AccessToken { get; set; }

    [JsonPropertyName("UEMBaseURL")]
    public string? UEMBaseURL { get; set; }

    [JsonPropertyName("LandscapeName")]
    public string? LandscapeName { get; set; }

    [JsonPropertyName("IsWorkflowPriorityEnabled")]
    public bool IsWorkflowPriorityEnabled { get; set; }
}

public class AvailableLanguage
{
    [JsonPropertyName("Name")]
    public string? Name { get; set; }

    [JsonPropertyName("DisplayName")]
    public string? DisplayName { get; set; }
}