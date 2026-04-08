using Application.Features.Authentication.Models;

namespace Application.Features.Authentication.DTOs;

/// <summary>
/// Response from the login endpoint.
/// Contains authentication result with session and user data.
/// </summary>
public class LoginResponse
{
    public AuthenticationResult? AuthenticationResult { get; set; }
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
}
