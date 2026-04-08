using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace Application.Common.Models.UserData;



public class UserData
{
    [JsonPropertyName("__type")]
    public string? Type { get; init; }

    [JsonPropertyName("UserTeamList")]
    public List<string> UserTeamList { get; init; } = new();

    [JsonPropertyName("SingleRoleUser")]
    public bool SingleRoleUser { get; init; }

    [JsonPropertyName("FirstName")]
    public string? FirstName { get; init; }

    [JsonPropertyName("MiddleName")]
    public string? MiddleName { get; init; }

    [JsonPropertyName("LastName")]
    public string? LastName { get; init; }

    [JsonPropertyName("DisplayName")]
    public string? DisplayName { get; init; }

    [JsonPropertyName("PrimaryEmail")]
    public string? PrimaryEmail { get; init; }

    [JsonPropertyName("PrimaryPhone")]
    public string? PrimaryPhone { get; init; }

    [JsonPropertyName("Status")]
    public string? Status { get; init; }

    [JsonPropertyName("OwnerTeam")]
    public string? OwnerTeam { get; init; }

    [JsonPropertyName("OrgUnitName")]
    public string? OrgUnitName { get; init; }

    [JsonPropertyName("OrgUnitId")]
    public string? OrgUnitId { get; init; }

    [JsonPropertyName("CurrencyCode")]
    public string? CurrencyCode { get; init; }

    [JsonPropertyName("CurrencySign")]
    public string? CurrencySign { get; init; }

    [JsonPropertyName("DisplayCurrencyName")]
    public bool DisplayCurrencyName { get; init; }

    [JsonPropertyName("UserTeam")]
    public string? UserTeam { get; init; }

    [JsonPropertyName("UserRole")]
    public string? UserRole { get; init; }

    [JsonPropertyName("SystemAccessRights")]
    public SystemAccessRights? SystemAccessRights { get; init; }

    [JsonPropertyName("Features")]
    public Features? Features { get; init; }

    [JsonPropertyName("userRoleList")]
    public List<UserRole> UserRoleList { get; init; } = new();

    [JsonPropertyName("TenantDisabledModules")]
    public List<object> TenantDisabledModules { get; init; } = new();

    // ⚠️ VERY LARGE dynamic object → keep flexible
    [JsonPropertyName("ObjectFields")]
    public Dictionary<string, object?> ObjectFields { get; init; } = new();

    // ⚠️ Key-value config → dictionary is BEST choice
    [JsonPropertyName("GlobalConstants")]
    public Dictionary<string, string?> GlobalConstants { get; init; } = new();

    [JsonPropertyName("IsSuperAdmin")]
    public bool IsSuperAdmin { get; init; }

    [JsonPropertyName("AnalystLOB")]
    public string? AnalystLOB { get; init; }


}



public class SystemAccessRights
{
    public int AccessSecurityGroups { get; init; }
    public int AccessUsers { get; init; }
    public int AccessBusinessUnits { get; init; }
    public int AccessApplicationTypes { get; init; }
    public int CreateDefinitionSet { get; init; }
    public int OpenDefinitionSet { get; init; }
    public int RegisterModule { get; init; }
    public int ApplyDefinitionSet { get; init; }
    public int ImportData { get; init; }
    public int ExportData { get; init; }
    public int HierarchicalDataManager { get; init; }
    public int RunAdmin { get; init; }
    public int NotificationSettings { get; init; }
    public int ServerSettings { get; init; }
}

public class Features
{
    public int UseBatchPriceCalculation { get; init; }
    public int EnableAdvancedWebService { get; init; }
    public int EnableGlobalSearch { get; init; }
    public int EnableAIGuardRail { get; init; }
    public int EnableOneNavUI { get; init; }
    public int EnableAITeamsBot { get; init; }
    public int EnableExternalIdentityServerLogin { get; init; }
    public int EnhancedSearch { get; init; }
    public int ActionServiceUsage { get; init; }
}
public class UserRole
{
    [JsonPropertyName("__type")]
    public string? Type { get; init; }

    public string? RecId { get; init; }

    public bool EnableNewUI { get; init; }

    public bool SelfServiceRole { get; init; }

    public bool EnableMobileAnalystUI { get; init; }

    public string? Name { get; init; }

    public string? DisplayName { get; init; }
}