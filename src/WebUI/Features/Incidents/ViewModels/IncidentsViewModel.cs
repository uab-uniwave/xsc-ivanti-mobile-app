using Application.Common;
using Application.Features.Incidents.DTOs;
using Application.Features.Workspaces.Models.WorkspaceData;
using Application.Features.Workspaces.Models.GridDataHandler;
using Application.Interfaces.State;
using WsFavorite = Application.Features.Workspaces.Models.WorkspaceData.WorkspaceFavorite;

using Application.Services;
using Microsoft.Extensions.Logging;

namespace WebUI.Features.Incidents.ViewModels;

/// <summary>
/// ViewModel for managing incidents list and related operations.
/// Handles state management and communication with the Ivanti service.
/// </summary>
public sealed class IncidentsViewModel
{
    private readonly IIvantiClient _ivanti;
    private readonly IIvantiStateService _stateService;
    private readonly ILogger<IncidentsViewModel> _logger;

    public List<IncidentListItemDto> Items { get; private set; } = new();
    public PagedResult<IncidentListItemDto>? CurrentPage { get; private set; }
    public List<WsFavorite> SavedSearches { get; private set; } = new();
    public WsFavorite? SelectedSearch { get; set; }
    public GridDataHandler? CurrentGridData { get; private set; }

    public bool IsLoading { get; private set; }
    public bool HasError { get; private set; }
    public string? ErrorMessage { get; private set; }

    /// <summary>
    /// Event raised when state changes to notify UI to re-render.
    /// </summary>
    public event Action? OnStateChanged;

    public IncidentsViewModel(
        IIvantiClient ivanti,
        IIvantiStateService stateService,
        ILogger<IncidentsViewModel> logger)
    {
        _ivanti = ivanti;
        _stateService = stateService;
        _logger = logger;
    }

    /// <summary>
    /// Initializes the incidents page using data already loaded after role selection.
    /// </summary>
    public async Task InitializeAsync(CancellationToken ct = default)
    {
        IsLoading = true;
        HasError = false;
        ErrorMessage = null;

        try
        {
            _logger.LogInformation("Initializing Incidents page...");

            // Data should already be loaded by AuthenticationService.SelectRoleAsync
            // Just extract saved searches from the state service
            if (_stateService.WorkspaceData?.SearchData?.Favorites != null)
            {
                SavedSearches = _stateService.WorkspaceData.SearchData.Favorites;
                _logger.LogInformation("Found {Count} saved searches", SavedSearches.Count);
            }
            else
            {
                _logger.LogWarning("No saved searches found in workspace data");
                SavedSearches = new List<WsFavorite>();
            }

            // Select default search or first available
            SelectedSearch = SavedSearches.FirstOrDefault(s => s.IsDefault) ?? SavedSearches.FirstOrDefault();

            // If we have a selected search, load its data
            if (SelectedSearch != null && !string.IsNullOrEmpty(SelectedSearch.Id))
            {
                _logger.LogInformation("Loading default search: {SearchName}", SelectedSearch.Name);
                await LoadGridDataAsync(SelectedSearch, ct);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error initializing Incidents page");
            HasError = true;
            ErrorMessage = ex.Message;
        }
        finally
        {
            IsLoading = false;
            NotifyStateChanged();
        }
    }

    /// <summary>
    /// Loads the first page of incidents (legacy method - calls InitializeAsync).
    /// </summary>
    public async Task LoadFirstPageAsync(CancellationToken ct)
    {
        await InitializeAsync(ct);
    }

    /// <summary>
    /// Handles search selection change from the MudSelect dropdown.
    /// Calls GridDataHandler to fetch data for the selected search.
    /// </summary>
    public async Task OnSearchChangedAsync(WsFavorite? selectedSearch, CancellationToken ct = default)
    {
        if (selectedSearch == null)
        {
            _logger.LogWarning("Search selection cleared");
            SelectedSearch = null;
            CurrentGridData = null;
            NotifyStateChanged();
            return;
        }

        _logger.LogInformation("Search changed to: {SearchName} (ID: {SearchId})", 
            selectedSearch.Name, selectedSearch.Id);

        SelectedSearch = selectedSearch;
        await LoadGridDataAsync(selectedSearch, ct);
    }

    /// <summary>
    /// Loads grid data for a specific saved search using GridDataHandler.
    /// </summary>
    private async Task LoadGridDataAsync(WsFavorite search, CancellationToken ct)
    {
        if (string.IsNullOrEmpty(search.Id))
        {
            _logger.LogWarning("Cannot load grid data: Search ID is empty");
            return;
        }

        IsLoading = true;
        HasError = false;
        ErrorMessage = null;
        NotifyStateChanged();

        try
        {
            if (!Guid.TryParse(search.Id, out var searchId))
            {
                _logger.LogError("Invalid search ID format: {SearchId}", search.Id);
                HasError = true;
                ErrorMessage = "Invalid search ID format";
                return;
            }

            _logger.LogInformation("Calling GridDataHandler for search: {SearchName} (ID: {SearchId})", 
                search.Name, searchId);

            var result = await _ivanti.GetGridDataAsync(searchId, skip: 0, take: 50, ct);

            if (result.IsFailure)
            {
                _logger.LogError("GridDataHandler failed: {Error}", result.Error);
                HasError = true;
                ErrorMessage = result.Error;
                return;
            }

            CurrentGridData = result.Value;
            _logger.LogInformation("GridDataHandler returned {Count} records (Total: {Total})", 
                CurrentGridData?.Data?.Count ?? 0, CurrentGridData?.TotalCount ?? 0);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading grid data for search: {SearchName}", search.Name);
            HasError = true;
            ErrorMessage = ex.Message;
        }
        finally
        {
            IsLoading = false;
            NotifyStateChanged();
        }
    }

    /// <summary>
    /// Loads a specific page of grid data.
    /// </summary>
    public async Task LoadPageAsync(int pageNumber, CancellationToken ct)
    {
        if (SelectedSearch == null || string.IsNullOrEmpty(SelectedSearch.Id))
        {
            _logger.LogWarning("Cannot load page: No search selected");
            return;
        }

        if (!Guid.TryParse(SelectedSearch.Id, out var searchId))
        {
            _logger.LogError("Invalid search ID format: {SearchId}", SelectedSearch.Id);
            return;
        }

        IsLoading = true;
        HasError = false;
        NotifyStateChanged();

        try
        {
            int pageSize = 50;
            int skip = (pageNumber - 1) * pageSize;

            _logger.LogInformation("Loading page {PageNumber} (Skip: {Skip}, Take: {Take})", 
                pageNumber, skip, pageSize);

            var result = await _ivanti.GetGridDataAsync(searchId, skip, pageSize, ct);

            if (result.IsFailure)
            {
                _logger.LogError("Failed to load page: {Error}", result.Error);
                HasError = true;
                ErrorMessage = result.Error;
                return;
            }

            CurrentGridData = result.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading page {PageNumber}", pageNumber);
            HasError = true;
            ErrorMessage = ex.Message;
        }
        finally
        {
            IsLoading = false;
            NotifyStateChanged();
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

    private void NotifyStateChanged() => OnStateChanged?.Invoke();
}
