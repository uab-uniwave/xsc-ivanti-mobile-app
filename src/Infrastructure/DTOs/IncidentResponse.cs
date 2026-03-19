namespace Infrastructure.DTOs;

public sealed class IncidentResponse
{
    public string RecId { get; set; } = default!;
    public int IncidentNumber { get; set; }

    public string Status { get; set; } = default!;
    public string Priority { get; set; } = default!;
    public string Service { get; set; } = default!;
    public string Category { get; set; } = default!;
    public string Urgency { get; set; } = default!;
    public string Impact { get; set; } = default!;

    public string Owner { get; set; } = default!;
    public string OwnerTeam { get; set; } = default!;

    public string Subject { get; set; } = default!;
    public string Symptom { get; set; } = default!;
}