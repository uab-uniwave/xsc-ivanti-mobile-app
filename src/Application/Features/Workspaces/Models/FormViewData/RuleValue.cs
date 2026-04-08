namespace Application.Features.Workspaces.Models.FormViewData;

public class RuleValue
{
    public string? Name { get; set; }

    public string? FieldName { get; set; }

    public bool Disable { get; set; }

    public Expression? Expression { get; set; }

    public string? Description { get; set; }
}
