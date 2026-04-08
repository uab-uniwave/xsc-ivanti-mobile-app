using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Features.Workspaces.Models.FormDefaultData;
using Application.Features.Workspaces.Models.FormViewData;

namespace Application.Features.Workspaces.DTOs;

public class GetFormValidationListDataRequest
{
    [JsonPropertyName("formValidationList")]
    public FormValidationList? FormValidationList { get; set; }

    [JsonPropertyName("_csrfToken")]
    public string? CsrfToken { get; set; }
}

public class FormValidationList
{
    [JsonPropertyName("NamedValidators")]
    public string? NamedValidators { get; set; }

    [JsonPropertyName("ValidatorsOverride")]
    public string? ValidatorsOverride { get; init; }

    [JsonPropertyName("MasterFormValues")]
    public FormDefaultData.FormDataModel? MasterFormValues { get; set; }

    [JsonPropertyName("objectId")]
    public string? ObjectId { get; set; }
}
