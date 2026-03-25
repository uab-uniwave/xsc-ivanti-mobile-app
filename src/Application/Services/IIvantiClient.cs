using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Common;
using Application.Models.FormDefaultData;
using Application.Models.FormViewData;
using Application.Models.RoleWorkspaces;
using Application.Models.SessonData;
using Application.Models.UserData;
using Application.Models.WorkspaceData;
using Application.Requests;
using Application.Responses;
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
    Task<Result<UserData>>
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


}

