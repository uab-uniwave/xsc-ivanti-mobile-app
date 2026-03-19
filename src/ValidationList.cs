
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

public class FormValidationListResponse
{
    [JsonPropertyName("formValidationList")]
    public FormValidationList? FormValidationList { get; set; }
}

public class FormValidationList
{
    [JsonPropertyName("NamedValidators")]
    public string? NamedValidatorsRaw { get; set; }

    [JsonPropertyName("ValidatorsOverride")]
    public string? ValidatorsOverrideRaw { get; set; }

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
    public Dictionary<string, DataModelObject> Objects { get; set; } = new();

    [JsonPropertyName("ObjectRelationships")]
    public Dictionary<string, Dictionary<string, string>> ObjectRelationships { get; set; } = new();
}

public class DataModelObject
{
    [JsonPropertyName("ObjectType")]
    public int ObjectType { get; set; }

    [JsonPropertyName("TableRef")]
    public string? TableRef { get; set; }

    [JsonPropertyName("RecordId")]
    public string? RecordId { get; set; }

    [JsonPropertyName("Values")]
    public Dictionary<string, JsonElement> Values { get; set; } = new();

    [JsonPropertyName("PureValues")]
    public Dictionary<string, JsonElement> PureValues { get; set; } = new();
}
