using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Models.FormViewData;

namespace Application.Requests;

public class FormValidationListDataRequest
{
    [JsonPropertyName("formValidationList")]
    public FormValidationList? FormValidationList { get; init; }

    [JsonPropertyName("_csrfToken")]
    public string? CsrfToken { get; init; }
}

public class FormValidationList
{
    [JsonPropertyName("NamedValidators")]
    public string? NamedValidators { get; set; }

    [JsonPropertyName("ValidatorsOverride")]
    public string? ValidatorsOverride { get; set; }

    [JsonPropertyName("MasterFormValues")]
    public MasterFormValues? MasterFormValues { get; set; }

    [JsonPropertyName("objectId")]
    public string? ObjectId { get; set; }
}

public class MasterFormValues
{
    [JsonPropertyName("__type")]
    public string? Type { get; set; }

    [JsonPropertyName("Objects")]
    public Dictionary<string, DataObject>? Objects { get; set; }

    [JsonPropertyName("ObjectRelationships")]
    public Dictionary<string, Dictionary<string, string>>? ObjectRelationships { get; set; }
}

public class DataObject
{
    [JsonPropertyName("ObjectType")]
    public int ObjectType { get; set; }

    [JsonPropertyName("TableRef")]
    public string? TableRef { get; set; }

    [JsonPropertyName("RecordId")]
    public string? RecordId { get; set; }

    [JsonPropertyName("Values")]
    public Dictionary<string, JsonElement>? Values { get; set; }

    [JsonPropertyName("PureValues")]
    public Dictionary<string, JsonElement>? PureValues { get; set; }
}