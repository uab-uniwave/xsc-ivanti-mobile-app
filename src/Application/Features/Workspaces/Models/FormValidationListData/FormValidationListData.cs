using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.FormValidationListData;

public class FormValidationListData
{
    [JsonPropertyName("ActualCategory")]
    public FormValidationFieldData? ActualCategory { get; set; }

    [JsonPropertyName("Category")]
    public FormValidationFieldData? Category { get; set; }

    [JsonPropertyName("CauseCode")]
    public FormValidationFieldData? CauseCode { get; set; }

    [JsonPropertyName("Impact")]
    public FormValidationFieldData? Impact { get; set; }

    [JsonPropertyName("Priority")]
    public FormValidationFieldData? Priority { get; set; }

    [JsonPropertyName("Service")]
    public FormValidationFieldData? Service { get; set; }

    [JsonPropertyName("Source")]
    public FormValidationFieldData? Source { get; set; }

    [JsonPropertyName("Status")]
    public FormValidationFieldData? Status { get; set; }

    [JsonPropertyName("Urgency")]
    public FormValidationFieldData? Urgency { get; set; }

    [JsonPropertyName("Owner")]
    public FormValidationFieldData? Owner { get; set; }

    [JsonPropertyName("OwnerTeam")]
    public FormValidationFieldData? OwnerTeam { get; set; }

    [JsonPropertyName("ActualService")]
    public FormValidationFieldData? ActualService { get; set; }

    [JsonPropertyName("OwningOrgUnitId")]
    public FormValidationFieldData? OwningOrgUnitId { get; set; }

    [JsonPropertyName("ReportingOrgUnitID")]
    public FormValidationFieldData? ReportingOrgUnitID { get; set; }

    [JsonPropertyName("Subcategory")]
    public FormValidationFieldData? Subcategory { get; set; }

    [JsonPropertyName("helpdesk_Priority")]
    public FormValidationFieldData? HelpdeskPriority { get; set; }

    [JsonPropertyName("CustomerLocation")]
    public FormValidationFieldData? CustomerLocation { get; set; }

    [JsonPropertyName("Approver")]
    public FormValidationFieldData? Approver { get; set; }

    [JsonPropertyName("HoursOfOperation")]
    public FormValidationFieldData? HoursOfOperation { get; set; }

    [JsonPropertyName("Cost_Currency")]
    public FormValidationFieldData? CostCurrency { get; set; }

    [JsonPropertyName("CostPerMinute_Currency")]
    public FormValidationFieldData? CostPerMinuteCurrency { get; set; }
}
