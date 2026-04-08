using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.FormValidationListData;

public class FormValidationListData
{
    [JsonPropertyName("ActualCategory")]
    public FieldData? ActualCategory { get; set; }

    [JsonPropertyName("Category")]
    public FieldData? Category { get; set; }

    [JsonPropertyName("CauseCode")]
    public FieldData? CauseCode { get; set; }

    [JsonPropertyName("Impact")]
    public FieldData? Impact { get; set; }

    [JsonPropertyName("Priority")]
    public FieldData? Priority { get; set; }

    [JsonPropertyName("Service")]
    public FieldData? Service { get; set; }

    [JsonPropertyName("Source")]
    public FieldData? Source { get; set; }

    [JsonPropertyName("Status")]
    public FieldData? Status { get; set; }

    [JsonPropertyName("Urgency")]
    public FieldData? Urgency { get; set; }

    [JsonPropertyName("Owner")]
    public FieldData? Owner { get; set; }

    [JsonPropertyName("OwnerTeam")]
    public FieldData? OwnerTeam { get; set; }

    [JsonPropertyName("ActualService")]
    public FieldData? ActualService { get; set; }

    [JsonPropertyName("OwningOrgUnitId")]
    public FieldData? OwningOrgUnitId { get; set; }

    [JsonPropertyName("ReportingOrgUnitID")]
    public FieldData? ReportingOrgUnitID { get; set; }

    [JsonPropertyName("Subcategory")]
    public FieldData? Subcategory { get; set; }

    [JsonPropertyName("helpdesk_Priority")]
    public FieldData? HelpdeskPriority { get; set; }

    [JsonPropertyName("CustomerLocation")]
    public FieldData? CustomerLocation { get; set; }

    [JsonPropertyName("Approver")]
    public FieldData? Approver { get; set; }

    [JsonPropertyName("HoursOfOperation")]
    public FieldData? HoursOfOperation { get; set; }

    [JsonPropertyName("Cost_Currency")]
    public FieldData? CostCurrency { get; set; }

    [JsonPropertyName("CostPerMinute_Currency")]
    public FieldData? CostPerMinuteCurrency { get; set; }
}

/// <summary>
/// Represents field data structure containing dropdown/lookup information for form fields
/// </summary>
public class FieldData
{
    /// <summary>
    /// Maps field property names to their column indices in the Data array
    /// </summary>
    [JsonPropertyName("FieldMap")]
    public Dictionary<string, int>? FieldMap { get; set; }

    /// <summary>
    /// The actual data rows for this field. Each array element is a row with values corresponding to FieldMap indices
    /// </summary>
    [JsonPropertyName("Data")]
    public List<List<object>>? Data { get; set; }

    /// <summary>
    /// If not null, indicates this field uses the same data as another field (referenced by this property name)
    /// </summary>
    [JsonPropertyName("SameAs")]
    public string? SameAs { get; set; }

    /// <summary>
    /// Indicates if there are more records available beyond what's returned
    /// </summary>
    [JsonPropertyName("More")]
    public bool More { get; set; }

    /// <summary>
    /// If true, a single value in the dropdown should be automatically selected
    /// </summary>
    [JsonPropertyName("AutoSelectSingleValue")]
    public bool AutoSelectSingleValue { get; set; }
}
