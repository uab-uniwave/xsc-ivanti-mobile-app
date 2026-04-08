using System.Collections.Generic;
using System.Text.Json.Serialization;
using Application.Common.Models.UserData;

namespace Application.Features.Authentication.DTOs;

/// <summary>
/// Response  for GetUserData API endpoint.
/// Received from Ivanti API containing user profile information.
/// </summary>
public class GetUserDataResponse
{
    [JsonPropertyName("d")]
    public UserData D { get; set; } = new();
}