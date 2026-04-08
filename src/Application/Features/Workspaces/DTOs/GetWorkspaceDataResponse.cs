using System.Collections.Generic;
using System.Text.Json.Serialization;
using Application.Features.Workspaces.Models.WorkspaceData;

namespace Application.Features.Workspaces.DTOs;

/// <summary>
/// Response  for GetWorkspaceData API endpoint.
/// Contains workspace metadata, search configuration, and field definitions.
/// </summary>
public class GetWorkspaceDataResponse
{
  
        [JsonPropertyName("d")]
        public WorkspaceData D { get; set; } = new();
   
}