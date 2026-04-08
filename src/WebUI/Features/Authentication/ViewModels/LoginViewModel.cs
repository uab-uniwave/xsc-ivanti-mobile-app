using System.ComponentModel.DataAnnotations;
using Application.Interfaces.Authentication;
using Application.Interfaces.Navigation;

namespace WebUI.Features.Authentication.ViewModels;

/// <summary>
/// ViewModel for the Login page.
/// Manages user authentication state and login logic.
/// </summary>
public class LoginViewModel
{
    private readonly IAuthenticationService _authenticationService;
    private readonly INavigationService _navigationService;
    private readonly ILogger<LoginViewModel> _logger;

    public LoginViewModel(
        IAuthenticationService authenticationService,
        INavigationService navigationService,
        ILogger<LoginViewModel> logger)
    {
        _authenticationService = authenticationService;
        _navigationService = navigationService;
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

    private string? _verificationToken;

    /// <summary>
    /// Initializes the login page by fetching the verification token.
    /// Should be called when the page loads.
    /// </summary>
    public async Task InitializeAsync()
    {
        try
        {
            _logger.LogInformation("Initializing login page...");

            var tokenResult = await _authenticationService.GetVerificationTokenAsync();
            if (tokenResult.IsSuccess && tokenResult.Value != null)
            {
                _verificationToken = tokenResult.Value.Token;
                _logger.LogInformation("Verification token retrieved successfully");
            }
            else
            {
                _logger.LogError("Failed to get verification token: {Error}", tokenResult.Error);
                HasError = true;
                ErrorMessage = "Failed to initialize login page. Please refresh.";
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error initializing login page");
            HasError = true;
            ErrorMessage = "An error occurred while loading the login page.";
        }
    }

    /// <summary>
    /// Handles the login button click.
    /// Authenticates the user and navigates to role selection.
    /// </summary>
    public async Task HandleLoginAsync()
    {
        if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
        {
            HasError = true;
            ErrorMessage = "Please enter both username and password.";
            return;
        }

        if (string.IsNullOrEmpty(_verificationToken))
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
                _verificationToken);

            if (result.IsSuccess)
            {
                _logger.LogInformation("Login successful, navigating to role selection");
                _navigationService.NavigateToSelectRole();
            }
            else
            {
                _logger.LogWarning("Login failed: {Error}", result.Error);
                HasError = true;
                ErrorMessage = result.Error ?? "Login failed. Please check your credentials.";
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
    /// Clears any error messages.
    /// </summary>
    public void ClearError()
    {
        HasError = false;
        ErrorMessage = string.Empty;
    }
}
