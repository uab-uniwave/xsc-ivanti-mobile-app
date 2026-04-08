namespace Infrastructure.Ivanti;

public class IvantiEndpoints
{
    private readonly string _tenant;

    public IvantiEndpoints(string tenant)
    {
        _tenant = tenant;
    }

    public string GetVerifiationToken =>
    $"{_tenant}/";

    public string Login =>
    $"{_tenant}/";

    public string SelectRole =>
    $"{_tenant}/Account/SelectRole";



    public string Incidents =>
        $"{_tenant}/api/odata/businessobject/incidents";
    public string InitializeSession =>
    $"{_tenant}/Services/Session.asmx/InitializeSession";
    public string GetUserData =>
    $"{_tenant}/Services/Session.asmx/GetUserData";
    public string GetRoleWorkspaces =>
    $"{_tenant}/Services/Workspace.asmx/GetRoleWorkspaces";
    public string GetWorkspaceData =>
    $"{_tenant}/Services/Workspace.asmx/GetWorkspaceData";

    public string FindFormViewData =>
        $"{_tenant}/Services/Workspace.asmx/FindFormViewData";
    public string GetFormDefaultData =>
    $"{_tenant}/Services/FormService.asmx/GetFormDefaultData";

    public string GetFormValidationListData =>
     $"{_tenant}/Services/FormService.asmx/GetFormValidationListData";
    public string GetValidatedSearch =>
     $"{_tenant}/Services/Search.asmx/GetValidatedSearch";

    public string GridDataHandler =>
     $"{_tenant}/Services/Search.asmx/GridDataHandler";
}