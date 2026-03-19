namespace Infrastructure.Ivanti;

public class IvantiEndpoints
{
    private readonly string _tenant;

    public IvantiEndpoints(string tenant)
    {
        _tenant = tenant;
    }

    public string Incidents =>
        $"{_tenant}api/odata/businessobject/incidents";
    public string Session =>
    $"{_tenant}/Services/Session.asmx/InitializeSession";
    public string UserData =>
    $"{_tenant}/Services/Session.asmx/GetUserData";
    public string WorkspaceRole =>
    $"{_tenant}/Services/Session.asmx/GetRoleWorkspaces";
    public string Workspaces =>
    $"{_tenant}/Services/Session.asmx/GetWorkspaceData";

    public string FormViewData =>
$"{_tenant}/Services/Session.asmx/FindFormViewData";
    public string FormValidationListData =>
$"{_tenant}/Services/Session.asmx/GetFormValidationListData";

}
