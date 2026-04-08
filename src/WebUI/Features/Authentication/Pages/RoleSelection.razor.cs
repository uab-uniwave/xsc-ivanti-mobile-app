using Microsoft.AspNetCore.Components;
using WebUI.Features.Authentication.ViewModels;

namespace WebUI.Features.Authentication.Pages;

/// <summary>
/// Code-behind for the RoleSelection page.
/// Handles component lifecycle and delegates logic to RoleSelectionViewModel.
/// Uses OnAfterRenderAsync to ensure JS interop is available for localStorage access.
/// </summary>
public partial class RoleSelection : ComponentBase
{
    [Inject] private RoleSelectionViewModel ViewModel { get; set; } = default!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await ViewModel.InitializeAsync();
            StateHasChanged();
        }
    }
}
