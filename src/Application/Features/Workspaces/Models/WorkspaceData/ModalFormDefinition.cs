using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.WorkspaceData;

public class ModalFormDefinition
{
    [JsonPropertyName("CreationForm")]
    public string? CreationForm { get; set; }

    [JsonPropertyName("EditForm")]
    public string? EditForm { get; set; }
}
