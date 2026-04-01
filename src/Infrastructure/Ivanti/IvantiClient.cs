using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Common;
using Application.DTOs;
using Application.Models.FormDefaultData;
using Application.Models.FormValidationListData;
using Application.Models.FormViewData;
using Application.Models.RoleWorkspaces;
using Application.Models.SessonData;
using Application.Models.UserData;
using Application.Models.WorkspaceData;
using Application.Requests;
using Application.Responses;
using Application.Services;
using Mapster.Models;
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



    // Session and initialization endpoints (must be called in sequence first)
    //=====================================================================
    // SESSION DATA
    //=====================================================================
    public async Task<Result<SessionData>>
        InitializeSessionAsync(CancellationToken ct)
    {


        var request = new InitializeSessionRequest();
        var response = await PostAsync<InitializeSessionResponse>(_endpoints.InitializeSession,
            request,
            ct);

        if(response.IsFailure || response.Value == null)
        {
            _logger.LogError("Failed to initialize session: {Error}", response.Error);
            return Result<SessionData>.Failure(response.Error ?? "Unknown error");
        }
        _sessionData =_mapper.Map<SessionData>(response.Value.D);


        return Result<SessionData>.Success(_sessionData);


    }

    //=====================================================================
    // USERDATA
    //=====================================================================
    public async Task<Result<UserData>>
        GetUserDataAsync(CancellationToken ct)
    {
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
    //=====================================================================
    //  Roles workspaces 
    //=====================================================================
    public async Task<Result<RoleWorkspaces>>
        GetRoleWorkspacesAsync(CancellationToken ct)
    {


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
    //=====================================================================
    // Workspaces data
    //=====================================================================
    public async Task<Result<WorkspaceData>>
        GetWorkspaceDataAsync(CancellationToken ct)
    {
        var workspace = _roleWorkspaces.Workspaces.FirstOrDefault(w => w.Name == "Incident") 
            ?? _roleWorkspaces.Workspaces.FirstOrDefault();

        if (workspace == null)
        {
            _logger.LogError("No workspace found in role workspaces");
            return Result<WorkspaceData>.Failure("No workspace found");
        }

        var request = new GetWorkspaceDataRequest()
        {
            ObjectId = workspace.Id,
            LayoutName = workspace.LayoutName,
            CsrfToken = _sessionData.SessionCsrfToken,
        };
        var response = await PostAsync<GetWorkspaceDataResponse>(_endpoints.GetWorkspaceData,
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
    //=====================================================================
    // Find Form View Data
    //=====================================================================
    public async Task<Result<FormViewData>>
        FindFormViewDataAsync(CancellationToken ct)
    {
        try
        {
            var workspace = _roleWorkspaces.Workspaces.FirstOrDefault();
            if (workspace == null)
            {
                _logger.LogError("No workspace found in role workspaces");
                return Result<FormViewData>.Failure("No workspace found");
            }

            var request = new FindFormViewDataRequest()
            {
                CreatedViewsOnClient = new(),
                IsNewRecord = true,
                LayoutName = workspace.LayoutName,
                ObjectId = _workspaceData.ObjectId,
                ViewName = _workspaceData.LayoutData?.OneNewRecordView ?? "formEdit",
                CsrfToken = _sessionData.SessionCsrfToken       
            };

            //    var json = JsonSerializer.Serialize(request);

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

            var workspace = _roleWorkspaces.Workspaces.FirstOrDefault(w => w.Name == "Incident")
        ?? _roleWorkspaces.Workspaces.FirstOrDefault();
            var request = new GetFormDefaultDataRequest()
            {
                
                FormName = _formViewData.FormDef.FormMeta?.Name,
                LayoutName = null,
                MasterData = null,
                ObjectId = "",


                DependentInfo = null,
                ObjectType = workspace.Id,
                ViewName = _formViewData.ViewName,
                CsrfToken = _sessionData.SessionCsrfToken
             
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
    //  Form Validation List    Data
    //=====================================================================


    public async Task<Result<FormValidationListData>>
        GetFormValidationListDataAsync(CancellationToken ct)
    {
        try
        {

            var workspace = _roleWorkspaces.Workspaces.FirstOrDefault(w => w.Name == "Incident")
        ?? _roleWorkspaces.Workspaces.FirstOrDefault();
            var request = new GetFormDefaultDataRequest()
            {

                FormName = _formViewData.FormDef.FormMeta?.Name,
                LayoutName = null,
                MasterData = null,
                ObjectId = "",


                DependentInfo = null,
                ObjectType = workspace.Id,
                ViewName = _formViewData.ViewName,
                CsrfToken = _sessionData.SessionCsrfToken

            };
            var response = await PostAsync<FormValidationListDataResponse>(_endpoints.GetFormDefaultData,
  request,
  ct);

            if (response.IsFailure || response.Value == null)
            {
                _logger.LogError("Failed to get form validation list data: {Error}", response.Error);
                return Result<FormValidationListData>.Failure(response.Error ?? "Unknown error");
            }

            return Result<FormValidationListData>.Success(response.Value.D);

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