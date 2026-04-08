using Application.Common.Models.SessonData;
using Application.Common.Models.UserData;
using Application.Features.Workspaces.Models.RoleWorkspaces;
using Application.Features.Workspaces.Models.WorkspaceData;
using Application.Interfaces.State;
using Microsoft.Extensions.Logging;

namespace Infrastructure.State;

/// <summary>
/// Scoped service for maintaining Ivanti session state across the Blazor circuit.
/// This service holds authentication and workspace data that persists across
/// page navigations within the same user session.
/// 
/// Registered as Scoped to ensure state persists within a single Blazor circuit
/// but is isolated between different user sessions.
/// </summary>
public class IvantiStateService : IIvantiStateService
{
    private readonly ILogger<IvantiStateService> _logger;

    public IvantiStateService(ILogger<IvantiStateService> logger)
    {
        _logger = logger;
        _logger.LogDebug("IvantiStateService instance created");
    }

    /// <inheritdoc />
    public SessionData? SessionData { get; set; }

    /// <inheritdoc />
    public UserData? UserData { get; set; }

    /// <inheritdoc />
    public RoleWorkspaces? RoleWorkspaces { get; set; }

    /// <inheritdoc />
    public List<WorkspaceFullData> AllWorkspacesData { get; set; } = new();

    /// <inheritdoc />
    public WorkspaceFullData? CurrentWorkspace { get; set; }

    /// <inheritdoc />
    public bool IsSessionInitialized => SessionData != null;

    /// <inheritdoc />
    public bool IsUserDataLoaded => UserData != null;

    /// <inheritdoc />
    public WorkspaceFullData? GetWorkspaceById(string workspaceId)
    {
        return AllWorkspacesData.FirstOrDefault(w => 
            w.Workspace.Id.Equals(workspaceId, StringComparison.OrdinalIgnoreCase));
    }

    /// <inheritdoc />
    public WorkspaceFullData? GetWorkspaceByName(string workspaceName)
    {
        return AllWorkspacesData.FirstOrDefault(w => 
            w.Workspace.Name.Equals(workspaceName, StringComparison.OrdinalIgnoreCase));
    }

    /// <inheritdoc />
    public Workspace? GetWorkspaceDefinitionById(string workspaceId)
    {
        return RoleWorkspaces?.Workspaces.FirstOrDefault(w => 
            w.Id.Equals(workspaceId, StringComparison.OrdinalIgnoreCase));
    }

    /// <inheritdoc />
    public Workspace? GetWorkspaceDefinitionByName(string workspaceName)
    {
        return RoleWorkspaces?.Workspaces.FirstOrDefault(w => 
            w.Name.Equals(workspaceName, StringComparison.OrdinalIgnoreCase));
    }

    /// <inheritdoc />
    public void Clear()
    {
        _logger.LogInformation("Clearing Ivanti state service data");

        SessionData = null;
        UserData = null;
        RoleWorkspaces = null;
        AllWorkspacesData.Clear();
        CurrentWorkspace = null;
    }
}
