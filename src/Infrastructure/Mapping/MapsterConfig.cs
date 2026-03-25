using Application.Dtos;
using Application.DTOs;
using Application.Models.FormDefaultData;
using Application.Models.FormViewData;
using Application.Models.RoleWorkspaces;
using Application.Models.SessonData;
using Application.Models.UserData;
using Application.Models.WorkspaceData;
using Application.Responses;
using Domain.Enums;
using Mapster;
using static Application.DTOs.FormDefaultDataDto;
using static Application.Models.FormDefaultData.FormDefaultData;
using static Application.Models.WorkspaceData.WorkspaceData;

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
        config.NewConfig<SessionData, SessionDataDto>().TwoWays();



        // UserData -> UserDataDto
        // Simple mapping - Mapster will match properties automatically
        config.NewConfig<UserData, UserDataDto>().TwoWays();



        // ====================================================
        // WORKSPACE MAPPINGS
        // ====================================================

   

        // RoleWorkspaces -> RoleWorkspacesDto
        config.NewConfig<RoleWorkspaces, RoleWorkspacesDto>().TwoWays();
        config.NewConfig<Workspace, WorkspaceDto>().TwoWays();
        config.NewConfig<WorkspaceData, WorkspaceDataDto>().TwoWays();
        config.NewConfig<RoleWorkspaceConfiguration, RoleWorkspaceConfigurationDto>().TwoWays();
        config.NewConfig<RoleWorkspaceNotifications, RoleWorkspaceNotificationsDto>().TwoWays();
        config.NewConfig<RoleWorkspaceBrandingOptions, RoleWorkspaceBrandingOptionsDto>().TwoWays();
        config.NewConfig<RoleWorkspaceSystemMenuOptions, RoleWorkspaceSystemMenuOptionsDto>().TwoWays(); 
        config.NewConfig<RoleWorkspaceSelectorOptions, RoleWorkspaceSelectorOptionsDto>().TwoWays();
        config.NewConfig<RoleWorkspaceHelpLink, RoleWorkspaceHelpLinkDto>().TwoWays();
        config.NewConfig<RoleWorkspaceLink, RoleWorkspaceLinkDto>().TwoWays();




        //SessionData -> SessionDataDto
        config.NewConfig<SessionData, SessionDataDto>().TwoWays();
        config.NewConfig<AvailableLanguage, AvailableLanguageDto>().TwoWays();


        //UserData -> UserDataDto
        config.NewConfig<UserData, UserDataDto>().TwoWays();




        config.NewConfig<WorkspaceData, WorkspaceDataDto>().TwoWays();

        // WorkspaceSearchData -> WorkspaceSearchDataDto
        config.NewConfig<WorkspaceSearchData, WorkspaceSearchDataDto>().TwoWays();

        // WorkspaceFavorite -> WorkspaceFavoriteDto
        config.NewConfig<WorkspaceFavorite, WorkspaceFavoriteDto>().TwoWays();

        // WorkspaceFieldsTreeData -> WorkspaceFieldsTreeDataDto
        config.NewConfig<WorkspaceFieldsTreeData, WorkspaceFieldsTreeDataDto>().TwoWays();

        // WorkspaceTableDef -> WorkspaceTableDefDto
        config.NewConfig<WorkspaceTableDef, WorkspaceTableDefDto>().TwoWays();

        // WorkspaceFieldItem -> WorkspaceFieldItemDto
        config.NewConfig<WorkspaceFieldItem, WorkspaceFieldItemDto>().TwoWays();

        // WorkspaceFieldMetaData -> WorkspaceFieldMetaDataDto
        config.NewConfig<WorkspaceFieldMetaData, WorkspaceFieldMetaDataDto>().TwoWays();

        config.NewConfig<FormViewData, FormViewDataDto>().TwoWays();
        config.NewConfig<FormDefinition, FormDefinitionDto>().TwoWays();
        config.NewConfig<TableMeta, TableMetaDto>().TwoWays();
        config.NewConfig<ToolbarDef, ToolbarDefDto>().TwoWays();
        config.NewConfig<FieldMeta, FieldMetaDto>().TwoWays();
        config.NewConfig<FieldLink, FieldLinkDto>().TwoWays();
        config.NewConfig<FormMeta, FormMetaDto>().TwoWays();
        config.NewConfig<FormCell, FormCellDto>().TwoWays();
        config.NewConfig<FormControl, FormControlDto>().TwoWays();
        config.NewConfig<RuleMeta, RuleMetaDto>().TwoWays();

        config.NewConfig<RuleValue, RuleValueDto>().TwoWays(); 
        config.NewConfig<ExpressionTree, ExpressionTreeDto>().TwoWays();
        config.NewConfig<AutoFillRule, AutoFillRuleDto>().TwoWays();
        config.NewConfig<ExpressionRule, ExpressionRuleDto>().TwoWays();
        config.NewConfig<FinalStateRule, FinalStateRuleDto>().TwoWays();
        config.NewConfig<ExpressionDto, Expression>().TwoWays();

        config.NewConfig<FormDefaultData, FormDefaultDataDto>().TwoWays();

        config.NewConfig<FormDefaultDataContainer, FormDefaultDataContainerDto>().TwoWays();

        config.NewConfig<ReferencedFieldDefinition, ReferencedFieldDefinitionDto>().TwoWays();

        config.NewConfig<FormDataModel, FormDataModelDto>().TwoWays();

        config.NewConfig<DataObject, DataObjectDto>().TwoWays();

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