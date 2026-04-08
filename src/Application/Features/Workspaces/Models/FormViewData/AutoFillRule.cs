namespace Application.Features.Workspaces.Models.FormViewData;

public class AutoFillRule
{
    public bool OnlyEmpty { get; set; }

    public bool Cascade { get; set; }

    public bool Disable { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public Expression? AutoFillExpression { get; set; }
}
