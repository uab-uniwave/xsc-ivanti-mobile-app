using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Common;
using Application.Common.Models.SessonData;
using Application.Common.Models.UserData;
using Application.Features.Workspaces.Models.FormDefaultData;
using Application.Features.Workspaces.Models.FormValidationListData;
using Application.Features.Workspaces.Models.FormViewData;
using Application.Features.Workspaces.Models.RoleWorkspaces;
using Application.Features.Workspaces.Models.WorkspaceData;
using Application.Features.Workspaces.Models.ValidatedSearch;
using MapsterMapper;

namespace Application.Services;

/// <summary>
/// Legacy Ivanti API client implementation.
/// This is the internal implementation used by IvantiServiceAdapter.
/// Do not use this directly - use IIvantiService interface instead.
/// </summary>

public interface IIvantiClient
{

    Task<Result<SessionData>>
        InitializeSessionAsync(CancellationToken ct);
    //=====================================================================
    // USERDATA
    //=====================================================================
    Task<Result<Common.Models.UserData.UserData>>
        GetUserDataAsync(CancellationToken ct);
    //=====================================================================
    //  Roles workspaces 
    //=====================================================================
     Task<Result<RoleWorkspaces>>
        GetRoleWorkspacesAsync(CancellationToken ct);
    //=====================================================================
    // Workspaces data
    //=====================================================================
    Task<Result<WorkspaceData>>
        GetWorkspaceDataAsync(CancellationToken ct);
    //=====================================================================
    // Find Form View Data
    //=====================================================================
    Task<Result<FormViewData>>
        FindFormViewDataAsync(CancellationToken ct);
    Task<Result<FormDefaultData>>
        GetFormDefaultDataAsync(CancellationToken ct);


    Task<Result<FormValidationListData>>
    GetFormValidationListDataAsync(CancellationToken ct);

    Task<Result<List<ValidatedSearch>>>
    GetValidatedSearchAsync(CancellationToken ct);
  

}

