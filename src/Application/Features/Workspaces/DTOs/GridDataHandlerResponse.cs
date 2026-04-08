using System.Collections.Generic;
using System.Text.Json.Serialization;
using Application.Features.Workspaces.Models.FormViewData;

namespace Application.Features.Workspaces.DTOs;

/// <summary>
/// Response DTO for FindFormViewData API endpoint.
/// Contains form view structure, layout configuration, and form definitions.
/// </summary>
public class GridDataHandlerResponse
{
    [JsonPropertyName("d")]
    public FormViewData D { get; set; } = new();
}
