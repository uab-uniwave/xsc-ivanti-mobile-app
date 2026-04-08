using Application.Features.Authentication.Models;

namespace Application.Features.Authentication.DTOs;

/// <summary>
/// Response containing the verification token extracted from the login page.
/// </summary>
public class GetVerificationTokenResponse
{
    public VerificationToken? VerificationToken { get; set; }
}
