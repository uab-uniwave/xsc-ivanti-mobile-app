using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Application.Features.Workspaces.Models.RoleWorkspaces;

namespace Application.Features.Workspaces.DTOs;

/// <summary>
/// Response  for GetRoleWorkspaces API endpoint.
/// Contains workspace configuration and branding options for the user's role.
/// </summary>
public class GetRoleWorkspacesResponse
{
    [JsonPropertyName("d")]
    public RoleWorkspaces D { get; set; } = new();

}
