using Domain.Enums;

namespace Application.DTOs.Incident;

public sealed class IncidentUpdateRequest
{
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