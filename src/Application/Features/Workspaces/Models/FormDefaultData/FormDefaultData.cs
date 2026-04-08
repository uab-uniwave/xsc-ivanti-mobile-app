using System.Collections.Generic;
using System.Text.Json.Serialization;
using Application.Features.Workspaces.Models.FormViewData;

namespace Application.Features.Workspaces.Models.FormDefaultData;

public class FormDefaultData
{
    [JsonPropertyName("ObjectId")]
    public string? ObjectId { get; set; }

    [JsonPropertyName("Def")]
    public FormViewDefinition? Def { get; set; }

    [JsonPropertyName("Data")]
    public FormDataModel? Data { get; set; }

    [JsonPropertyName("FinalStateStatus")]
    public object? FinalStateStatus { get; set; }
}
