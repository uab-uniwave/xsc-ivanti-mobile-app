using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Application.DTOs;

namespace Application.Responses;

/// <summary>
/// Response  for GetRoleWorkspaces API endpoint.
/// Contains workspace configuration and branding options for the user's role.
/// </summary>
public class GetRoleWorkspacesResponse
{
    [JsonPropertyName("d")]
    public RoleWorkspacesDto D { get; set; } = new();

}
