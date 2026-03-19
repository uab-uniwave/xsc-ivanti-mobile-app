namespace Application.DTOs.Incident;

public sealed class IncidentListItemDto
{
    public string Id { get; set; } = default!;
    public string Number { get; set; } = default!;
    public string Subject { get; set; } = default!;
    public string Status { get; set; } = default!;
    public string Priority { get; set; } = default!;
    public string Impact { get; set; } = default!;
    public DateTimeOffset CreatedAt { get; set; }
}