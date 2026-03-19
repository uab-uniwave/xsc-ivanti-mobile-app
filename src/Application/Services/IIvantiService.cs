using Application.Common;
using Application.DTOs.Incident;

namespace Application.Services;

public interface IIvantiService
{
    Task<Result<PagedResult<IncidentListItemDto>>>
        GetIncidentsAsync(PagedQuery query, CancellationToken ct);

    Task<Result<IncidentDto>>
        GetIncidentByIdAsync(string id, CancellationToken ct);

    Task<Result<bool>>
        UpdateIncidentAsync(string id, IncidentUpdateRequest request);
}