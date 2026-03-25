using System.Collections.Generic;
using System.Text.Json.Serialization;
using Application.DTOs;
using Application.Models;

namespace Application.Responses;

/// <summary>
/// Response  for GetUserData API endpoint.
/// Received from Ivanti API containing user profile information.
/// </summary>
public class GetUserDataResponse
{
    [JsonPropertyName("d")]
    public UserDataDto D { get; set; } = new();
}