namespace Application.Features.Workspaces.Models.FormViewData;

public class FormViewExpressionRule
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public List<string> FieldRefs { get; set; } = new();

    public bool IsFullExpression { get; set; }

    public FormViewExpressionTree? Tree { get; set; }

    public string? Source { get; set; }

    public int ValidationStatus { get; set; }
}
