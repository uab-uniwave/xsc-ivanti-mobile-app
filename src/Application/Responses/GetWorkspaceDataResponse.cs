using System.Collections.Generic;
using System.Text.Json.Serialization;
using Application.DTOs;

namespace Application.Responses;

/// <summary>
/// Response  for GetWorkspaceData API endpoint.
/// Contains workspace metadata, search configuration, and field definitions.
/// </summary>
public class GetWorkspaceDataResponse
{
  
        [JsonPropertyName("d")]
        public WorkspaceDataDto D { get; set; } = new();
   
}