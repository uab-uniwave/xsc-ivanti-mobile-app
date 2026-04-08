using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Application.Interfaces.Authentication;
using Application.Interfaces.Navigation;
using Application.Interfaces.Storage;
using Application.Features.Authentication.Models;

namespace WebUI.Features.Authentication.ViewModels;

/// <summary>
/// ViewModel for the Login page.
/// Manages user authentication state and login logic.
/// 
/// Workflow:
/// 1. First, try to restore existing session from cookies (skip login if valid)
/// 2. If session invalid/expired, fetch verification token and show login form
/// 3. On successful login, store session data for future restoration
/// </summary>
public class LoginViewModel
{
    private readonly IAuthenticationService _authenticationService;
    private readonly ISessionValidator _sessionValidator;
    private readonly ICookieManager _cookieManager;
    private readonly INavigationService _navigationService;
    private readonly IClientStorageService _storageService;
    private readonly ILogger<LoginViewModel> _logger;

    // Storage keys
    private const string VerificationDataKey = "ivanti_verification_data";
    private const string SessionValidKey = "ivanti_session_valid";
    private const string SelectRoleDataKey = "ivanti_select_role_data";

    public LoginViewModel(
        IAuthenticationService authenticationService,
        ISessionValidator sessionValidator,
        ICookieManager cookieManager,
        INavigationService navigationService,
        IClientStorageService storageService,
        ILogger<LoginViewModel> logger)
    {
        _authenticationService = authenticationService;
        _sessionValidator = sessionValidator;
        _cookieManager = cookieManager;
        _navigationService = navigationService;
        _storageService = storageService;
        _logger = logger;
    }

    // Properties for two-way binding
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = string.Empty;

    public bool IsLoading { get; private set; }
    public bool HasError { get; private set; }
    public string ErrorMessage { get; private set; } = string.Empty;
    public bool IsInitialized { get; private set; }
    public bool IsCheckingSession { get; private set; }

    private VerificationToken? _verificationToken;

    /// <summary>
    /// Initializes the login page.
    /// First tries to restore existing session, then falls back to login flow.
    /// Should be called only during interactive rendering (OnAfterRenderAsync).
    /// </summary>
    public async Task InitializeAsync()
    {
        // Skip if already initialized
        if (IsInitialized)
        {
            _logger.LogDebug("Login page already initialized, skipping");
            return;
        }

        // Check if JS interop is available (skip during prerendering)
        if (!await _storageService.IsAvailableAsync())
        {
            _logger.LogDebug("JS interop not available (prerendering), skipping initialization");
            return;
        }

        try
        {
            _logger.LogInformation("Initializing login page...");
            IsCheckingSession = true;

            // Step 1: Try to restore existing session from cookies
            var sessionRestored = await TryRestoreExistingSessionAsync();
            if (sessionRestored)
            {
                // Session is valid, navigate to home (skip login)
                return;
            }

            // Step 2: Session invalid/expired, prepare for login flow
            await PrepareLoginFlowAsync();

            IsInitialized = true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error initializing login page");
            HasError = true;
            ErrorMessage = "An error occurred while loading the login page.";
        }
        finally
        {
            IsCheckingSession = false;
        }
    }

    /// <summary>
    /// Tries to restore an existing session using stored cookies.
    /// Returns true if session is valid and user was redirected.
    /// </summary>
    private async Task<bool> TryRestoreExistingSessionAsync()
    {
        // Check if we have a marker indicating a potentially valid session
        var sessionMarker = await _storageService.GetItemAsync(SessionValidKey);
        if (string.IsNullOrEmpty(sessionMarker))
        {
            _logger.LogDebug("No session marker found, skipping session restore");
            return false;
        }

        _logger.LogInformation("Found session marker, attempting to restore session...");

        // Try to restore the session using existing cookies
        var restoreResult = await _sessionValidator.TryRestoreSessionAsync();

        if (restoreResult.IsSuccess && restoreResult.Value != null)
        {
            _logger.LogInformation("Session restored successfully, navigating to home");
            _navigationService.NavigateToHome();
            return true;
        }

        // Session invalid/expired, clear markers
        _logger.LogInformation("Session expired or invalid, clearing stored data");
        await _storageService.ClearAuthDataAsync();
        return false;
    }

    /// <summary>
    /// Prepares the login flow by fetching a fresh verification token.
    /// Note: Verification tokens from Ivanti are tied to server-side session state
    /// and cannot be reliably restored across app restarts because the AFT cookie
    /// pairing is managed server-side by Ivanti.
    /// </summary>
    private async Task PrepareLoginFlowAsync()
    {
        // Always fetch a fresh token - cached tokens become invalid after app restart
        // because Ivanti's server-side AFT cookie pairing is lost when CookieContainer is recreated
        _cookieManager.ClearCookies();
        await _storageService.RemoveItemAsync(VerificationDataKey);

        _logger.LogInformation("Fetching fresh verification token from Ivanti...");

        var tokenResult = await _authenticationService.GetVerificationTokenAsync();
        if (tokenResult.IsSuccess && tokenResult.Value != null)
        {
            _verificationToken = tokenResult.Value;

            // Save to localStorage for potential use within the same browser session
            var serializedData = JsonSerializer.Serialize(_verificationToken);
            await _storageService.SetItemAsync(VerificationDataKey, serializedData);

            _logger.LogInformation("Verification token retrieved and stored. Tenant: {Tenant}, Cookies: {CookieCount}", 
                _verificationToken.Tenant, _verificationToken.Cookies?.Count ?? 0);
        }
        else
        {
            _logger.LogError("Failed to get verification token: {Error}", tokenResult.Error);
            HasError = true;
            ErrorMessage = "Failed to initialize login page. Please refresh.";
        }
    }

    /// <summary>
    /// Handles the login button click.
    /// Authenticates the user and navigates to role selection or home.
    /// </summary>
    public async Task HandleLoginAsync()
    {
        if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
        {
            HasError = true;
            ErrorMessage = "Please enter both username and password.";
            return;
        }

        if (_verificationToken == null || string.IsNullOrEmpty(_verificationToken.Token))
        {
            HasError = true;
            ErrorMessage = "Verification token is missing. Please refresh the page.";
            return;
        }

        IsLoading = true;
        HasError = false;
        ErrorMessage = string.Empty;

        try
        {
            _logger.LogInformation("Attempting login for user: {Username}", Username);

            var result = await _authenticationService.LoginAsync(
                Username,
                Password,
                _verificationToken.Token);

            if (result.IsSuccess && result.Value != null)
            {
                var rolesCount = result.Value.AvailableRoles.Count;
                _logger.LogInformation("Login successful, {RoleCount} roles available", rolesCount);

                // Clear verification data, keep session marker for future restore
                await _storageService.RemoveItemAsync(VerificationDataKey);

                if (rolesCount == 0)
                {
                    HasError = true;
                    ErrorMessage = "No roles available for this user.";
                }
                else if (rolesCount == 1)
                {
                    // Auto-select the only role and complete authentication
                    await CompleteAuthenticationAsync(
                        result.Value.AvailableRoles[0].Id,
                        result.Value.VerificationToken);
                }
                else
                {
                    // Multiple roles available - store role data and navigate to role selection
                    var serializedRoleData = JsonSerializer.Serialize(result.Value);
                    await _storageService.SetItemAsync(SelectRoleDataKey, serializedRoleData);
                    _logger.LogInformation("Stored {Count} roles to localStorage for role selection page", rolesCount);

                    _navigationService.NavigateToRoleSelection();
                }
            }
            else
            {
                _logger.LogWarning("Login failed: {Error}", result.Error);
                HasError = true;
                ErrorMessage = result.Error ?? "Login failed. Please check your credentials.";

                // Clear stored data AND cookies on login failure to get fresh token/cookie pair
                _cookieManager.ClearCookies();
                await _storageService.ClearAuthDataAsync();
                IsInitialized = false;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login");
            HasError = true;
            ErrorMessage = "An unexpected error occurred during login.";
        }
        finally
        {
            IsLoading = false;
        }
    }

    /// <summary>
    /// Completes authentication by selecting a role and storing session marker.
    /// </summary>
    private async Task CompleteAuthenticationAsync(string roleId, string verificationToken)
    {
        _logger.LogInformation("Auto-selecting role: {Role}", roleId);

        var selectResult = await _authenticationService.SelectRoleAsync(roleId, verificationToken);

        if (selectResult.IsSuccess)
        {
            // Store session marker for future session restoration
            await _storageService.SetItemAsync(SessionValidKey, DateTime.UtcNow.ToString("O"));

            _navigationService.NavigateToHome();
        }
        else
        {
            HasError = true;
            ErrorMessage = selectResult.Error ?? "Failed to complete authentication.";
        }
    }

    /// <summary>
    /// Clears any error messages.
    /// </summary>
    public void ClearError()
    {
        HasError = false;
        ErrorMessage = string.Empty;
    }

    /// <summary>
    /// Forces a fresh verification token fetch (clears cached token and cookies).
    /// </summary>
    public async Task RefreshTokenAsync()
    {
        _cookieManager.ClearCookies();
        await _storageService.ClearAuthDataAsync();
        IsInitialized = false;
        await InitializeAsync();
    }
}
