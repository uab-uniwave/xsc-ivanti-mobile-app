using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.DTOs;

/// <summary>
/// Request  for FindFormViewData API endpoint.
/// Sent to Ivanti API to retrieve form view structure and layout.
/// </summary>
public class FindFormViewDataRequest
{
    [JsonPropertyName("createdViewsOnClient")]
    public Dictionary<string, object>? CreatedViewsOnClient { get; init; } = new();

    [JsonPropertyName("isNewRecord")]
    public bool IsNewRecord { get; init; }

    [JsonPropertyName("layoutName")]
    public string? LayoutName { get; init; }

    [JsonPropertyName("objectId")]
    public string? ObjectId { get; init; }

    [JsonPropertyName("viewName")]
    public string? ViewName { get; init; }

    [JsonPropertyName("_csrfToken")]
    public string? CsrfToken { get; init; }
}
