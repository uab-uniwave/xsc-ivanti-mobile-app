using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Common;
using Application.Common.Models.SessonData;
using Application.Common.Models.UserData;
using Application.Features.Authentication.DTOs;
using Application.Features.Workspaces.DTOs;
using Application.Features.Workspaces.Models.FormDefaultData;
using Application.Features.Workspaces.Models.FormValidationListData;
using Application.Features.Workspaces.Models.FormViewData;
using Application.Features.Workspaces.Models.RoleWorkspaces;
using Application.Features.Workspaces.Models.WorkspaceData;
using Application.Features.Workspaces.Models.ValidatedSearch;
using Application.Services;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Infrastructure.Ivanti;

/// <summary>
/// Legacy Ivanti API client implementation.
/// This is the internal implementation used by IvantiServiceAdapter.
/// Do not use this directly - use IIvantiService interface instead.
/// </summary>
public sealed class IvantiClient :IIvantiClient
{
    private readonly HttpClient _http;
    private readonly ILogger<IvantiClient> _logger;
    private readonly IvantiEndpoints _endpoints;
    private readonly IMapper _mapper;

    private SessionData _sessionData;
    private UserData _userData;
    private RoleWorkspaces _roleWorkspaces;
    private WorkspaceData _workspaceData;
    private FormViewData _formViewData;
    private FormDefaultData _formDefaultData;
    private FormValidationListData _formValidationListData;   
    
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
        IMapper mapper)
    {
        _http = http;
        _logger = logger;
        _endpoints = endpoints;
        _mapper = mapper;
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
            _sessionData = _mapper.Map<SessionData>(response.Value.D);


            return Result<SessionData>.Success(_sessionData);
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

            var request = new GetUserDataRequest() {
                TimeZoneOffset = GetTimezoneOffset(_sessionData.TimezoneName),
                CsrfToken = _sessionData.SessionCsrfToken

            };

            var response = await PostAsync<GetUserDataResponse>(_endpoints.GetUserData,
                request,
                ct);

            if (response.IsFailure || response.Value == null)
            {
                _logger.LogError("Failed to get user data: {Error}", response.Error);
                return Result<UserData>.Failure(response.Error ?? "Unknown error");
            }

            _userData = _mapper.Map<UserData>(response.Value.D);

            return Result<UserData>.Success(_userData);

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

                var request = new RoleWorkspacesRequest()
                {
                    SRole = _userData.UserRole,
                    CsrfToken = _sessionData.SessionCsrfToken

                };
                var response = await PostAsync<GetRoleWorkspacesResponse>(_endpoints.GetRoleWorkspaces,
                request,
                ct);

                if (response.IsFailure || response.Value == null)
                {
                    _logger.LogError("Failed to get role workspaces: {Error}", response.Error);
                    return Result<RoleWorkspaces>.Failure(response.Error ?? "Unknown error");
                }


            _roleWorkspaces = _mapper.Map<RoleWorkspaces>(response.Value.D);

            return Result<RoleWorkspaces>.Success(_roleWorkspaces);
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

            _logger.LogError("Exception while geting role workspaces: {Message}", ex.Message);
            return Result<RoleWorkspaces>.Failure(ex.Message ?? "Unknown error");
        }

    }
    //=====================================================================
    // Workspaces data
    //=====================================================================
    public async Task<Result<WorkspaceData>>
        GetWorkspaceDataAsync(CancellationToken ct)
    {
        try
        {
            var request = new GetWorkspaceDataRequest()
            {
                SearchId = null,
                CsrfToken = _sessionData.SessionCsrfToken,
            };

            var response = await PostAsync<GetValideatedSearchResponse>(_endpoints.GetValidatedSearch,
                request,
                ct);

            if (response.IsFailure || response.Value == null)
            {
                _logger.LogError("Failed to get workspace data: {Error}", response.Error);
                return Result<WorkspaceData>.Failure(response.Error ?? "Unknown error");
            }
                
            _workspaceData = _mapper.Map<WorkspaceData>(response.Value.D);

            return Result<WorkspaceData>.Success(_workspaceData);
        }
        catch (JsonSerializationException ex)
        {

            _logger.LogError(ex, "JSON deserialization error while geting workspace data");
            return Result<WorkspaceData>.Failure(ex.Message ?? "Unknown error");
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP request error while geting workspace data");
            return Result<WorkspaceData>.Failure(ex.Message ?? "Unknown error");
        }
        catch (Exception ex)
        {

            _logger.LogError("Exception while  geting workspace data: {Message}", ex.Message);
            return Result<WorkspaceData>.Failure(ex.Message ?? "Unknown error");
        }
    }
    //=====================================================================
    // Find Form View Data
    //=====================================================================
    public async Task<Result<FormViewData>>
        FindFormViewDataAsync(CancellationToken ct)
    {
        try
        {

                var request = new FindFormViewDataRequest()
            {
                CreatedViewsOnClient = new(),
                IsNewRecord = true,
                LayoutName = _roleWorkspaces.Workspaces.FirstOrDefault()?.LayoutName,
                ObjectId = _workspaceData.ObjectId,
                ViewName = _workspaceData.LayoutData?.OneNewRecordView ?? "formView",
                CsrfToken = _sessionData.SessionCsrfToken
            };


            var response = await PostAsync<FindFormViewDataResponse>(_endpoints.FindFormViewData,
         request,
         ct);

            if (response.IsFailure || response.Value == null)
            {
                _logger.LogError("Failed to get workspace data: {Error}", response.Error);
                return Result<FormViewData>.Failure(response.Error ?? "Unknown error");
            }

            _formViewData = _mapper.Map<FormViewData>(response.Value.D);

            return Result<FormViewData>.Success(_formViewData);
        }
        catch (JsonSerializationException ex)
        {

            _logger.LogError(ex, "JSON deserialization error while finding form view data");
            return Result<FormViewData>.Failure(ex.Message ?? "Unknown error");
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP request error while finding form view data");
            return Result<FormViewData>.Failure(ex.Message ?? "Unknown error");
        }
        catch (Exception ex)
        {

            _logger.LogError("Exception while finding form view data: {Message}", ex.Message);
            return Result<FormViewData>.Failure(ex.Message ?? "Unknown error");
        }
    }

    //=====================================================================
    //  Form Default Data
    //=====================================================================


    public async Task<Result<FormDefaultData>>
        GetFormDefaultDataAsync(CancellationToken ct)
    {
        try
        {


            var request = new GetFormDefaultDataRequest()
            {
                
                FormName = string.Join(".",_roleWorkspaces.Workspaces.FirstOrDefault()?.Name,_userData.UserRole,"NewForm"),
                LayoutName = _roleWorkspaces.Workspaces.FirstOrDefault()?.LayoutName,
                MasterData = null,
                ObjectId = _workspaceData.ObjectId,
                ObjectType = _roleWorkspaces.Workspaces.FirstOrDefault()?.Id,
                Overridings = null,
                ViewName = _formViewData.ViewName,
                CsrfToken = _sessionData.SessionCsrfToken,
                DependentInfo = null,

            };
            var response = await PostAsync<GetFormDefaultDataResponse>(_endpoints.GetFormDefaultData,
  request,
  ct);

            if (response.IsFailure || response.Value == null)
            {
                _logger.LogError("Failed to get workspace data: {Error}", response.Error);
                return Result<FormDefaultData>.Failure(response.Error ?? "Unknown error");
            }

            _formDefaultData = _mapper.Map<FormDefaultData>(response.Value.D);

            return Result<FormDefaultData>.Success(_formDefaultData);

        }
        catch (JsonSerializationException ex)
        {

            _logger.LogError(ex, "JSON deserialization error while getting form default data ");
            return Result<FormDefaultData>.Failure(ex.Message ?? "Unknown error");
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP request error while  getting form default data");
            return Result<FormDefaultData>.Failure(ex.Message ?? "Unknown error");
        }
        catch (Exception ex)
        {

            _logger.LogError("Exception geting form default data : {Message}", ex.Message);
            return Result<FormDefaultData>.Failure(ex.Message ?? "Unknown error");
        }
    }


    //=====================================================================
    //  Form Validation ListData
    //=====================================================================


    public async Task<Result<FormValidationListData>>
        GetFormValidationListDataAsync(CancellationToken ct)
    {
        try
        {



            var request = new GetFormValidationListDataRequest() {

                FormValidationList = new FormValidationList()
                {
                    NamedValidators = System.Text.Json.JsonSerializer.Serialize(_formViewData.FormDef?.TableMeta?.ValidatedFields, JsonOptions),
                    ValidatorsOverride = "{}",
                    MasterFormValues = null, // TODO: Fix FormDefaultData structure
                    ObjectId = _roleWorkspaces.Workspaces.FirstOrDefault()?.Id
                },
                CsrfToken = _sessionData.SessionCsrfToken

            };



            var response = await PostAsync<GetFormValidationListDataResponse>(_endpoints.GetFormValidationListData, request, ct);

            if (response.IsFailure || response.Value == null)
            {
                _logger.LogError("Failed to get form validation list data: {Error}", response.Error);
                return Result<FormValidationListData>.Failure(response.Error ?? "Unknown error");
            }

            _formValidationListData = _mapper.Map<FormValidationListData>(response.Value.D);

            return Result<FormValidationListData>.Success(_formValidationListData);
        }

        catch (JsonSerializationException ex)
        {

            _logger.LogError(ex, "JSON deserialization error while getting form validation list data ");
            return Result<FormValidationListData>.Failure(ex.Message ?? "Unknown error");
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP request error while  getting form validation list data");
            return Result<FormValidationListData>.Failure(ex.Message ?? "Unknown error");
        }
        catch (Exception ex)
        {

            _logger.LogError("Exception getting form validation list data : {Message}", ex.Message);
            return Result<FormValidationListData>.Failure(ex.Message ?? "Unknown error");
        }
    }
    //=====================================================================
    //  Validated Search Data - Gets all favorite searches
    //=====================================================================

    public async Task<Result<List<ValidatedSearch>>>
        GetValidatedSearchAsync(CancellationToken ct)
    {
        try
        {
            // Get all favorite searches from workspace data
            var favoriteSearches = _workspaceData.SearchData?.Favorites;

            if (favoriteSearches == null || !favoriteSearches.Any())
            {
                _logger.LogWarning("No favorite searches found in workspace data");
                return Result<List<ValidatedSearch>>.Success(new List<ValidatedSearch>());
            }

            _logger.LogInformation("Found {Count} favorite searches to load", favoriteSearches.Count);

            var validatedSearches = new List<ValidatedSearch>();

            // Iterate through each favorite and get its validated search data
            foreach (var favorite in favoriteSearches)
            {
                if (string.IsNullOrEmpty(favorite.Id))
                {
                    _logger.LogWarning("Skipping favorite search with empty ID: {Name}", favorite.Name);
                    continue;
                }

                try
                {
                    var request = new GetValideatedSearchRequest()
                    {
                        SearchId = Guid.Parse(favorite.Id),
                        ObjectId = _workspaceData.ObjectId,
                        LayoutName = _roleWorkspaces.Workspaces.FirstOrDefault()?.LayoutName,
                        CsrfToken = _sessionData.SessionCsrfToken
                    };

                    var response = await PostAsync<GetValideatedSearchResponse>(_endpoints.GetValidatedSearch, request, ct);

                    if (response.IsFailure || response.Value == null)
                    {
                        _logger.LogWarning("Failed to get validated search for ID {SearchId}: {Error}", 
                            favorite.Id, response.Error);
                        continue;
                    }

                    var validatedSearch = _mapper.Map<ValidatedSearch>(response.Value.D);

                    _logger.LogInformation("Loaded validated search: {SearchName} (ID: {SearchId}) with {ConditionCount} conditions", 
                        validatedSearch.Name, 
                        validatedSearch.Id,
                        validatedSearch.Conditions.Count);

                    validatedSearches.Add(validatedSearch);
                }
                catch (FormatException ex)
                {
                    _logger.LogWarning(ex, "Invalid SearchId format for favorite: {FavoriteId}", favorite.Id);
                    continue;
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Error loading validated search for favorite: {FavoriteId}", favorite.Id);
                    continue;
                }
            }

            if (!validatedSearches.Any())
            {
                _logger.LogWarning("No validated searches were successfully loaded");
                return Result<List<ValidatedSearch>>.Success(new List<ValidatedSearch>());
            }

            _logger.LogInformation("Successfully loaded {Count} validated searches", validatedSearches.Count);
            return Result<List<ValidatedSearch>>.Success(validatedSearches);
        }
        catch (JsonSerializationException ex)
        {
            _logger.LogError(ex, "JSON deserialization error while getting validated search data");
            return Result<List<ValidatedSearch>>.Failure(ex.Message ?? "Unknown error");
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP request error while getting validated search data");
            return Result<List<ValidatedSearch>>.Failure(ex.Message ?? "Unknown error");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception getting validated search data: {Message}", ex.Message);
            return Result<List<ValidatedSearch>>.Failure(ex.Message ?? "Unknown error");
        }
    }

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


    /// <summary>
    /// Calculates the UTC offset in minutes for a given timezone name.
    /// Returns negative value for timezones ahead of UTC.
    /// </summary>
    private int GetTimezoneOffset(string? timezoneName)
    {
        if (string.IsNullOrWhiteSpace(timezoneName))
        {
            _logger.LogWarning("Timezone name is null or empty, using UTC offset 0");
            return 0;
        }

        try
        {
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timezoneName);
            var utcOffset = timeZoneInfo.GetUtcOffset(DateTime.UtcNow);
            // Negate the offset: UTC+2 becomes -120 minutes
            return -(int)utcOffset.TotalMinutes;
        }
        catch (TimeZoneNotFoundException ex)
        {
            _logger.LogWarning(ex, "Timezone '{TimezoneName}' not found, using UTC offset 0", timezoneName);
            return 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calculating timezone offset for '{TimezoneName}'", timezoneName);
            return 0;
        }
    }


}