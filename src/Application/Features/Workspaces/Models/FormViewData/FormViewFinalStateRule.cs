namespace Application.Features.Workspaces.Models.FormViewData;

public class FormViewFinalStateRule
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public bool Disable { get; set; }

    public FormViewExpression? Expression { get; set; }

    public List<string> Exceptions { get; set; } = new();
}
