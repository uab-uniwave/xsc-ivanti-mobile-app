using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Common;
using Application.Common.Models.SessonData;
using Application.Interfaces.State;
using Application.Common.Models.UserData;
using Application.Features.Authentication.DTOs;
using Application.Features.Workspaces.DTOs;
using Application.Features.Workspaces.Models;
using Application.Features.Workspaces.Models.FormDefaultData;
using Application.Features.Workspaces.Models.FormValidationListData;
using Application.Features.Workspaces.Models.FormViewData;
using Application.Features.Workspaces.Models.RoleWorkspaces;
using Application.Features.Workspaces.Models.WorkspaceData;
using Application.Features.Workspaces.Models.ValidatedSearch;
using Application.Features.Workspaces.Models.GridDataHandler;
using Application.Services;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Application.Features.Authentication.Models;

namespace Infrastructure.Ivanti;

/// <summary>
/// Ivanti API client implementation.
/// Handles all communication with Ivanti ServiceDesk API.
/// </summary>
public sealed class IvantiClient : IIvantiClient
{
    private readonly HttpClient _http;
    private readonly ILogger<IvantiClient> _logger;
    private readonly IvantiEndpoints _endpoints;
    private readonly IMapper _mapper;
    private readonly IIvantiStateService _stateService;

    private VerificationToken? _verificationToken;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = null,
        DefaultIgnoreCondition = JsonIgnoreCondition.Never,
        NumberHandling = JsonNumberHandling.AllowReadingFromString,
        UnknownTypeHandling = JsonUnknownTypeHandling.JsonElement,
    };

    public IvantiClient(
        HttpClient http,
        ILogger<IvantiClient> logger,
        IvantiEndpoints endpoints,
        IMapper mapper,
        IIvantiStateService stateService)
    {
        _http = http;
        _logger = logger;
        _endpoints = endpoints;
        _mapper = mapper;
        _stateService = stateService;
    }
    //=====================================================================
    // Get Verification Code
    //=====================================================================
    public async Task<Result<VerificationToken>>
        GetVerificationToken(CancellationToken ct)
    {
        try
        {
            _logger.LogInformation("Fetching verification token from login page...");
            _logger.LogInformation("Current cookie count: {Count}", CookieContainerManager.CookieCount);

            // GET request to retrieve the login page HTML
            var response = await _http.GetAsync(_endpoints.GetVerifiationToken, ct);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Failed to fetch login page. Status: {StatusCode}", response.StatusCode);
                return Result<VerificationToken>.Failure($"HTTP request failed with status {response.StatusCode}");
            }

            var html = await response.Content.ReadAsStringAsync(ct);

            // Extract __RequestVerificationToken from HTML
            var token = ExtractHiddenInputValue(html, "__RequestVerificationToken");
            if (string.IsNullOrEmpty(token))
            {
                _logger.LogError("Failed to extract __RequestVerificationToken from login page");
                return Result<VerificationToken>.Failure("Verification token not found in login page");
            }

            // Extract Tenant from HTML
            var tenant = ExtractHiddenInputValue(html, "Tenant");

            // Extract cookies from response
            var cookies = new Dictionary<string, string>();
            if (response.Headers.TryGetValues("Set-Cookie", out var cookieValues))
            {
                foreach (var cookieValue in cookieValues)
                {
                    var cookieParts = cookieValue.Split(';')[0].Split('=', 2);
                    if (cookieParts.Length == 2)
                    {
                        cookies[cookieParts[0].Trim()] = cookieParts[1].Trim();
                    }
                }
            }

            _verificationToken = new VerificationToken
            {
                Token = token,
                Tenant = tenant,
                Cookies = cookies,
                // Extract additional fields from HTML
                TenantUrl = ExtractHiddenInputValue(html, "TenantUrl"),
                SelectRoleUrl = ExtractHiddenInputValue(html, "SelectRoleUrl"),
                LoginUrl = ExtractHiddenInputValue(html, "LoginUrl"),
                ResetPasswordUrl = ExtractHiddenInputValue(html, "ResetPasswordUrl"),
                AuthenticateBioMetricUrl = ExtractHiddenInputValue(html, "authenticateBioMetricUrl"),
                ModelReturnUrl = ExtractHiddenInputValue(html, "ModelReturnUrl"),
                IsUrlSharedByTenants = ParseBool(ExtractHiddenInputValue(html, "IsUrlSharedByTenants")),
                ClientTimeOffset = ParseInt(ExtractHiddenInputValue(html, "ClientTimeOffset")),
                ClientTimezoneName = ExtractHiddenInputValue(html, "ClientTimezoneName"),
                ReturnUrl = ExtractHiddenInputValue(html, "ReturnUrl"),
                PreferredRole = ExtractHiddenInputValue(html, "PrefferedRole"),
                IsForgotPasswordAllowed = ParseBool(ExtractHiddenInputValue(html, "IsForgotPasswordAllowed")),
                IsFrame = ParseBool(ExtractHiddenInputValue(html, "IsFrame")),
                OpenIdSignIn = ExtractHiddenInputValue(html, "OpenIDSignIn"),
                SsoReturnUrl = ExtractHiddenInputValue(html, "SsoReturnUrl"),
                LoginStylesGeneratorUrl = ExtractHiddenInputValue(html, "LoginStylesGeneratorUrl")
            };

            _logger.LogInformation("Successfully extracted verification token. Tenant: {Tenant}", tenant);

            return Result<VerificationToken>.Success(_verificationToken);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP request error while getting verification token");
            return Result<VerificationToken>.Failure(ex.Message ?? "Unknown error");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception while getting verification token: {Message}", ex.Message);
            return Result<VerificationToken>.Failure(ex.Message ?? "Unknown error");
        }
    }

    //=====================================================================
    // Login - Post credentials and get SelectRole page
    //=====================================================================
    public async Task<Result<SelectRolePageData>> LoginAsync(LoginRequest request, CancellationToken ct)
    {
        try
        {
            _logger.LogInformation("Posting login credentials for user: {Username}", request.Username);
            _logger.LogInformation("Cookie count before POST: {Count}", CookieContainerManager.CookieCount);

            // Log cookies in container
            foreach (var cookieInfo in CookieContainerManager.GetCookieInfo())
            {
                _logger.LogInformation("Cookie in container: {CookieInfo}", cookieInfo);
            }

            // Build form content for login POST
            var formContent = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["__RequestVerificationToken"] = request.VerificationToken,
                ["UserName"] = request.Username,
                ["Password"] = request.Password,
                ["EnableBiometric"] = request.EnableBiometric,
                ["Tenant"] = request.Tenant ?? _verificationToken?.Tenant ?? "",
                ["IsUrlSharedByTenants"] = request.IsUrlSharedByTenants,
                ["ClientTimeOffset"] = request.ClientTimeOffset,
                ["ClientTimezoneName"] = request.ClientTimezoneName ?? "",
                ["ReturnUrl"] = request.ReturnUrl,
                ["PrefferedRole"] = request.PreferredRole,
                ["IsForgotPasswordAllowed"] = request.IsForgotPasswordAllowed,
                ["IsFrame"] = request.IsFrame,
                ["OpenIDSignIn"] = request.OpenIdSignIn,
                ["SsoReturnUrl"] = request.SsoReturnUrl
            });

            // Create request with explicit Cookie header to ensure AFT cookie is sent
            // The CookieContainer may not be sending cookies correctly due to domain/path matching issues
            var loginRequest = new HttpRequestMessage(HttpMethod.Post, _endpoints.GetVerifiationToken)
            {
                Content = formContent
            };

            // Manually add the AFT cookie from the verification token
            // This bypasses CookieContainer issues and ensures the cookie is sent
            if (_verificationToken?.Cookies != null && _verificationToken.Cookies.TryGetValue("AFT", out var aftCookie))
            {
                loginRequest.Headers.Add("Cookie", $"AFT={aftCookie}");
                _logger.LogInformation("Manually added AFT cookie to request header");
            }
            else
            {
                _logger.LogWarning("AFT cookie not found in verification token - login may fail");
            }

            // POST login - expect 302 redirect to SelectRole
            var response = await _http.SendAsync(loginRequest, ct);

            // Extract cookies from login response
            var cookies = ExtractCookiesFromResponse(response);

            // Handle redirect (302) or follow to SelectRole page
            string selectRoleHtml;
            if (response.StatusCode == System.Net.HttpStatusCode.Redirect || 
                response.StatusCode == System.Net.HttpStatusCode.Found)
            {
                var redirectUrl = response.Headers.Location?.ToString();
                _logger.LogInformation("Login successful, redirecting to: {RedirectUrl}", redirectUrl);

                // Follow redirect to get SelectRole page
                var selectRoleResponse = await _http.GetAsync(redirectUrl, ct);
                if (!selectRoleResponse.IsSuccessStatusCode)
                {
                    _logger.LogError("Failed to fetch SelectRole page. Status: {StatusCode}", selectRoleResponse.StatusCode);
                    return Result<SelectRolePageData>.Failure($"Failed to fetch SelectRole page: {selectRoleResponse.StatusCode}");
                }

                // Merge cookies from redirect response
                var redirectCookies = ExtractCookiesFromResponse(selectRoleResponse);
                foreach (var cookie in redirectCookies)
                {
                    cookies[cookie.Key] = cookie.Value;
                }

                selectRoleHtml = await selectRoleResponse.Content.ReadAsStringAsync(ct);
            }
            else if (response.IsSuccessStatusCode)
            {
                // Direct response (might be error page or SelectRole page)
                selectRoleHtml = await response.Content.ReadAsStringAsync(ct);
            }
            else
            {
                _logger.LogError("Login failed with status: {StatusCode}", response.StatusCode);
                return Result<SelectRolePageData>.Failure($"Login failed with status {response.StatusCode}");
            }

            // Parse SelectRole page HTML
            var selectRoleData = ParseSelectRolePage(selectRoleHtml, cookies);
            if (selectRoleData == null)
            {
                // Check if this is an "Access denied" error page
                if (selectRoleHtml.Contains("Access denied", StringComparison.OrdinalIgnoreCase))
                {
                    _logger.LogError("Login failed: Access denied. Token/cookie mismatch detected.");
                    return Result<SelectRolePageData>.Failure("Access denied. Please refresh and try again.");
                }

                // Check if we landed on the main application page (login successful, auto-redirected)
                if (selectRoleHtml.Contains("ServiceDesk", StringComparison.OrdinalIgnoreCase) ||
                    selectRoleHtml.Contains("MainLayout", StringComparison.OrdinalIgnoreCase) ||
                    selectRoleHtml.Contains("/HEAT/ServiceDesk", StringComparison.OrdinalIgnoreCase))
                {
                    _logger.LogInformation("Login successful - redirected directly to application (single role or auto-select)");
                    // Return empty roles list to indicate direct login (no role selection needed)
                    return Result<SelectRolePageData>.Success(new SelectRolePageData
                    {
                        VerificationToken = "",
                        AvailableRoles = new List<AvailableRole>(),
                        SelectedRoleId = "",
                        ReturnUrl = "",
                        Cookies = cookies
                    });
                }

                // Log the first 500 chars of the response for debugging
                var preview = selectRoleHtml.Length > 500 
                    ? selectRoleHtml[..500] + "..." 
                    : selectRoleHtml;
                _logger.LogWarning("Failed to parse SelectRole page. Response preview: {Preview}", preview);

                // Check for common page indicators
                _logger.LogInformation("Page contains 'login-role-row': {HasRoleRow}", 
                    selectRoleHtml.Contains("login-role-row", StringComparison.OrdinalIgnoreCase));
                _logger.LogInformation("Page contains '__RequestVerificationToken': {HasToken}", 
                    selectRoleHtml.Contains("__RequestVerificationToken", StringComparison.OrdinalIgnoreCase));
                _logger.LogInformation("Page contains 'SelectRole': {HasSelectRole}", 
                    selectRoleHtml.Contains("SelectRole", StringComparison.OrdinalIgnoreCase));

                return Result<SelectRolePageData>.Failure("Failed to parse SelectRole page - unexpected response");
            }

            _logger.LogInformation("Successfully parsed SelectRole page with {RoleCount} roles", 
                selectRoleData.AvailableRoles.Count);

            return Result<SelectRolePageData>.Success(selectRoleData);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP request error during login");
            return Result<SelectRolePageData>.Failure(ex.Message ?? "Unknown error");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception during login: {Message}", ex.Message);
            return Result<SelectRolePageData>.Failure(ex.Message ?? "Unknown error");
        }
    }

    //=====================================================================
    // SelectRole - Select a role and complete authentication
    //=====================================================================
    public async Task<Result<string>> SelectRoleAsync(string roleId, string verificationToken, CancellationToken ct)
    {
        try
        {
            _logger.LogInformation("Selecting role: {RoleId}", roleId);

            var formContent = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["__RequestVerificationToken"] = verificationToken,
                ["SelectedRole"] = roleId,
                ["ReturnUrl"] = ""
            });

            var response = await _http.PostAsync(_endpoints.SelectRole, formContent, ct);

            if (!response.IsSuccessStatusCode && 
                response.StatusCode != System.Net.HttpStatusCode.Redirect &&
                response.StatusCode != System.Net.HttpStatusCode.Found)
            {
                _logger.LogError("SelectRole failed with status: {StatusCode}", response.StatusCode);
                return Result<string>.Failure($"SelectRole failed with status {response.StatusCode}");
            }

            _logger.LogInformation("Successfully selected role: {RoleId}", roleId);
            return Result<string>.Success(roleId);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP request error during role selection");
            return Result<string>.Failure(ex.Message ?? "Unknown error");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception during role selection: {Message}", ex.Message);
            return Result<string>.Failure(ex.Message ?? "Unknown error");
        }
    }

    /// <summary>
    /// Extracts cookies from HTTP response headers.
    /// </summary>
    private static Dictionary<string, string> ExtractCookiesFromResponse(HttpResponseMessage response)
    {
        var cookies = new Dictionary<string, string>();
        if (response.Headers.TryGetValues("Set-Cookie", out var cookieValues))
        {
            foreach (var cookieValue in cookieValues)
            {
                var cookieParts = cookieValue.Split(';')[0].Split('=', 2);
                if (cookieParts.Length == 2)
                {
                    cookies[cookieParts[0].Trim()] = cookieParts[1].Trim();
                }
            }
        }
        return cookies;
    }

    /// <summary>
    /// Parses the SelectRole HTML page to extract available roles.
    /// </summary>
    private SelectRolePageData? ParseSelectRolePage(string html, Dictionary<string, string> cookies)
    {
        var verificationToken = ExtractHiddenInputValue(html, "__RequestVerificationToken");
        if (string.IsNullOrEmpty(verificationToken))
        {
            _logger.LogWarning("No verification token found in SelectRole page");
            return null;
        }

        var roles = new List<AvailableRole>();

        // Parse role rows: <div class="login-role-row" frs-id="Admin" frs-isAnalyst="False" frs-isSSM="False" frs-ndx="0">
        var rolePattern = @"<div[^>]*class\s*=\s*[""']login-role-row[^""']*[""'][^>]*frs-id\s*=\s*[""']([^""']+)[""'][^>]*frs-isAnalyst\s*=\s*[""']([^""']+)[""'][^>]*frs-isSSM\s*=\s*[""']([^""']+)[""'][^>]*frs-ndx\s*=\s*[""']([^""']+)[""'][^>]*>\s*([^<]+)";

        var matches = System.Text.RegularExpressions.Regex.Matches(html, rolePattern, 
            System.Text.RegularExpressions.RegexOptions.IgnoreCase | 
            System.Text.RegularExpressions.RegexOptions.Singleline);

        foreach (System.Text.RegularExpressions.Match match in matches)
        {
            var role = new AvailableRole
            {
                Id = match.Groups[1].Value.Trim(),
                IsAnalyst = ParseBool(match.Groups[2].Value),
                IsSelfServiceMobile = ParseBool(match.Groups[3].Value),
                Index = ParseInt(match.Groups[4].Value),
                Name = match.Groups[5].Value.Trim(),
                IsSelected = match.Value.Contains("login-role-row-selected")
            };
            roles.Add(role);
        }

        var selectedRoleId = ExtractHiddenInputValue(html, "SelectedRole");
        var returnUrl = ExtractHiddenInputValue(html, "ReturnUrl");

        return new SelectRolePageData
        {
            VerificationToken = verificationToken,
            AvailableRoles = roles,
            SelectedRoleId = selectedRoleId,
            ReturnUrl = returnUrl,
            Cookies = cookies
        };
    }

    /// <summary>
    /// Parses a string to boolean, defaulting to false.
    /// </summary>
    private static bool ParseBool(string? value)
    {
        if (string.IsNullOrEmpty(value)) return false;
        return value.Equals("true", StringComparison.OrdinalIgnoreCase) ||
               value.Equals("True", StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Parses a string to integer, defaulting to 0.
    /// </summary>
    private static int ParseInt(string? value)
    {
        if (string.IsNullOrEmpty(value)) return 0;
        return int.TryParse(value, out var result) ? result : 0;
    }

    /// <summary>
    /// Extracts the value of a hidden input field from HTML content.
    /// </summary>
    /// <param name="html">The HTML content to parse.</param>
    /// <param name="inputName">The name attribute of the input field.</param>
    /// <returns>The value of the input field, or null if not found.</returns>
    private static string? ExtractHiddenInputValue(string html, string inputName)
    {
        // Pattern: <input ... name="inputName" ... value="..." />
        // Handle both name="X" value="Y" and value="Y" name="X" orderings
        var patterns = new[]
        {
            $@"<input[^>]*\sname\s*=\s*[""']{inputName}[""'][^>]*\svalue\s*=\s*[""']([^""']*)[""'][^>]*\/?>",
            $@"<input[^>]*\svalue\s*=\s*[""']([^""']*)[""'][^>]*\sname\s*=\s*[""']{inputName}[""'][^>]*\/?>"
        };

        foreach (var pattern in patterns)
        {
            var match = System.Text.RegularExpressions.Regex.Match(html, pattern, 
                System.Text.RegularExpressions.RegexOptions.IgnoreCase | 
                System.Text.RegularExpressions.RegexOptions.Singleline);

            if (match.Success)
            {
                return match.Groups[1].Value;
            }
        }

        return null;
    }
    //=====================================================================
    // Session and authentication endpoints
    //=====================================================================
    public async Task<Result<SessionData>>
        InitializeSessionAsync(CancellationToken ct)
    {

        try {

            var request = new InitializeSessionRequest();
            var response = await PostAsync<InitializeSessionResponse>(_endpoints.InitializeSession,
                request,
                ct);

            if (response.IsFailure || response.Value == null)
            {
                _logger.LogError("Failed to initialize session: {Error}", response.Error);
                return Result<SessionData>.Failure(response.Error ?? "Unknown error");
            }
            _stateService.SessionData = _mapper.Map<SessionData>(response.Value.D);


            return Result<SessionData>.Success(_stateService.SessionData);
        }
        catch (JsonSerializationException ex)
        {
            _logger.LogError(ex, "JSON deserialization error while initialize session");
            return Result<SessionData>.Failure(ex.Message ?? "Unknown error");
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP request error while initialize session");
            return Result<SessionData>.Failure(ex.Message ?? "Unknown error");
        }
        catch (Exception ex)
        {

            _logger.LogError("Exception while initialize session: {Message}", ex.Message);
            return Result<SessionData>.Failure(ex.Message ?? "Unknown error");
        }

    }

    //=====================================================================
    // Get User Data
    //=====================================================================
    public async Task<Result<UserData>>
        GetUserDataAsync(CancellationToken ct)
    {
        try {
            _logger.LogInformation("Initializing user data...");

            if (_stateService.SessionData == null)
            {
                _logger.LogError("Session data is null, cannot get user data");
                return Result<UserData>.Failure("Session not initialized");
            }

            var request = new GetUserDataRequest() {
                TimeZoneOffset = GetTimezoneOffset(_stateService.SessionData.TimezoneName),
                CsrfToken = _stateService.SessionData.SessionCsrfToken

            };

            var response = await PostAsync<GetUserDataResponse>(_endpoints.GetUserData,
                request,
                ct);

            if (response.IsFailure || response.Value == null)
            {
                _logger.LogError("Failed to get user data: {Error}", response.Error);
                return Result<UserData>.Failure(response.Error ?? "Unknown error");
            }

            _stateService.UserData = _mapper.Map<UserData>(response.Value.D);

            return Result<UserData>.Success(_stateService.UserData);

        }
        catch (JsonSerializationException ex)
        {

            _logger.LogError(ex, "JSON deserialization error while geting user data");
            return Result<UserData>.Failure(ex.Message ?? "Unknown error");
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP request error while geting user data");
            return Result<UserData>.Failure(ex.Message ?? "Unknown error");
        }
        catch (Exception ex)
        {

            _logger.LogError("Exception while geting user data: {Message}", ex.Message);
            return Result<UserData>.Failure(ex.Message ?? "Unknown error");
        }
    }
    //=====================================================================
    //  Roles workspaces 
    //=====================================================================
    public async Task<Result<RoleWorkspaces>>
        GetRoleWorkspacesAsync(CancellationToken ct)
    {
        try {
                if (_stateService.UserData == null)
                {
                    _logger.LogError("User data is null, cannot get role workspaces");
                    return Result<RoleWorkspaces>.Failure("User data not initialized");
                }

                if (_stateService.SessionData == null)
                {
                    _logger.LogError("Session data is null, cannot get role workspaces");
                    return Result<RoleWorkspaces>.Failure("Session not initialized");
                }

                var request = new RoleWorkspacesRequest()
                {
                    SRole = _stateService.UserData.UserRole,
                    CsrfToken = _stateService.SessionData.SessionCsrfToken

                };
                var response = await PostAsync<GetRoleWorkspacesResponse>(_endpoints.GetRoleWorkspaces,
                request,
                ct);

                if (response.IsFailure || response.Value == null)
                {
                    _logger.LogError("Failed to get role workspaces: {Error}", response.Error);
                    return Result<RoleWorkspaces>.Failure(response.Error ?? "Unknown error");
                }


            _stateService.RoleWorkspaces = _mapper.Map<RoleWorkspaces>(response.Value.D);

            return Result<RoleWorkspaces>.Success(_stateService.RoleWorkspaces);
        }
        catch (JsonSerializationException ex)
        {

            _logger.LogError(ex, "JSON deserialization error while geting role workspaces");
            return Result<RoleWorkspaces>.Failure(ex.Message ?? "Unknown error");
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP request error while geting role workspaces");
            return Result<RoleWorkspaces>.Failure(ex.Message ?? "Unknown error");
        }
        catch (Exception ex)
        {

            _logger.LogError("Exception while getting role workspaces: {Message}", ex.Message);
            return Result<RoleWorkspaces>.Failure(ex.Message ?? "Unknown error");
        }

    }

    //=====================================================================
    // Load ALL Workspaces Basic Data (WorkspaceData only for each)
    //=====================================================================

    /// <summary>
    /// Loads WorkspaceData for ALL workspaces.
    /// Called after GetRoleWorkspaces to get basic data for each workspace.
    /// </summary>
    public async Task<Result<List<WorkspaceFullData>>> LoadAllWorkspacesBasicDataAsync(CancellationToken ct)
    {
        try
        {
            if (_stateService.SessionData == null)
            {
                _logger.LogError("Session data is null, cannot load workspaces");
                return Result<List<WorkspaceFullData>>.Failure("Session not initialized");
            }

            if (_stateService.RoleWorkspaces == null || !_stateService.RoleWorkspaces.Workspaces.Any())
            {
                _logger.LogError("Role workspaces is null or empty");
                return Result<List<WorkspaceFullData>>.Failure("Role workspaces not initialized");
            }

            var allWorkspacesData = new List<WorkspaceFullData>();
            var workspaces = _stateService.RoleWorkspaces.Workspaces;

            _logger.LogInformation("Loading WorkspaceData for {Count} workspaces...", workspaces.Count);

            foreach (var workspace in workspaces)
            {
                var workspaceFullData = new WorkspaceFullData
                {
                    Workspace = workspace
                };

                try
                {
                    _logger.LogInformation("Loading WorkspaceData for: {WorkspaceName} (ID: {WorkspaceId})", 
                        workspace.Name, workspace.Id);

                    // Get workspace data for this workspace
                    var workspaceDataResult = await GetWorkspaceDataAsync(workspace, ct);

                    if (workspaceDataResult.IsSuccess && workspaceDataResult.Value != null)
                    {
                        workspaceFullData.WorkspaceData = workspaceDataResult.Value;
                        _logger.LogInformation("Successfully loaded WorkspaceData for: {WorkspaceName}", workspace.Name);
                    }
                    else
                    {
                        workspaceFullData.ErrorMessage = workspaceDataResult.Error;
                        _logger.LogWarning("Failed to load WorkspaceData for {WorkspaceName}: {Error}", 
                            workspace.Name, workspaceDataResult.Error);
                    }
                }
                catch (Exception ex)
                {
                    workspaceFullData.ErrorMessage = ex.Message;
                    _logger.LogError(ex, "Error loading WorkspaceData for {WorkspaceName}", workspace.Name);
                }

                allWorkspacesData.Add(workspaceFullData);
            }

            // Store in state service
            _stateService.AllWorkspacesData = allWorkspacesData;

            _logger.LogInformation("Loaded WorkspaceData for {Loaded}/{Total} workspaces", 
                allWorkspacesData.Count(w => w.WorkspaceData != null), allWorkspacesData.Count);

            return Result<List<WorkspaceFullData>>.Success(allWorkspacesData);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading all workspaces basic data");
            return Result<List<WorkspaceFullData>>.Failure(ex.Message ?? "Unknown error");
        }
    }

    //=====================================================================
    // Load SPECIFIC Workspace Form Data (when navigating to workspace)
    //=====================================================================

    /// <summary>
    /// Loads complete form data for a specific workspace by ID.
    /// Called when user navigates to a workspace (e.g., Incidents).
    /// </summary>
    public async Task<Result<WorkspaceFullData>> LoadWorkspaceFormDataAsync(string workspaceId, CancellationToken ct)
    {
        var workspaceFullData = _stateService.AllWorkspacesData
            .FirstOrDefault(w => w.Workspace.Id.Equals(workspaceId, StringComparison.OrdinalIgnoreCase));

        if (workspaceFullData == null)
        {
            _logger.LogError("Workspace not found: {WorkspaceId}", workspaceId);
            return Result<WorkspaceFullData>.Failure($"Workspace not found: {workspaceId}");
        }

        return await LoadWorkspaceFormDataInternalAsync(workspaceFullData, ct);
    }

    /// <summary>
    /// Loads complete form data for a specific workspace by name.
    /// Called when user navigates to a workspace (e.g., Incidents).
    /// </summary>
    public async Task<Result<WorkspaceFullData>> LoadWorkspaceFormDataByNameAsync(string workspaceName, CancellationToken ct)
    {
        var workspaceFullData = _stateService.AllWorkspacesData
            .FirstOrDefault(w => w.Workspace.Name.Equals(workspaceName, StringComparison.OrdinalIgnoreCase));

        if (workspaceFullData == null)
        {
            _logger.LogError("Workspace not found: {WorkspaceName}", workspaceName);
            return Result<WorkspaceFullData>.Failure($"Workspace not found: {workspaceName}");
        }

        return await LoadWorkspaceFormDataInternalAsync(workspaceFullData, ct);
    }

    /// <summary>
    /// Internal method to load form data for a workspace.
    /// </summary>
    private async Task<Result<WorkspaceFullData>> LoadWorkspaceFormDataInternalAsync(
        WorkspaceFullData workspaceFullData, 
        CancellationToken ct)
    {
        try
        {
            var workspace = workspaceFullData.Workspace;
            var workspaceData = workspaceFullData.WorkspaceData;

            if (workspaceData == null)
            {
                _logger.LogError("WorkspaceData not loaded for {WorkspaceName}", workspace.Name);
                return Result<WorkspaceFullData>.Failure("WorkspaceData not loaded");
            }

            _logger.LogInformation("Loading form data for workspace: {WorkspaceName}", workspace.Name);

            // 1. FormViewData
            _logger.LogInformation("Step 1/4: Loading FormViewData for {WorkspaceName}...", workspace.Name);
            var formViewResult = await FindFormViewDataAsync(workspace, workspaceData, ct);
            if (formViewResult.IsSuccess && formViewResult.Value != null)
            {
                workspaceFullData.FormViewData = formViewResult.Value;
            }
            else
            {
                _logger.LogWarning("Failed to load FormViewData: {Error}", formViewResult.Error);
            }

            // 2. FormDefaultData (requires FormViewData)
            if (workspaceFullData.FormViewData != null)
            {
                _logger.LogInformation("Step 2/4: Loading FormDefaultData for {WorkspaceName}...", workspace.Name);
                var formDefaultResult = await GetFormDefaultDataAsync(workspace, workspaceData, workspaceFullData.FormViewData, ct);
                if (formDefaultResult.IsSuccess && formDefaultResult.Value != null)
                {
                    workspaceFullData.FormDefaultData = formDefaultResult.Value;
                }
                else
                {
                    _logger.LogWarning("Failed to load FormDefaultData: {Error}", formDefaultResult.Error);
                }

                // 3. FormValidationListData (requires FormViewData)
                _logger.LogInformation("Step 3/4: Loading FormValidationListData for {WorkspaceName}...", workspace.Name);
                var validationListResult = await GetFormValidationListDataAsync(workspace, workspaceFullData.FormViewData, ct);
                if (validationListResult.IsSuccess && validationListResult.Value != null)
                {
                    workspaceFullData.FormValidationListData = validationListResult.Value;
                }
                else
                {
                    _logger.LogWarning("Failed to load FormValidationListData: {Error}", validationListResult.Error);
                }
            }

            // 4. ValidatedSearches
            _logger.LogInformation("Step 4/4: Loading ValidatedSearches for {WorkspaceName}...", workspace.Name);
            var searchesResult = await GetValidatedSearchAsync(workspace, workspaceData, ct);
            if (searchesResult.IsSuccess && searchesResult.Value != null)
            {
                workspaceFullData.ValidatedSearches = searchesResult.Value;
            }
            else
            {
                _logger.LogWarning("Failed to load ValidatedSearches: {Error}", searchesResult.Error);
            }

            // Set as current workspace
            _stateService.CurrentWorkspace = workspaceFullData;

            _logger.LogInformation("Completed loading form data for workspace: {WorkspaceName}. IsFullyLoaded: {IsFullyLoaded}", 
                workspace.Name, workspaceFullData.IsFullyLoaded);

            return Result<WorkspaceFullData>.Success(workspaceFullData);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading form data for workspace");
            return Result<WorkspaceFullData>.Failure(ex.Message ?? "Unknown error");
        }
    }

    //=====================================================================
    // Workspace Data Methods with Explicit Parameters
    //=====================================================================

    /// <summary>
    /// Gets workspace data for a specific workspace.
    /// </summary>
    public async Task<Result<WorkspaceData>> GetWorkspaceDataAsync(Workspace workspace, CancellationToken ct)
    {
        try
        {
            var request = new GetWorkspaceDataRequest()
            {
                ObjectId = workspace.Id,
                LayoutName = workspace.LayoutName,
                CsrfToken = _stateService.SessionData!.SessionCsrfToken,
            };

            _logger.LogInformation("Getting workspace data for: {WorkspaceName} (ObjectId: {ObjectId})",
                workspace.Name, workspace.Id);

            var response = await PostAsync<GetWorkspaceDataResponse>(_endpoints.GetWorkspaceData, request, ct);

            if (response.IsFailure || response.Value == null)
            {
                _logger.LogError("Failed to get workspace data: {Error}", response.Error);
                return Result<WorkspaceData>.Failure(response.Error ?? "Unknown error");
            }

            return Result<WorkspaceData>.Success(_mapper.Map<WorkspaceData>(response.Value.D));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception getting workspace data for {WorkspaceName}", workspace.Name);
            return Result<WorkspaceData>.Failure(ex.Message ?? "Unknown error");
        }
    }

    /// <summary>
    /// Gets form view data for a specific workspace.
    /// </summary>
    public async Task<Result<FormViewData>> FindFormViewDataAsync(
        Workspace workspace, 
        WorkspaceData workspaceData, 
        CancellationToken ct)
    {
        try
        {
            var request = new FindFormViewDataRequest()
            {
                CreatedViewsOnClient = new(),
                IsNewRecord = true,
                LayoutName = workspace.LayoutName,
                ObjectId = workspaceData.ObjectId,
                ViewName = workspaceData.LayoutData?.OneNewRecordView ?? "formView",
                CsrfToken = _stateService.SessionData!.SessionCsrfToken
            };

            var response = await PostAsync<FindFormViewDataResponse>(_endpoints.FindFormViewData, request, ct);

            if (response.IsFailure || response.Value == null)
            {
                return Result<FormViewData>.Failure(response.Error ?? "Unknown error");
            }

            return Result<FormViewData>.Success(_mapper.Map<FormViewData>(response.Value.D));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception finding form view data for {WorkspaceName}", workspace.Name);
            return Result<FormViewData>.Failure(ex.Message ?? "Unknown error");
        }
    }

    /// <summary>
    /// Gets form default data for a specific workspace.
    /// </summary>
    public async Task<Result<FormDefaultData>> GetFormDefaultDataAsync(
        Workspace workspace,
        WorkspaceData workspaceData,
        FormViewData formViewData,
        CancellationToken ct)
    {
        try
        {
            var request = new GetFormDefaultDataRequest()
            {
                FormName = string.Join(".", workspace.Name, _stateService.UserData!.UserRole, "NewForm"),
                LayoutName = workspace.LayoutName,
                MasterData = null,
                ObjectId = workspaceData.ObjectId,
                ObjectType = workspace.Id,
                Overridings = null,
                ViewName = formViewData.ViewName,
                CsrfToken = _stateService.SessionData!.SessionCsrfToken,
                DependentInfo = null,
            };

            var response = await PostAsync<GetFormDefaultDataResponse>(_endpoints.GetFormDefaultData, request, ct);

            if (response.IsFailure || response.Value == null)
            {
                return Result<FormDefaultData>.Failure(response.Error ?? "Unknown error");
            }

            return Result<FormDefaultData>.Success(_mapper.Map<FormDefaultData>(response.Value.D));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception getting form default data for {WorkspaceName}", workspace.Name);
            return Result<FormDefaultData>.Failure(ex.Message ?? "Unknown error");
        }
    }

    /// <summary>
    /// Gets form validation list data for a specific workspace.
    /// </summary>
    public async Task<Result<FormValidationListData>> GetFormValidationListDataAsync(
        Workspace workspace,
        FormViewData formViewData,
        CancellationToken ct)
    {
        try
        {
            var request = new GetFormValidationListDataRequest()
            {
                FormValidationList = new FormValidationList()
                {
                    NamedValidators = System.Text.Json.JsonSerializer.Serialize(
                        formViewData?.FormDef?.TableMeta?.ValidatedFields, JsonOptions),
                    ValidatorsOverride = "{}",
                    MasterFormValues = null,
                    ObjectId = workspace.Id
                },
                CsrfToken = _stateService.SessionData!.SessionCsrfToken
            };

            var response = await PostAsync<GetFormValidationListDataResponse>(_endpoints.GetFormValidationListData, request, ct);

            if (response.IsFailure || response.Value == null)
            {
                return Result<FormValidationListData>.Failure(response.Error ?? "Unknown error");
            }

            return Result<FormValidationListData>.Success(_mapper.Map<FormValidationListData>(response.Value.D));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception getting form validation list data for {WorkspaceName}", workspace.Name);
            return Result<FormValidationListData>.Failure(ex.Message ?? "Unknown error");
        }
    }

    /// <summary>
    /// Gets validated searches for a specific workspace.
    /// </summary>
    public async Task<Result<List<ValidatedSearch>>> GetValidatedSearchAsync(
        Workspace workspace,
        WorkspaceData workspaceData,
        CancellationToken ct)
    {
        try
        {
            var favoriteSearches = workspaceData?.SearchData?.Favorites;

            if (favoriteSearches == null || !favoriteSearches.Any())
            {
                _logger.LogWarning("No favorite searches found for workspace: {WorkspaceName}", workspace.Name);
                return Result<List<ValidatedSearch>>.Success(new List<ValidatedSearch>());
            }

            var validatedSearches = new List<ValidatedSearch>();

            foreach (var favorite in favoriteSearches)
            {
                if (string.IsNullOrEmpty(favorite.Id)) continue;

                try
                {
                    var request = new GetValideatedSearchRequest()
                    {
                        SearchId = Guid.Parse(favorite.Id),
                        ObjectId = workspaceData!.ObjectId,
                        LayoutName = workspace.LayoutName,
                        CsrfToken = _stateService.SessionData!.SessionCsrfToken
                    };

                    var response = await PostAsync<GetValideatedSearchResponse>(_endpoints.GetValidatedSearch, request, ct);

                    if (response.IsSuccess && response.Value != null)
                    {
                        validatedSearches.Add(_mapper.Map<ValidatedSearch>(response.Value.D));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Error loading validated search: {SearchId}", favorite.Id);
                }
            }

            return Result<List<ValidatedSearch>>.Success(validatedSearches);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception getting validated searches for {WorkspaceName}", workspace.Name);
            return Result<List<ValidatedSearch>>.Failure(ex.Message ?? "Unknown error");
        }
    }

    //=====================================================================
    // Grid Data Handler
    //=====================================================================

    /// <summary>
    /// Fetches grid data for a specific workspace and search.
    /// </summary>
    public async Task<Result<GridDataHandler>> GetGridDataAsync(
        string workspaceId,
        Guid searchId,
        int skip = 0,
        int take = 50,
        CancellationToken ct = default)
    {
        try
        {
            var workspaceData = _stateService.GetWorkspaceById(workspaceId);
            if (workspaceData?.WorkspaceData == null)
            {
                return Result<GridDataHandler>.Failure($"Workspace not found or not loaded: {workspaceId}");
            }

            var request = new GridDataHandlerRequest
            {
                SearchId = searchId,
                ObjectId = workspaceData.WorkspaceData.ObjectId,
                LayoutName = workspaceData.Workspace.LayoutName,
                Skip = skip,
                Take = take,
                CsrfToken = _stateService.SessionData!.SessionCsrfToken
            };

            var response = await PostAsync<GridDataHandlerResponse>(_endpoints.GridDataHandler, request, ct);

            if (response.IsFailure || response.Value == null)
            {
                return Result<GridDataHandler>.Failure(response.Error ?? "Unknown error");
            }

            return Result<GridDataHandler>.Success(_mapper.Map<GridDataHandler>(response.Value.D));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception getting grid data");
            return Result<GridDataHandler>.Failure(ex.Message ?? "Unknown error");
        }
    }

    //=====================================================================
    // Legacy Methods (deprecated - for backward compatibility)
    //=====================================================================

    [Obsolete("Use GetWorkspaceDataAsync(Workspace, CancellationToken) instead")]
    public async Task<Result<WorkspaceData>> GetWorkspaceDataAsync(CancellationToken ct)
    {
        var firstWorkspace = _stateService.RoleWorkspaces?.Workspaces?.FirstOrDefault();
        if (firstWorkspace == null)
            return Result<WorkspaceData>.Failure("No workspaces available");

        var result = await GetWorkspaceDataAsync(firstWorkspace, ct);
        if (result.IsSuccess)
        {
#pragma warning disable CS0618
            _stateService.WorkspaceData = result.Value;
#pragma warning restore CS0618
        }
        return result;
    }

    [Obsolete("Use FindFormViewDataAsync(Workspace, WorkspaceData, CancellationToken) instead")]
    public async Task<Result<FormViewData>> FindFormViewDataAsync(CancellationToken ct)
    {
        var workspace = _stateService.RoleWorkspaces?.Workspaces?.FirstOrDefault();
#pragma warning disable CS0618
        var workspaceData = _stateService.WorkspaceData;
#pragma warning restore CS0618

        if (workspace == null || workspaceData == null)
            return Result<FormViewData>.Failure("Workspace or WorkspaceData not available");

        var result = await FindFormViewDataAsync(workspace, workspaceData, ct);
        if (result.IsSuccess)
        {
#pragma warning disable CS0618
            _stateService.FormViewData = result.Value;
#pragma warning restore CS0618
        }
        return result;
    }

    [Obsolete("Use GetFormDefaultDataAsync(Workspace, WorkspaceData, FormViewData, CancellationToken) instead")]
    public async Task<Result<FormDefaultData>> GetFormDefaultDataAsync(CancellationToken ct)
    {
        var workspace = _stateService.RoleWorkspaces?.Workspaces?.FirstOrDefault();
#pragma warning disable CS0618
        var workspaceData = _stateService.WorkspaceData;
        var formViewData = _stateService.FormViewData;
#pragma warning restore CS0618

        if (workspace == null || workspaceData == null || formViewData == null)
            return Result<FormDefaultData>.Failure("Required data not available");

        var result = await GetFormDefaultDataAsync(workspace, workspaceData, formViewData, ct);
        if (result.IsSuccess)
        {
#pragma warning disable CS0618
            _stateService.FormDefaultData = result.Value;
#pragma warning restore CS0618
        }
        return result;
    }

    [Obsolete("Use GetFormValidationListDataAsync(Workspace, FormViewData, CancellationToken) instead")]
    public async Task<Result<FormValidationListData>> GetFormValidationListDataAsync(CancellationToken ct)
    {
        var workspace = _stateService.RoleWorkspaces?.Workspaces?.FirstOrDefault();
#pragma warning disable CS0618
        var formViewData = _stateService.FormViewData;
#pragma warning restore CS0618

        if (workspace == null || formViewData == null)
            return Result<FormValidationListData>.Failure("Required data not available");

        var result = await GetFormValidationListDataAsync(workspace, formViewData, ct);
        if (result.IsSuccess)
        {
#pragma warning disable CS0618
            _stateService.FormValidationListData = result.Value;
#pragma warning restore CS0618
        }
        return result;
    }

    [Obsolete("Use GetValidatedSearchAsync(Workspace, WorkspaceData, CancellationToken) instead")]
    public async Task<Result<List<ValidatedSearch>>> GetValidatedSearchAsync(CancellationToken ct)
    {
        var workspace = _stateService.RoleWorkspaces?.Workspaces?.FirstOrDefault();
#pragma warning disable CS0618
        var workspaceData = _stateService.WorkspaceData;
#pragma warning restore CS0618

        if (workspace == null || workspaceData == null)
            return Result<List<ValidatedSearch>>.Failure("Required data not available");

        return await GetValidatedSearchAsync(workspace, workspaceData, ct);
    }

    [Obsolete("Use GetGridDataAsync with workspaceId parameter")]
    public async Task<Result<GridDataHandler>> GetGridDataAsync(
        Guid searchId,
        int skip = 0,
        int take = 50,
        CancellationToken ct = default)
    {
        var workspace = _stateService.RoleWorkspaces?.Workspaces?.FirstOrDefault();
        if (workspace == null)
            return Result<GridDataHandler>.Failure("No workspace available");

        return await GetGridDataAsync(workspace.Id, searchId, skip, take, ct);
    }

    //=====================================================================
    // Helper Methods
    //=====================================================================

    private async Task<Result<T>> PostAsync<T>(string endpoint, object request, CancellationToken ct)
    {
        try
        {
            var response = await _http.PostAsJsonAsync(endpoint, request, JsonOptions, ct);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("HTTP request failed with status {StatusCode}", response.StatusCode);
                return Result<T>.Failure($"HTTP request failed with status {response.StatusCode}");
            }

            var json = await response.Content.ReadAsStringAsync(ct);
            var content = System.Text.Json.JsonSerializer.Deserialize<T>(json, JsonOptions);
            return Result<T>.Success(content!);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error making POST request to {Endpoint}", endpoint);
            return Result<T>.Failure($"Error: {ex.Message}");
        }
    }

    private int GetTimezoneOffset(string? timezoneName)
    {
        if (string.IsNullOrWhiteSpace(timezoneName))
            return 0;

        try
        {
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timezoneName);
            var utcOffset = timeZoneInfo.GetUtcOffset(DateTime.UtcNow);
            return -(int)utcOffset.TotalMinutes;
        }
        catch
        {
            return 0;
        }
    }
}