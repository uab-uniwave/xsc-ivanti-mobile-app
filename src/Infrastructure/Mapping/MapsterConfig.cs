using Application.Common.Models.SessonData;
using Application.Common.Models.UserData;
using Application.Features.Workspaces.Models.FormDefaultData;
using Application.Features.Workspaces.Models.FormViewData;
using Application.Features.Workspaces.Models.RoleWorkspaces;
using Application.Features.Workspaces.Models.WorkspaceData;
using Domain.Enums;
using Mapster;
using static Application.Features.Workspaces.Models.FormDefaultData.FormDefaultData;
using static Application.Features.Workspaces.Models.WorkspaceData.WorkspaceData;

namespace Infrastructure.Mapping;

public static class MapsterConfig
{
    public static void RegisterMappings()
    {
        var config = TypeAdapterConfig.GlobalSettings;

        // ====================================================
        // SESSION MAPPINGS
        // ====================================================

        // SessionData -> SessionDataDto
        // Note: SessionData does not have SessionId, mapping what's available




        // UserData -> UserDataDto
        // Simple mapping - Mapster will match properties automatically



        // ====================================================
        // WORKSPACE MAPPINGS
        // ====================================================



        // RoleWorkspaces -> RoleWorkspacesDto




        //SessionData -> SessionDataDto


        //UserData -> UserDataDto






        // WorkspaceSearchData -> WorkspaceSearchDataDto

        // WorkspaceFavorite -> WorkspaceFavoriteDto


        // WorkspaceFieldsTreeData -> WorkspaceFieldsTreeDataDto


        // WorkspaceTableDef -> WorkspaceTableDefDto


        // WorkspaceFieldItem -> WorkspaceFieldItemDto  config.NewConfig<WorkspaceFieldItem, WorkspaceFieldItemDto>().TwoWays();

        // WorkspaceFieldMetaData -> WorkspaceFieldMetaDataDto
    } 


    // 👇 Expression-tree safe method
    private static IncidentStatus ParseStatus(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return IncidentStatus.Open;

        if (Enum.TryParse(typeof(IncidentStatus), value, true, out var result))
            return (IncidentStatus)result;

        return IncidentStatus.Open;
    }
}