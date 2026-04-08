using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.WorkspaceData;

public class WorkspaceLayoutData
{
    [JsonPropertyName("newRecordViews")]
    public Dictionary<string, string>? NewRecordViews { get; set; }

    [JsonPropertyName("oneNewRecordView")]
    public string? OneNewRecordView { get; set; }

    [JsonPropertyName("editRecordViews")]
    public Dictionary<string, string>? EditRecordViews { get; set; }

    [JsonPropertyName("oneEditRecordView")]
    public string? OneEditRecordView { get; set; }

    [JsonPropertyName("modalForms")]
    public Dictionary<string, ModalFormDefinition>? ModalForms { get; set; }
}
