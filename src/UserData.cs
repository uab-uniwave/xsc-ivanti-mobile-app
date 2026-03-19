using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

public class GetUserDataResponse
{
    [JsonPropertyName("d")]
    public UserData? Data { get; set; }
}

public class UserData
{
    [JsonPropertyName("__type")]
    public string? Type { get; set; }

    [JsonPropertyName("UserTeamList")]
    public List<string> UserTeamList { get; set; } = new();

    [JsonPropertyName("SingleRoleUser")]
    public bool SingleRoleUser { get; set; }

    [JsonPropertyName("FirstName")]
    public string? FirstName { get; set; }

    [JsonPropertyName("MiddleName")]
    public string? MiddleName { get; set; }

    [JsonPropertyName("LastName")]
    public string? LastName { get; set; }

    [JsonPropertyName("DisplayName")]
    public string? DisplayName { get; set; }

    [JsonPropertyName("PrimaryAddress")]
    public string? PrimaryAddress { get; set; }

    [JsonPropertyName("PrimaryEmail")]
    public string? PrimaryEmail { get; set; }

    [JsonPropertyName("PrimaryPhone")]
    public string? PrimaryPhone { get; set; }

    [JsonPropertyName("Status")]
    public string? Status { get; set; }

    [JsonPropertyName("Title")]
    public string? Title { get; set; }

    [JsonPropertyName("Room")]
    public string? Room { get; set; }

    [JsonPropertyName("OwnerTeam")]
    public string? OwnerTeam { get; set; }

    [JsonPropertyName("OrgUnitName")]
    public string? OrgUnitName { get; set; }

    [JsonPropertyName("OrgUnitId")]
    public string? OrgUnitId { get; set; }

    [JsonPropertyName("SessionKey")]
    public string? SessionKey { get; set; }

    [JsonPropertyName("CurrencySign")]
    public string? CurrencySign { get; set; }

    [JsonPropertyName("CurrencyCode")]
    public string? CurrencyCode { get; set; }

    [JsonPropertyName("CurrencyCodeId")]
    public string? CurrencyCodeId { get; set; }

    [JsonPropertyName("DisplayCurrencyName")]
    public bool DisplayCurrencyName { get; set; }

    [JsonPropertyName("HasInternalAuth")]
    public bool HasInternalAuth { get; set; }

    [JsonPropertyName("SSOUrl")]
    public string? SSOUrl { get; set; }

    [JsonPropertyName("PhotoRevision")]
    public int PhotoRevision { get; set; }

    [JsonPropertyName("CanChangePicture")]
    public bool CanChangePicture { get; set; }

    [JsonPropertyName("SsoProviderName")]
    public string? SsoProviderName { get; set; }

    [JsonPropertyName("SsoLoginId")]
    public string? SsoLoginId { get; set; }

    [JsonPropertyName("DisabledChildTabPlugins")]
    public List<string> DisabledChildTabPlugins { get; set; } = new();

    [JsonPropertyName("IdentityObject")]
    public string? IdentityObject { get; set; }

    [JsonPropertyName("IsIdentityServerEnabled")]
    public bool IsIdentityServerEnabled { get; set; }

    [JsonPropertyName("UserTeam")]
    public string? UserTeam { get; set; }

    [JsonPropertyName("UserRole")]
    public string? UserRole { get; set; }

    [JsonPropertyName("SystemAccessRights")]
    public Dictionary<string, int> SystemAccessRights { get; set; } = new();

    [JsonPropertyName("IPCMSipUName3")]
    public string? IPCMSipUName3 { get; set; }

    [JsonPropertyName("Features")]
    public Dictionary<string, int> Features { get; set; } = new();

    [JsonPropertyName("userRoleList")]
    public List<UserRoleInfo> UserRoleList { get; set; } = new();

    [JsonPropertyName("TenantDisabledModules")]
    public List<string> TenantDisabledModules { get; set; } = new();

    [JsonPropertyName("ObjectFields")]
    public UserObjectFields? ObjectFields { get; set; }

    [JsonPropertyName("GlobalConstants")]
    public Dictionary<string, JsonElement> GlobalConstants { get; set; } = new();

    [JsonPropertyName("TelemetryConfig")]
    public JsonElement? TelemetryConfig { get; set; }

    [JsonPropertyName("IsSuperAdmin")]
    public bool IsSuperAdmin { get; set; }

    [JsonPropertyName("AnalystLOB")]
    public string? AnalystLOB { get; set; }
}

public class UserRoleInfo
{
    [JsonPropertyName("__type")]
    public string? Type { get; set; }

    [JsonPropertyName("RecId")]
    public string? RecId { get; set; }

    [JsonPropertyName("EnableNewUI")]
    public bool EnableNewUI { get; set; }

    [JsonPropertyName("SelfServiceRole")]
    public bool SelfServiceRole { get; set; }

    [JsonPropertyName("EnableNewSSLandingPage")]
    public bool EnableNewSSLandingPage { get; set; }

    [JsonPropertyName("SSExtendedRole")]
    public bool SSExtendedRole { get; set; }

    [JsonPropertyName("EnableMobileAnalystUI")]
    public bool EnableMobileAnalystUI { get; set; }

    [JsonPropertyName("Name")]
    public string? Name { get; set; }

    [JsonPropertyName("DisplayName")]
    public string? DisplayName { get; set; }
}

public class UserObjectFields
{
    [JsonPropertyName("InitialNotReadyReasonValue")]
    public string? InitialNotReadyReasonValue { get; set; }

    [JsonPropertyName("Address1")]
    public string? Address1 { get; set; }

    [JsonPropertyName("Address1City")]
    public string? Address1City { get; set; }

    [JsonPropertyName("Address1Country")]
    public string? Address1Country { get; set; }

    [JsonPropertyName("Address1State")]
    public string? Address1State { get; set; }

    [JsonPropertyName("Address1Zip")]
    public string? Address1Zip { get; set; }

    [JsonPropertyName("Birthdate")]
    public string? Birthdate { get; set; }

    [JsonPropertyName("BusinessUnit")]
    public string? BusinessUnit { get; set; }

    [JsonPropertyName("BusinessUnitID")]
    public string? BusinessUnitID { get; set; }

    [JsonPropertyName("ContactId")]
    public string? ContactId { get; set; }

    [JsonPropertyName("CostCentre")]
    public string? CostCentre { get; set; }

    [JsonPropertyName("CreatedBy")]
    public string? CreatedBy { get; set; }

    [JsonPropertyName("CreatedDateTime")]
    public string? CreatedDateTime { get; set; }

    [JsonPropertyName("CreationMethod")]
    public string? CreationMethod { get; set; }

    [JsonPropertyName("Department")]
    public string? Department { get; set; }

    [JsonPropertyName("DepartmentCode")]
    public string? DepartmentCode { get; set; }

    [JsonPropertyName("Department_Sync")]
    public string? DepartmentSync { get; set; }

    [JsonPropertyName("Disabled")]
    public bool? Disabled { get; set; }

    [JsonPropertyName("DisplayName")]
    public string? DisplayName { get; set; }

    [JsonPropertyName("EmployeeInformation")]
    public string? EmployeeInformation { get; set; }

    [JsonPropertyName("EmployeeLocation")]
    public string? EmployeeLocation { get; set; }

    [JsonPropertyName("EmployeePhotoName")]
    public string? EmployeePhotoName { get; set; }

    [JsonPropertyName("EmployeePhotoRevision")]
    public int? EmployeePhotoRevision { get; set; }

    [JsonPropertyName("EnableIPCMIntegration")]
    public string? EnableIPCMIntegration { get; set; }

    [JsonPropertyName("FirstName")]
    public string? FirstName { get; set; }

    [JsonPropertyName("Floor")]
    public string? Floor { get; set; }

    [JsonPropertyName("HiredDate")]
    public string? HiredDate { get; set; }

    [JsonPropertyName("IPCM_AgentGroup")]
    public string? IPCMAgentGroup { get; set; }

    [JsonPropertyName("IPCM_Audited")]
    public string? IPCMAudited { get; set; }

    [JsonPropertyName("IPCM_Description")]
    public string? IPCMDescription { get; set; }

    [JsonPropertyName("IPCM_EnableIPCMUser")]
    public string? IPCMEnableIPCMUser { get; set; }

    [JsonPropertyName("IPCM_InitialAgentStatus")]
    public string? IPCMInitialAgentStatus { get; set; }

    [JsonPropertyName("IPCM_InitialNotReadyReason")]
    public string? IPCMInitialNotReadyReason { get; set; }

    [JsonPropertyName("IPCM_NotReadyRequired")]
    public string? IPCMNotReadyRequired { get; set; }

    [JsonPropertyName("IPCM_OverrideDN")]
    public string? IPCMOverrideDN { get; set; }

    [JsonPropertyName("IPCM_RecordingPct")]
    public string? IPCMRecordingPct { get; set; }

    [JsonPropertyName("IPCM_SearchableByName")]
    public string? IPCMSearchableByName { get; set; }

    [JsonPropertyName("IPCM_UILanguage")]
    public string? IPCMUILanguage { get; set; }

    [JsonPropertyName("IPCM_VOIPLink")]
    public string? IPCMVOIPLink { get; set; }

    [JsonPropertyName("IPCM_VoiceAgent")]
    public string? IPCMVoiceAgent { get; set; }

    [JsonPropertyName("IPCM_VoiceSupervisor")]
    public string? IPCMVoiceSupervisor { get; set; }

    [JsonPropertyName("IPCM_WrapupSeconds")]
    public string? IPCMWrapupSeconds { get; set; }

    [JsonPropertyName("IPCM_WrapupTimeout")]
    public string? IPCMWrapupTimeout { get; set; }

    [JsonPropertyName("IVRPinCode")]
    public string? IVRPinCode { get; set; }

    [JsonPropertyName("IdentityId")]
    public string? IdentityId { get; set; }

    [JsonPropertyName("InternalPwdDateTime")]
    public string? InternalPwdDateTime { get; set; }

    [JsonPropertyName("IsAutoProvisioned")]
    public string? IsAutoProvisioned { get; set; }

    [JsonPropertyName("IsExternalAuth")]
    public bool? IsExternalAuth { get; set; }

    [JsonPropertyName("IsInternalAuth")]
    public bool? IsInternalAuth { get; set; }

    [JsonPropertyName("IsNamedUser")]
    public bool? IsNamedUser { get; set; }

    [JsonPropertyName("LastExternalLoginId")]
    public string? LastExternalLoginId { get; set; }

    [JsonPropertyName("LastModBy")]
    public string? LastModBy { get; set; }

    [JsonPropertyName("LastModDateTime")]
    public string? LastModDateTime { get; set; }

    [JsonPropertyName("LastName")]
    public string? LastName { get; set; }

    [JsonPropertyName("LoginID")]
    public string? LoginID { get; set; }

    [JsonPropertyName("ManagerEmail")]
    public string? ManagerEmail { get; set; }

    [JsonPropertyName("ManagerLink")]
    public string? ManagerLink { get; set; }

    [JsonPropertyName("MiddleName")]
    public string? MiddleName { get; set; }

    [JsonPropertyName("NetworkUserName")]
    public string? NetworkUserName { get; set; }

    [JsonPropertyName("NotificationLink")]
    public string? NotificationLink { get; set; }

    [JsonPropertyName("OrgUnitLink")]
    public string? OrgUnitLink { get; set; }

    [JsonPropertyName("EntityLink")]
    public string? EntityLink { get; set; }

    [JsonPropertyName("OrganizationalUnit")]
    public string? OrganizationalUnit { get; set; }

    [JsonPropertyName("Owner")]
    public string? Owner { get; set; }

    [JsonPropertyName("ParentLink")]
    public string? ParentLink { get; set; }

    [JsonPropertyName("PasswordExpiration")]
    public bool? PasswordExpiration { get; set; }

    [JsonPropertyName("PrimaryPhone")]
    public string? PrimaryPhone { get; set; }

    [JsonPropertyName("Phone1")]
    public string? Phone1 { get; set; }

    [JsonPropertyName("Phone2")]
    public string? Phone2 { get; set; }

    [JsonPropertyName("Prefix")]
    public string? Prefix { get; set; }

    [JsonPropertyName("PrimaryEmail")]
    public string? PrimaryEmail { get; set; }

    [JsonPropertyName("ProfileID")]
    public string? ProfileID { get; set; }

    [JsonPropertyName("ReadOnly")]
    public string? ReadOnly { get; set; }

    [JsonPropertyName("RecId")]
    public string? RecId { get; set; }

    [JsonPropertyName("RegionLink")]
    public string? RegionLink { get; set; }

    [JsonPropertyName("Room")]
    public string? Room { get; set; }

    [JsonPropertyName("Status")]
    public string? Status { get; set; }

    [JsonPropertyName("Suffix")]
    public string? Suffix { get; set; }

    [JsonPropertyName("Supervisor")]
    public string? Supervisor { get; set; }

    [JsonPropertyName("Team")]
    public string? Team { get; set; }

    [JsonPropertyName("TeamEmail")]
    public string? TeamEmail { get; set; }

    [JsonPropertyName("TeamManagerEmail")]
    public string? TeamManagerEmail { get; set; }

    [JsonPropertyName("TempPwdDatetime")]
    public string? TempPwdDatetime { get; set; }

    [JsonPropertyName("TerminatedDate")]
    public string? TerminatedDate { get; set; }

    [JsonPropertyName("Title")]
    public string? Title { get; set; }

    [JsonPropertyName("Title_Sync")]
    public string? TitleSync { get; set; }

    [JsonPropertyName("VIP")]
    public bool? VIP { get; set; }

    [JsonPropertyName("ReportedByLink")]
    public string? ReportedByLink { get; set; }

    [JsonPropertyName("LocationLink")]
    public string? LocationLink { get; set; }

    [JsonPropertyName("DefaultChargingAccount")]
    public string? DefaultChargingAccount { get; set; }

    [JsonPropertyName("DN")]
    public string? DN { get; set; }

    [JsonPropertyName("Language")]
    public string? Language { get; set; }

    [JsonPropertyName("RemoteControlUID")]
    public string? RemoteControlUID { get; set; }

    [JsonPropertyName("RemoteControlPwd")]
    public string? RemoteControlPwd { get; set; }

    [JsonPropertyName("Address1Line2")]
    public string? Address1Line2 { get; set; }

    [JsonPropertyName("NamedLicenseBundle")]
    public string? NamedLicenseBundle { get; set; }

    [JsonPropertyName("CreationSource")]
    public string? CreationSource { get; set; }

    [JsonPropertyName("LockDate")]
    public string? LockDate { get; set; }

    [JsonPropertyName("LoginAttemptCount")]
    public int? LoginAttemptCount { get; set; }

    [JsonPropertyName("CustID")]
    public string? CustID { get; set; }

    [JsonPropertyName("SLAClass")]
    public string? SLAClass { get; set; }

    [JsonPropertyName("LockType")]
    public string? LockType { get; set; }

    [JsonPropertyName("Phone1Ext")]
    public string? Phone1Ext { get; set; }

    [JsonPropertyName("Phone2Ext")]
    public string? Phone2Ext { get; set; }

    [JsonPropertyName("WeeklyAvailability")]
    public string? WeeklyAvailability { get; set; }

    [JsonPropertyName("GlobalId")]
    public string? GlobalId { get; set; }

    [JsonPropertyName("LoginId_Name")]
    public string? LoginIdName { get; set; }

    [JsonPropertyName("Emp_LoginId")]
    public string? EmpLoginId { get; set; }

    [JsonPropertyName("ivnt_Director")]
    public string? IvntDirector { get; set; }

    [JsonPropertyName("ivnt_CurrencyText")]
    public string? IvntCurrencyText { get; set; }

    [JsonPropertyName("ivnt_Country")]
    public string? IvntCountry { get; set; }

    [JsonPropertyName("ivnt_HRCaseLink")]
    public string? IvntHRCaseLink { get; set; }

    [JsonPropertyName("ivnt_WorkOrderlink")]
    public string? IvntWorkOrderLink { get; set; }

    [JsonPropertyName("ivnt_BuildingLink")]
    public string? IvntBuildingLink { get; set; }

    [JsonPropertyName("ivnt_CubicleLink")]
    public string? IvntCubicleLink { get; set; }

    [JsonPropertyName("ivnt_FloorLink")]
    public string? IvntFloorLink { get; set; }

    [JsonPropertyName("ivnt_BuildingName")]
    public string? IvntBuildingName { get; set; }

    [JsonPropertyName("ivnt_FloorName")]
    public string? IvntFloorName { get; set; }

    [JsonPropertyName("ivnt_CubicleNumber")]
    public string? IvntCubicleNumber { get; set; }

    [JsonPropertyName("ivnt_UpdateBuilding")]
    public string? IvntUpdateBuilding { get; set; }

    [JsonPropertyName("ivnt_UpdateFloor")]
    public string? IvntUpdateFloor { get; set; }

    [JsonPropertyName("ivnt_UpdateLocation")]
    public string? IvntUpdateLocation { get; set; }

    [JsonPropertyName("BuildingName")]
    public string? BuildingName { get; set; }

    [JsonPropertyName("AccessListOrgUnit")]
    public string? AccessListOrgUnit { get; set; }

    [JsonPropertyName("AzureAD_ID")]
    public string? AzureADID { get; set; }

    [JsonPropertyName("ROLE_TO_LINK")]
    public string? RoleToLink { get; set; }

    [JsonPropertyName("DexClass")]
    public string? DexClass { get; set; }

    [JsonPropertyName("DexScore")]
    public string? DexScore { get; set; }

    [JsonPropertyName("DexScoreDate")]
    public string? DexScoreDate { get; set; }

    [JsonPropertyName("extensionAttributes")]
    public string? ExtensionAttributes { get; set; }

    [JsonPropertyName("extensionAttribute1")]
    public string? ExtensionAttribute1 { get; set; }

    [JsonPropertyName("extensionAttribute2")]
    public string? ExtensionAttribute2 { get; set; }

    [JsonPropertyName("extensionAttribute3")]
    public string? ExtensionAttribute3 { get; set; }

    [JsonPropertyName("extensionAttribute4")]
    public string? ExtensionAttribute4 { get; set; }

    [JsonPropertyName("extensionAttribute5")]
    public string? ExtensionAttribute5 { get; set; }

    [JsonPropertyName("extensionAttribute6")]
    public string? ExtensionAttribute6 { get; set; }

    [JsonPropertyName("extensionAttribute7")]
    public string? ExtensionAttribute7 { get; set; }

    [JsonPropertyName("extensionAttribute8")]
    public string? ExtensionAttribute8 { get; set; }

    [JsonPropertyName("extensionAttribute9")]
    public string? ExtensionAttribute9 { get; set; }

    [JsonPropertyName("extensionAttribute10")]
    public string? ExtensionAttribute10 { get; set; }

    [JsonPropertyName("extensionAttribute11")]
    public string? ExtensionAttribute11 { get; set; }

    [JsonPropertyName("extensionAttribute12")]
    public string? ExtensionAttribute12 { get; set; }

    [JsonPropertyName("extensionAttribute13")]
    public string? ExtensionAttribute13 { get; set; }

    [JsonPropertyName("extensionAttribute14")]
    public string? ExtensionAttribute14 { get; set; }

    [JsonPropertyName("extensionAttribute15")]
    public string? ExtensionAttribute15 { get; set; }

    [JsonPropertyName("nrn_DailyHours")]
    public string? NrnDailyHours { get; set; }

    [JsonPropertyName("nrn_OperationalPercent")]
    public int? NrnOperationalPercent { get; set; }

    [JsonPropertyName("nrn_AvailableHours")]
    public string? NrnAvailableHours { get; set; }

    [JsonPropertyName("nrn_RemainingCapacity")]
    public string? NrnRemainingCapacity { get; set; }

    [JsonPropertyName("nrn_NumberOfDaysRequested")]
    public string? NrnNumberOfDaysRequested { get; set; }

    [JsonPropertyName("nrn_TotalAllocatedHours")]
    public string? NrnTotalAllocatedHours { get; set; }

    [JsonPropertyName("nrn_TaskStartDate")]
    public string? NrnTaskStartDate { get; set; }

    [JsonPropertyName("nrn_TaskEndDate")]
    public string? NrnTaskEndDate { get; set; }

    [JsonPropertyName("nrn_OperationalHours")]
    public string? NrnOperationalHours { get; set; }

    [JsonPropertyName("nrn_AutoCreateTimesheet")]
    public string? NrnAutoCreateTimesheet { get; set; }

    [JsonPropertyName("nrn_FullName")]
    public string? NrnFullName { get; set; }

    [JsonPropertyName("nrn_Gender")]
    public string? NrnGender { get; set; }

    [JsonPropertyName("nrn_MaritalStatus")]
    public string? NrnMaritalStatus { get; set; }

    [JsonPropertyName("nrn_CitizenshipStatus")]
    public string? NrnCitizenshipStatus { get; set; }

    [JsonPropertyName("nrn_MilitaryServiceStatus")]
    public string? NrnMilitaryServiceStatus { get; set; }

    [JsonPropertyName("nrn_ExemptionStatus")]
    public string? NrnExemptionStatus { get; set; }

    [JsonPropertyName("nrn_PersonalEmail")]
    public string? NrnPersonalEmail { get; set; }

    [JsonPropertyName("nrn_InternationalWorkPhone")]
    public string? NrnInternationalWorkPhone { get; set; }

    [JsonPropertyName("nrn_InternationalMobilePhone")]
    public string? NrnInternationalMobilePhone { get; set; }

    [JsonPropertyName("nrn_MailingAddress")]
    public string? NrnMailingAddress { get; set; }

    [JsonPropertyName("nrn_MailingAddress2")]
    public string? NrnMailingAddress2 { get; set; }

    [JsonPropertyName("nrn_MailingCIty")]
    public string? NrnMailingCity { get; set; }

    [JsonPropertyName("nrn_MailingProvinceState")]
    public string? NrnMailingProvinceState { get; set; }

    [JsonPropertyName("nrn_MailingPostalCode")]
    public string? NrnMailingPostalCode { get; set; }

    [JsonPropertyName("nrn_MailingCountry")]
    public string? NrnMailingCountry { get; set; }

    [JsonPropertyName("nrn_MailingCountryCode")]
    public string? NrnMailingCountryCode { get; set; }

    [JsonPropertyName("nrn_MaritalStatusEffectiveDate")]
    public string? NrnMaritalStatusEffectiveDate { get; set; }

    [JsonPropertyName("nrn_PositionID")]
    public string? NrnPositionID { get; set; }

    [JsonPropertyName("nrn_JobProfile")]
    public string? NrnJobProfile { get; set; }

    [JsonPropertyName("nrn_WorkerType")]
    public string? NrnWorkerType { get; set; }

    [JsonPropertyName("nrn_EffectiveDate")]
    public string? NrnEffectiveDate { get; set; }

    [JsonPropertyName("nrn_PayRateType")]
    public string? NrnPayRateType { get; set; }

    [JsonPropertyName("nrn_ScheduledWeeklyHours")]
    public string? NrnScheduledWeeklyHours { get; set; }

    [JsonPropertyName("nrn_PositionTimeType")]
    public string? NrnPositionTimeType { get; set; }

    [JsonPropertyName("nrn_JobExempt")]
    public string? NrnJobExempt { get; set; }

    [JsonPropertyName("nrn_FirstDateOfWork")]
    public string? NrnFirstDateOfWork { get; set; }

    [JsonPropertyName("nrn_IsAManager")]
    public string? NrnIsAManager { get; set; }

    [JsonPropertyName("nrn_ManagementLevel")]
    public string? NrnManagementLevel { get; set; }

    [JsonPropertyName("nrn_Terminated")]
    public string? NrnTerminated { get; set; }

    [JsonPropertyName("nrn_Retired")]
    public string? NrnRetired { get; set; }

    [JsonPropertyName("nrn_RetriementDate")]
    public string? NrnRetirementDate { get; set; }

    [JsonPropertyName("nrn_EmergencyContact")]
    public string? NrnEmergencyContact { get; set; }

    [JsonPropertyName("nrn_WorkPhone")]
    public string? NrnWorkPhone { get; set; }

    [JsonPropertyName("nrn_InternationalPhone")]
    public string? NrnInternationalPhone { get; set; }

    [JsonPropertyName("nrn_Address")]
    public string? NrnAddress { get; set; }

    [JsonPropertyName("nrn_City")]
    public string? NrnCity { get; set; }

    [JsonPropertyName("nrn_State")]
    public string? NrnState { get; set; }

    [JsonPropertyName("nrn_PostalCode")]
    public string? NrnPostalCode { get; set; }

    [JsonPropertyName("nrn_HomePhone")]
    public string? NrnHomePhone { get; set; }

    [JsonPropertyName("nrn_Relationship")]
    public string? NrnRelationship { get; set; }

    [JsonPropertyName("nrn_SecondEmergencyContact")]
    public string? NrnSecondEmergencyContact { get; set; }

    [JsonPropertyName("nrn_ICE_WorkPhone")]
    public string? NrnIceWorkPhone { get; set; }

    [JsonPropertyName("nrn_ICE_InternationalPhone")]
    public string? NrnIceInternationalPhone { get; set; }

    [JsonPropertyName("nrn_ICE_Address")]
    public string? NrnIceAddress { get; set; }

    [JsonPropertyName("nrn_ICE_City")]
    public string? NrnIceCity { get; set; }

    [JsonPropertyName("nrn_ICE_State")]
    public string? NrnIceState { get; set; }

    [JsonPropertyName("nrn_ICE_PostalCode")]
    public string? NrnIcePostalCode { get; set; }

    [JsonPropertyName("nrn_ICE_HomePhone")]
    public string? NrnIceHomePhone { get; set; }

    [JsonPropertyName("nrn_ICE_Relationship")]
    public string? NrnIceRelationship { get; set; }

    [JsonPropertyName("nrn_ICE2_WorkPhone")]
    public string? NrnIce2WorkPhone { get; set; }

    [JsonPropertyName("nrn_ICE2_InternationalPhone")]
    public string? NrnIce2InternationalPhone { get; set; }

    [JsonPropertyName("nrn_ICE2_Address")]
    public string? NrnIce2Address { get; set; }

    [JsonPropertyName("nrn_ICE2_City")]
    public string? NrnIce2City { get; set; }

    [JsonPropertyName("nrn_ICE2_State")]
    public string? NrnIce2State { get; set; }

    [JsonPropertyName("nrn_ICE2_PostalCode")]
    public string? NrnIce2PostalCode { get; set; }

    [JsonPropertyName("nrn_ICE2_HomePhone")]
    public string? NrnIce2HomePhone { get; set; }

    [JsonPropertyName("nrn_ICE2_Relationship")]
    public string? NrnIce2Relationship { get; set; }

    [JsonPropertyName("ChatAnalystLinkedDepartment")]
    public string? ChatAnalystLinkedDepartment { get; set; }

    [JsonPropertyName("IsEnabledChatAsAnalyst")]
    public string? IsEnabledChatAsAnalyst { get; set; }

    [JsonPropertyName("DiscoveryId")]
    public string? DiscoveryId { get; set; }

    [JsonPropertyName("IS_SYSTEM_ACCOUNT")]
    public string? IsSystemAccount { get; set; }

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? AdditionalFields { get; set; }
}