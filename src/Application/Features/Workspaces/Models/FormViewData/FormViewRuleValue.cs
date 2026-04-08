namespace Application.Features.Workspaces.Models.FormViewData;

public class FormViewRuleValue
{
    public string? Name { get; set; }

    public string? FieldName { get; set; }

    public bool Disable { get; set; }

    public FormViewExpression? Expression { get; set; }

    public string? Description { get; set; }
}
