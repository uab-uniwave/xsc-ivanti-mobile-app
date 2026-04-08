using System.Text.Json.Serialization;
using Application.Features.Workspaces.Models.GridDataHandler;

namespace Application.Features.Workspaces.DTOs;

/// <summary>
/// Response DTO for GridDataHandler API endpoint.
/// Contains grid data with rows and paging information.
/// </summary>
public class GridDataHandlerResponse
{
    [JsonPropertyName("d")]
    public GridDataHandler D { get; set; } = new();
}
