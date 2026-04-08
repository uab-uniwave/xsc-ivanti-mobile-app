namespace Application.Features.Workspaces.Models.FormViewData;

public class FormViewExpression
{
    public string? Name { get; set; }

    public string? Source { get; set; }

    public int ValidationStatus { get; set; }

    public bool IsFullExpression { get; set; }

    public List<string> FieldRefs { get; set; } = new();

    public FormViewExpressionTree? Tree { get; set; }

    public string? Description { get; set; }
}
