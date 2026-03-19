using System.Net.Http.Json;
using System.Text.Json;
using Application.Common;
using Application.DTOs.Incident;
using Application.Services;
using Infrastructure.DTOs;
using MapsterMapper;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Ivanti;

public sealed class IvantiClient : IIvantiService
{
    private readonly HttpClient _http;
    private readonly ILogger<IvantiClient> _logger;
    private readonly IvantiEndpoints _endpoints;
    private readonly IMapper _mapper;

    public IvantiClient(
        HttpClient http,
        ILogger<IvantiClient> logger,
        IvantiEndpoints endpoints,
        IMapper mapper)
    {
        _http = http;
        _logger = logger;
        _endpoints = endpoints;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<IncidentListItemDto>>>
        GetIncidentsAsync(PagedQuery query, CancellationToken ct)
    {
        var odata = ODataQueryBuilder.Build(query);
        var url = _endpoints.Incidents + odata;

        var response = await SendAsync<
            ODataResponse<IncidentResponse>>(url, ct);

        if (response.IsFailure)
            return Result<PagedResult<IncidentListItemDto>>
                .Failure(response.Error!);

        var items = _mapper.Map<List<IncidentListItemDto>>(
            response.Value!.Value);

        var paged = new PagedResult<IncidentListItemDto>(
            items,
            query.Page,
            query.PageSize,
            response.Value.Count);

        return Result<PagedResult<IncidentListItemDto>>
            .Success(paged);
    }

    public async Task<Result<IncidentDto>>
      GetIncidentByIdAsync(string id, CancellationToken ct)
    {
        var url = $"{_endpoints.Incidents}('{id}')";

        var response = await SendAsync<IncidentResponse>(url, ct);

        if (response.IsFailure)
            return Result<IncidentDto>.Failure(response.Error!);

        var dto = _mapper.Map<IncidentDto>(response.Value!);

        return Result<IncidentDto>.Success(dto);
    }

    public async Task<Result<bool>>
        UpdateIncidentAsync(string id, IncidentUpdateRequest request)
    {
        try
        {
            var payload = new
            {
                Status = request.Status.ToString(),
                Priority = request.Priority,
                Service = request.Service,
                Category = request.Category,
                Urgency = request.Urgency,
                Impact = request.Impact,
                Owner = request.Owner,
                OwnerTeam = request.OwnerTeam,
                Subject = request.Subject,
                Symptom = request.Description
            };

            var response = await _http.PatchAsJsonAsync(
                $"{_endpoints.Incidents}('{id}')",
                payload);

            if (!response.IsSuccessStatusCode)
                return Result<bool>.Failure("Ivanti update failed");

            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Update failed");
            return Result<bool>.Failure("Unexpected error");
        }
    }
    private async Task<Result<T>> SendAsync<T>(
    string url,
    CancellationToken ct)
    {
        try
        {
            var httpResponse = await _http.GetAsync(url, ct);

            if (!httpResponse.IsSuccessStatusCode)
            {
                _logger.LogWarning(
                    "Ivanti returned {StatusCode} for {Url}",
                    httpResponse.StatusCode, url);

                return Result<T>.Failure(
                    $"Ivanti error: {httpResponse.StatusCode}");
            }

            var stream = await httpResponse.Content.ReadAsStreamAsync(ct);

            var result = await JsonSerializer.DeserializeAsync<T>(
                stream,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                },
                ct);

            if (result == null)
                return Result<T>.Failure("Invalid Ivanti response");

            return Result<T>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ivanti failure for {Url}", url);
            return Result<T>.Failure("Unexpected Ivanti error");
        }
    }
}