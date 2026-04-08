using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.FormDefaultData;

public class FormDataModel
{
    [JsonPropertyName("__type")]
    public string? Type { get; set; }

    [JsonPropertyName("Objects")]
    public Dictionary<string, FormDefaultDataObject> Objects { get; set; } = new();

    [JsonPropertyName("ObjectRelationships")]
    public Dictionary<string, Dictionary<string, string>> ObjectRelationships { get; set; } = new();
}
