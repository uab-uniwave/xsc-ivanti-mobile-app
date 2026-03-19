using Application.DTOs.Incident;
using Application.Services;
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
    private IIvantiService IvantiService { get; set; } = default!;

    private IncidentDto Model { get; set; } = new();
    private bool IsLoading;

    protected override async Task OnInitializedAsync()
    {
        IsLoading = true;

        var result = await IvantiService.GetIncidentByIdAsync(Id, CancellationToken.None);

        if (result.IsSuccess)
            Model = result.Value!;

        IsLoading = false;
    }

    private async Task Save()
    {
        var request = new IncidentUpdateRequest
        {
            Status = Model.Status,
            Priority = Model.Priority,
            Service = Model.Service,
            Category = Model.Category,
            Urgency = Model.Urgency,
            Impact = Model.Impact,
            Owner = Model.Owner,
            OwnerTeam = Model.OwnerTeam,
            Subject = Model.Subject,
            Description = Model.Description
        };

        await IvantiService.UpdateIncidentAsync(Id, request);

        MudDialog.Close(DialogResult.Ok(true));
    }

    private void Cancel()
    {
        MudDialog.Cancel();
    }
}