namespace Application.Features.Workspaces.Models.FormViewData;

public class FinalStateRule
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public bool Disable { get; set; }

    public Expression? Expression { get; set; }

    public List<string> Exceptions { get; set; } = new();
}
