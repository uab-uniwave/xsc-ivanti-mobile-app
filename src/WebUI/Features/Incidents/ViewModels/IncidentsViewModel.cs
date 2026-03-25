using Application.Common;
using Application.DTOs.Incident;
using Application.Services;

namespace WebUI.Features.Incidents.ViewModels;

/// <summary>
/// ViewModel for managing incidents list and related operations.
/// Handles state management and communication with the Ivanti service.
/// </summary>
public sealed class IncidentsViewModel
{
    private readonly IIvantiClient _ivanti;

    public List<IncidentListItemDto> Items { get; private set; } = new();
    public PagedResult<IncidentListItemDto>? CurrentPage { get; private set; }

    public bool IsLoading { get; private set; }
    public bool HasError { get; private set; }
    public string? ErrorMessage { get; private set; }

    public IncidentsViewModel(IIvantiClient ivanti)
    {
        _ivanti = ivanti;
    }

    /// <summary>
    /// Loads the first page of incidents.
    /// Initializes session and retrieves incidents list.
    /// </summary>
    public async Task LoadFirstPageAsync(CancellationToken ct)
    {
        await LoadPageAsync(1, ct);
    }

    /// <summary>
    /// Loads a specific page of incidents.
    /// </summary>
    public async Task LoadPageAsync(int pageNumber, CancellationToken ct)
    {
        IsLoading = true;
        HasError = false;
        ErrorMessage = null;

        try
        {
            // Initialize session (required for all other calls)
            var sessionResult = await _ivanti.InitializeSessionAsync(ct);
            if (sessionResult.IsFailure)
            {
                throw new InvalidOperationException($"Failed to initialize session: {sessionResult.Error}");
            }

            // Get user data
                var userDataResult = await _ivanti.GetUserDataAsync(ct);
                if (userDataResult.IsFailure)
            {
                throw new InvalidOperationException($"Failed to get user data: {userDataResult.Error}");
            }


            var roleWorkspacesResult = await _ivanti.GetRoleWorkspacesAsync(ct);
            if (roleWorkspacesResult.IsFailure)
            {
                throw new InvalidOperationException($"Failed to role workspace: {roleWorkspacesResult.Error}");
            }



            var workspaceDataResult = await _ivanti.GetWorkspaceDataAsync(ct);
            if (workspaceDataResult.IsFailure)
            {
                throw new InvalidOperationException($"Failed to get worksapace data: {workspaceDataResult.Error}");
            }

            var formViewDataResult = await _ivanti.FindFormViewDataAsync(ct);
            if (formViewDataResult.IsFailure)
            {
                throw new InvalidOperationException($"Failed to find form view fata data: {formViewDataResult.Error}");
            }
            var formDataDefaultResult = await _ivanti.GetFormDefaultDataAsync(ct);
            if (formDataDefaultResult.IsFailure)
            {
                throw new InvalidOperationException($"Failed to find form view fata data: {formDataDefaultResult.Error}");
            }


            // Load incidents
            var query = new PagedQuery
            {
                Page = pageNumber,
                PageSize = 10
            };

            //var result = await _ivanti.GetIncidentsAsync(query, ct);

            //if (result.IsFailure)
            //{
            //    throw new InvalidOperationException($"Failed to load incidents: {result.Error}");
            //}

            //CurrentPage = result.Value;
            //Items = result.Value?.Items ?? new();
        }
        catch (Exception ex)
        {
            HasError = true;
            ErrorMessage = ex.Message;
        }
        finally
        {
            IsLoading = false;
        }
    }

    /// <summary>
    /// Gets a single incident by its ID.
    /// </summary>
    public async Task<IncidentDto?> GetIncidentByIdAsync(string incidentId, CancellationToken ct)
    {
        try
        {
            //var result = await _ivanti.GetIncidentByIdAsync(incidentId, ct);

            //if (result.IsFailure)
            //{
            //    ErrorMessage = result.Error;
            //    return null;
            //}

            //return result.Value;

            return null;
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
            return null;
        }
    }

    /// <summary>
    /// Updates an incident with new data.
    /// </summary>
    public async Task<bool> UpdateIncidentAsync(string incidentId, IncidentUpdateRequestDto request, CancellationToken ct)
    {
        try
        {
            //    var result = await _ivanti.UpdateIncidentAsync(incidentId, request, ct);

            //    if (result.IsFailure)
            //    {
            //        ErrorMessage = result.Error;
            //        return false;
            //    }


            //return result.Value;

            return true;
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
            return false;
        }
    }
}
