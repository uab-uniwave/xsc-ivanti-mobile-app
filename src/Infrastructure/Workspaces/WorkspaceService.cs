using Application.Common;
using Application.Interfaces.Workspaces;
using Application.Features.Workspaces.Models.FormDefaultData;
using Application.Features.Workspaces.Models.FormValidationListData;
using Application.Features.Workspaces.Models.FormViewData;
using Application.Features.Workspaces.Models.RoleWorkspaces;
using Application.Features.Workspaces.Models.ValidatedSearch;
using Application.Features.Workspaces.Models.WorkspaceData;
using Application.Features.Workspaces.DTOs;
using Application.Services;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Infrastructure.Workspaces;

/// <summary>
/// Implementation of workspace service using IvantiClient.
/// Provides access to workspace data, forms, and searches.
/// </summary>
public class WorkspaceService : IWorkspaceService
{
    private readonly IIvantiClient _ivantiClient;
    private readonly ILogger<WorkspaceService> _logger;

    public WorkspaceService(
        IIvantiClient ivantiClient,
        ILogger<WorkspaceService> logger)
    {
        _ivantiClient = ivantiClient;
        _logger = logger;
    }

    public async Task<Result<RoleWorkspaces>> GetRoleWorkspacesAsync(CancellationToken ct = default)
    {
        _logger.LogInformation("Getting role workspaces...");
        return await _ivantiClient.GetRoleWorkspacesAsync(ct);
    }

    public async Task<Result<WorkspaceData>> GetWorkspaceDataAsync(
        string? searchId = null,
        CancellationToken ct = default)
    {
        _logger.LogInformation("Getting workspace data (SearchId: {SearchId})...", searchId ?? "none");
        return await _ivantiClient.GetWorkspaceDataAsync(ct);
    }

    public async Task<Result<FormViewData>> FindFormViewDataAsync(
        string layoutName,
        string objectId,
        string viewName,
        bool isNewRecord = true,
        CancellationToken ct = default)
    {
        _logger.LogInformation("Finding form view data for layout: {LayoutName}, view: {ViewName}",
            layoutName, viewName);
        return await _ivantiClient.FindFormViewDataAsync(ct);
    }

    public async Task<Result<FormDefaultData>> GetFormDefaultDataAsync(
        string formName,
        string layoutName,
        string objectId,
        string objectType,
        string viewName,
        CancellationToken ct = default)
    {
        _logger.LogInformation("Getting form default data for form: {FormName}", formName);
        return await _ivantiClient.GetFormDefaultDataAsync(ct);
    }

    public async Task<Result<FormValidationListData>> GetFormValidationListDataAsync(
        string namedValidators,
        object masterFormValues,
        string objectId,
        CancellationToken ct = default)
    {
        _logger.LogInformation("Getting form validation list data for object: {ObjectId}", objectId);
        return await _ivantiClient.GetFormValidationListDataAsync(ct);
    }

    public async Task<Result<List<ValidatedSearch>>> GetValidatedSearchesAsync(
        WorkspaceData workspaceData,
        string layoutName,
        CancellationToken ct = default)
    {
        _logger.LogInformation("Getting validated searches for layout: {LayoutName}", layoutName);
        return await _ivantiClient.GetValidatedSearchAsync(ct);
    }
}
