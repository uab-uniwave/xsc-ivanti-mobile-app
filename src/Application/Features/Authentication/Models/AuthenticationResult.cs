using Application.Common.Models.SessonData;

namespace Application.Features.Authentication.Models;

/// <summary>
/// Contains the complete result of a successful authentication,
/// including session data and user information.
/// </summary>
public class AuthenticationResult
{
    public SessionData SessionData { get; set; } = null!;
    public Common.Models.UserData.UserData UserData { get; set; } = null!;
    public string? AccessToken { get; set; }
    public DateTime AuthenticatedAt { get; set; } = DateTime.UtcNow;
    public bool IsAuthenticated => SessionData != null && UserData != null;
}
