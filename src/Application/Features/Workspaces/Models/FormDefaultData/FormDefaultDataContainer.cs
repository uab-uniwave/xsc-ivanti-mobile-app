using System.Text.Json.Serialization;
using Application.Features.Workspaces.Models.FormViewData;

namespace Application.Features.Workspaces.Models.FormDefaultData;

public class FormDefaultDataContainer
{
    [JsonPropertyName("ObjectId")]
    public string? ObjectId { get; set; }

    [JsonPropertyName("Def")]
    public FormViewDefinition? Def { get; set; }

    [JsonPropertyName("Data")]
    public FormDataModel? FormData { get; set; }

    [JsonPropertyName("FinalStateStatus")]
    public object? FinalStateStatus { get; set; }
}
