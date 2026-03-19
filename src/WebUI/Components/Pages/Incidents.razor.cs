using Microsoft.AspNetCore.Components;
using MudBlazor;
using WebUI.Components.ViewModels;

namespace WebUI.Components.Pages;

public partial class Incidents : ComponentBase
{
    [Inject]
    public IDialogService DialogService { get; set; } = default!;
    [Inject]
    public IncidentsViewModel ViewModel { get; set; } = default!;


    [Inject]
    private IDialogService Dialog { get; set; } = default!;


    
    protected override async Task OnInitializedAsync()
    {
        await ViewModel.LoadFirstPageAsync(CancellationToken.None);
    }

    private async Task OpenEditDialog(string id)
    {
        var parameters = new DialogParameters
    {
        { "Id", id }
    };

        await Dialog.ShowAsync<EditIncidentDialog>(
            "Edit Incident",
            parameters);
    }

}
