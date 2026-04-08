using Microsoft.AspNetCore.Components;
using WebUI.Features.Authentication.ViewModels;

namespace WebUI.Features.Authentication.Pages;

/// <summary>
/// Code-behind for the Login page.
/// Handles component lifecycle and delegates logic to LoginViewModel.
/// Uses OnAfterRenderAsync to ensure JS interop is available for localStorage.
/// </summary>
public partial class Login : ComponentBase
{
    [Inject] private LoginViewModel ViewModel { get; set; } = default!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            // Initialize only after render when JS interop is available
            await ViewModel.InitializeAsync();
            StateHasChanged();
        }
    }

    private async Task HandleLogin()
    {
        await ViewModel.HandleLoginAsync();
        StateHasChanged();
    }
}
