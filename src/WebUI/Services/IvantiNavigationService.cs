using Application.Common;
using Application.Common.Models.SessonData;
using Application.Common.Models.UserData;
using Application.Features.Workspaces.Models.RoleWorkspaces;
using Application.Services;

namespace WebUI.Services;

/// <summary>
/// Manages Ivanti session state and navigation data for the application.
/// This service initializes and maintains user session, user data, and workspaces.
/// </summary>
public class IvantiNavigationService
{
    private readonly IIvantiClient _ivantiClient;
    private readonly ILogger<IvantiNavigationService> _logger;

    public SessionData? SessionData { get; private set; }
    public UserData? UserData { get; private set; }
    public RoleWorkspaces? RoleWorkspaces { get; private set; }

    public bool IsInitialized { get; private set; }
    public bool IsLoading { get; private set; }
    public string? ErrorMessage { get; private set; }

    public event Action? OnStateChanged;

    public IvantiNavigationService(
        IIvantiClient ivantiClient,
        ILogger<IvantiNavigationService> logger)
    {
        _ivantiClient = ivantiClient;
        _logger = logger;
    }

    /// <summary>
    /// Initializes the Ivanti session by calling the required APIs in sequence.
    /// </summary>
    public async Task<bool> InitializeAsync(CancellationToken cancellationToken = default)
    {
        if (IsInitialized)
        {
            _logger.LogInformation("Ivanti navigation already initialized");
            return true;
        }

        IsLoading = true;
        ErrorMessage = null;
        NotifyStateChanged();

        try
        {
            _logger.LogInformation("Starting Ivanti session initialization...");

            // Step 1: Initialize Session
            var sessionResult = await _ivantiClient.InitializeSessionAsync(cancellationToken);
            if (sessionResult.IsFailure)
            {
                ErrorMessage = $"Failed to initialize session: {sessionResult.Error}";
                _logger.LogError(ErrorMessage);
                return false;
            }
            SessionData = sessionResult.Value;
            _logger.LogInformation("Session initialized successfully");

            // Step 2: Get User Data
            var userDataResult = await _ivantiClient.GetUserDataAsync(cancellationToken);
            if (userDataResult.IsFailure)
            {
                ErrorMessage = $"Failed to get user data: {userDataResult.Error}";
                _logger.LogError(ErrorMessage);
                return false;
            }
            UserData = userDataResult.Value;
            _logger.LogInformation("User data retrieved: {DisplayName}", UserData.DisplayName);

            // Step 3: Get Role Workspaces
            var workspacesResult = await _ivantiClient.GetRoleWorkspacesAsync(cancellationToken);
            if (workspacesResult.IsFailure)
            {
                ErrorMessage = $"Failed to get workspaces: {workspacesResult.Error}";
                _logger.LogError(ErrorMessage);
                return false;
            }
            RoleWorkspaces = workspacesResult.Value;
            _logger.LogInformation("Loaded {Count} workspaces", RoleWorkspaces.Workspaces.Count);

            IsInitialized = true;
            ErrorMessage = null;
            return true;
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Initialization error: {ex.Message}";
            _logger.LogError(ex, "Error during Ivanti navigation initialization");
            return false;
        }
        finally
        {
            IsLoading = false;
            NotifyStateChanged();
        }
    }

    /// <summary>
    /// Signs out the user and clears all session data.
    /// </summary>
    public Task SignOutAsync()
    {
        SessionData = null;
        UserData = null;
        RoleWorkspaces = null;
        IsInitialized = false;
        ErrorMessage = null;

        _logger.LogInformation("User signed out");
        NotifyStateChanged();

        return Task.CompletedTask;
    }

    /// <summary>
    /// Gets the user's initials for avatar display.
    /// </summary>
    public string GetUserInitials()
    {
        if (UserData == null) return "?";

        var firstName = UserData.FirstName?.FirstOrDefault() ?? '?';
        var lastName = UserData.LastName?.FirstOrDefault() ?? '?';

        return $"{firstName}{lastName}".ToUpper();
    }

    /// <summary>
    /// Gets the user's display name.
    /// </summary>
    public string GetUserDisplayName()
    {
        return UserData?.DisplayName ?? "Unknown User";
    }

    private void NotifyStateChanged() => OnStateChanged?.Invoke();
}
