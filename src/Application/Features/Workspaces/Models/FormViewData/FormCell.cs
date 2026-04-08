namespace Application.Features.Workspaces.Models.FormViewData;

public class FormCell
{
    public bool SectionBreak { get; set; }

    public int Row { get; set; }

    public int Column { get; set; }

    public int RowSpan { get; set; }

    public int ColSpan { get; set; }

    public List<string> ControlNames { get; set; } = new();

    public int ControlPlacement { get; set; }

    public string? CellStyle { get; set; }

    public string? CellStyleExpression { get; set; }

    public string? CellVAlign { get; set; }

    public object? Margins { get; set; }
}
