using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.FormDefaultData;

public class FormDefaultDataObject
{
    [JsonPropertyName("ObjectType")]
    public int ObjectType { get; set; }

    [JsonPropertyName("TableRef")]
    public string? TableRef { get; set; }

    [JsonPropertyName("RecordId")]
    public string? RecordId { get; set; }

    [JsonPropertyName("Values")]
    public Dictionary<string, object?> Values { get; set; } = new();

    [JsonPropertyName("PureValues")]
    public Dictionary<string, object?> PureValues { get; set; } = new();
}
