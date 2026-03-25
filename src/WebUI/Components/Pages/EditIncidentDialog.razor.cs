using Application.DTOs.Incident;
using Application.Services;
using Infrastructure.Ivanti;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace WebUI.Components.Pages;

public partial class EditIncidentDialog
{
    [CascadingParameter]
    private IMudDialogInstance MudDialog { get; set; } = default!;

    [Parameter]
    public string Id { get; set; } = default!;

    [Inject]
    private IIvantiClient IvantiClient { get; set; } = default!;

    private IncidentDto Model { get; set; } = new();
    private bool IsLoading;

    protected override async Task OnInitializedAsync()
    {
        IsLoading = true;

                   

        IsLoading = false;
    }



  
 
  
       
  
}