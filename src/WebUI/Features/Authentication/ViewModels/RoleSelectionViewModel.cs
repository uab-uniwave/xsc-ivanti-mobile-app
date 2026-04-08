using System.Text.Json;
using Application.Features.Authentication.Models;
using Application.Interfaces.Authentication;
using Application.Interfaces.Navigation;
using Application.Interfaces.Storage;
using Microsoft.Extensions.Logging;
using WebUI.Services;

namespace WebUI.Features.Authentication.ViewModels;

/// <summary>
/// ViewModel for the RoleSelection page.
/// Manages Ivanti role selection after successful login when multiple roles are available.
/// </summary>
public class RoleSelectionViewModel
{
    private readonly IAuthenticationService _authenticationService;
    private readonly INavigationService _navigationService;
    private readonly IClientStorageService _storageService;
    private readonly ILogger<RoleSelectionViewModel> _logger;

    private const string SessionValidKey = "ivanti_session_valid";
    private const string SelectRoleDataKey = "ivanti_select_role_data";

    public RoleSelectionViewModel(
        IAuthenticationService authenticationService,
        INavigationService navigationService,
        IClientStorageService storageService,
        ILogger<RoleSelectionViewModel> logger)
    {
        _authenticationService = authenticationService;
        _navigationService = navigationService;
        _storageService = storageService;
        _logger = logger;
    }

    /// <summary>
    /// Gets the list of available Ivanti roles.
    /// </summary>
    public List<AvailableRole> AvailableRoles { get; private set; } = new();

    /// <summary>
    /// Gets whether data is currently loading.
    /// </summary>
    public bool IsLoading { get; private set; }

    /// <summary>
    /// Gets whether the page has been initialized.
    /// </summary>
    public bool IsInitialized { get; private set; }

    /// <summary>
    /// Gets whether an error occurred.
    /// </summary>
    public bool HasError { get; private set; }

    /// <summary>
    /// Gets the error message if an error occurred.
    /// </summary>
    public string ErrorMessage { get; private set; } = string.Empty;

    /// <summary>
    /// Gets the verification token for role selection.
    /// </summary>
    private string VerificationToken { get; set; } = string.Empty;

    /// <summary>
    /// Initializes the role selection page by loading available roles from localStorage.
    /// Should be called only during interactive rendering (OnAfterRenderAsync).
    /// </summary>
    public async Task InitializeAsync()
    {
        // Skip if already initialized
        if (IsInitialized)
        {
            _logger.LogDebug("Role selection page already initialized, skipping");
            return;
        }

        // Check if JS interop is available (skip during prerendering)
        if (!await _storageService.IsAvailableAsync())
        {
            _logger.LogDebug("JS interop not available (prerendering), skipping initialization");
            return;
        }

        HasError = false;
        ErrorMessage = string.Empty;

        try
        {
            _logger.LogInformation("Loading available roles from localStorage...");

            // Try to load from localStorage first
            var serializedData = await _storageService.GetItemAsync(SelectRoleDataKey);

            if (string.IsNullOrEmpty(serializedData))
            {
                _logger.LogWarning("No role data found in localStorage");
                HasError = true;
                ErrorMessage = "No roles available. Please login again.";
                IsInitialized = true;
                return;
            }

            var selectRoleData = JsonSerializer.Deserialize<SelectRolePageData>(serializedData);

            if (selectRoleData == null || selectRoleData.AvailableRoles.Count == 0)
            {
                _logger.LogWarning("No roles available in deserialized data");
                HasError = true;
                ErrorMessage = "No roles available. Please login again.";
                IsInitialized = true;
                return;
            }

            AvailableRoles = selectRoleData.AvailableRoles;
            VerificationToken = selectRoleData.VerificationToken;

            _logger.LogInformation("Loaded {Count} roles for selection from localStorage", AvailableRoles.Count);
            IsInitialized = true;
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "Error deserializing role data from localStorage");
            HasError = true;
            ErrorMessage = "Failed to load role data. Please login again.";
            IsInitialized = true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading available roles");
            HasError = true;
            ErrorMessage = "An error occurred while loading roles.";
            IsInitialized = true;
        }
    }

    /// <summary>
    /// Handles role selection and completes the authentication flow.
    /// </summary>
    /// <param name="role">The selected role</param>
    public async Task SelectRoleAsync(AvailableRole role)
    {
        if (role == null)
        {
            _logger.LogWarning("Attempted to select null role");
            return;
        }

        IsLoading = true;
        HasError = false;
        ErrorMessage = string.Empty;

        try
        {
            _logger.LogInformation("Selecting role: {RoleId} ({RoleName})", role.Id, role.Name);

            var result = await _authenticationService.SelectRoleAsync(role.Id, VerificationToken);

            if (result.IsSuccess)
            {
                _logger.LogInformation("Role selection successful, session initialized");

                // Clear role data from localStorage
                await _storageService.RemoveItemAsync(SelectRoleDataKey);

                // Store session marker for future session restoration
                await _storageService.SetItemAsync(SessionValidKey, DateTime.UtcNow.ToString("O"));

                // Navigate to workspace selection
                _navigationService.NavigateToSelectRole();
            }
            else
            {
                _logger.LogError("Failed to select role: {Error}", result.Error);
                HasError = true;
                ErrorMessage = result.Error ?? "Failed to select role. Please try again.";
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error selecting role");
            HasError = true;
            ErrorMessage = "An unexpected error occurred during role selection.";
        }
        finally
        {
            IsLoading = false;
        }
    }

    /// <summary>
    /// Gets the appropriate icon for a role based on its type.
    /// </summary>
    /// <param name="role">The role to get the icon for</param>
    /// <returns>MudBlazor icon string</returns>
    public static string GetRoleIcon(AvailableRole role)
    {
        if (role.IsAnalyst)
            return "Icons.Material.Filled.Engineering";
        if (role.IsSelfServiceMobile)
            return "Icons.Material.Filled.PhoneAndroid";
        return "Icons.Material.Filled.Person";
    }

    /// <summary>
    /// Handles logout and returns to login page.
    /// </summary>
    public async Task LogoutAsync()
    {
        _logger.LogInformation("User initiated logout from role selection");

        // Clear stored role data
        await _storageService.RemoveItemAsync(SelectRoleDataKey);

        await _authenticationService.LogoutAsync();
        _navigationService.NavigateToLogin();
    }
}
