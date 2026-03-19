using Domain.Enums;

namespace Application.DTOs.Incident;

public sealed class IncidentDto
{
    public string Id { get; set; } = default!;
    public string Number { get; set; } = default!;

    public IncidentStatus Status { get; set; }

    public string Priority { get; set; } = default!;
    public string Service { get; set; } = default!;
    public string Category { get; set; } = default!;
    public string Urgency { get; set; } = default!;
    public string Impact { get; set; } = default!;

    public string Owner { get; set; } = default!;
    public string OwnerTeam { get; set; } = default!;

    public string Subject { get; set; } = default!;
    public string Description { get; set; } = default!;
}