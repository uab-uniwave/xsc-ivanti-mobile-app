using Microsoft.AspNetCore.Components;
using MudBlazor;
using WebUI.Features.Incidents.ViewModels;

namespace WebUI.Features.Incidents.Pages;

public partial class Incidents : ComponentBase, IDisposable
{
    [Inject]
    public IDialogService DialogService { get; set; } = default!;

    [Inject]
    public IncidentsViewModel ViewModel { get; set; } = default!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    [Inject]
    public ISnackbar Snackbar { get; set; } = default!;

    protected override void OnInitialized()
    {
        // Subscribe to ViewModel state changes
        ViewModel.OnStateChanged += StateHasChanged;
    }

    public void Dispose()
    {
        // Unsubscribe to prevent memory leaks
        ViewModel.OnStateChanged -= StateHasChanged;
    }
}
