using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.ValidatedSearch;

public class SearchCondition
{
    [JsonPropertyName("__type")]
    public string? Type { get; set; }

    [JsonPropertyName("ObjectId")]
    public string? ObjectId { get; set; }

    [JsonPropertyName("ObjectDisplay")]
    public string? ObjectDisplay { get; set; }

    [JsonPropertyName("JoinRule")]
    public string? JoinRule { get; set; }

    [JsonPropertyName("Condition")]
    public string? Condition { get; set; }

    [JsonPropertyName("ConditionType")]
    public int ConditionType { get; set; }

    [JsonPropertyName("FieldName")]
    public string? FieldName { get; set; }

    [JsonPropertyName("FieldDisplay")]
    public string? FieldDisplay { get; set; }

    [JsonPropertyName("FieldType")]
    public string? FieldType { get; set; }

    [JsonPropertyName("FieldValue")]
    public string? FieldValue { get; set; }

    [JsonPropertyName("FieldValueDisplay")]
    public string? FieldValueDisplay { get; set; }
}
