using Application.Common;
using Application.DTOs.Incident;
using Application.Services;
using WebUI.Components.Pages;

namespace WebUI.Components.ViewModels;

public sealed class IncidentsViewModel
{
    private readonly IIvantiService _ivanti;

    public List<IncidentListItemDto> Items { get; private set; } = new();

    public bool IsLoading { get; private set; }
    public bool HasError { get; private set; }
    public string? ErrorMessage { get; private set; }

    public IncidentsViewModel(IIvantiService ivanti)
    {
        _ivanti = ivanti;
    }
    public async Task LoadFirstPageAsync(CancellationToken ct)
    {
        IsLoading = true;
        HasError = false;

        var query = new PagedQuery
        {
            Page = 1,
            PageSize = 20
        };

        var result = await _ivanti.GetIncidentsAsync(query, ct);

        if (result.IsFailure)
        {
            HasError = true;
            ErrorMessage = result.Error;
        }
        else
        {
            Items = result.Value!.Items.ToList();
        }

        IsLoading = false;
    }
    public async Task LoadAsync()
    {
        IsLoading = true;
        HasError = false;

        var query = new PagedQuery
        {
            Page = 1,
            PageSize = 20
        };

        var result = await _ivanti.GetIncidentsAsync(query, CancellationToken.None);

        if (result.IsFailure)
        {
            HasError = true;
            ErrorMessage = result.Error;
        }
        else
        {
            Items = result.Value!.Items.ToList();
        }

        IsLoading = false;
    }
}