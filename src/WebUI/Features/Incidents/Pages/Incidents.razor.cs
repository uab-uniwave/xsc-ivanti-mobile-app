using Microsoft.AspNetCore.Components;
using MudBlazor;
using WebUI.Features.Incidents.ViewModels;

namespace WebUI.Features.Incidents.Pages;

public partial class Incidents : ComponentBase
{
    [Inject]
    public IDialogService DialogService { get; set; } = default!;

    [Inject]
    public IncidentsViewModel ViewModel { get; set; } = default!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    [Inject]
    public ISnackbar Snackbar { get; set; } = default!;
}
