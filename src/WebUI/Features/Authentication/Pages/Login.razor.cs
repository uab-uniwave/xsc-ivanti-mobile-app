using Microsoft.AspNetCore.Components;
using WebUI.Features.Authentication.ViewModels;

namespace WebUI.Features.Authentication.Pages;

/// <summary>
/// Code-behind for the Login page.
/// Handles component lifecycle and delegates logic to LoginViewModel.
/// </summary>
public partial class Login : ComponentBase
{
    [Inject] private LoginViewModel ViewModel { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await ViewModel.InitializeAsync();
    }

    private async Task HandleLogin()
    {
        await ViewModel.HandleLoginAsync();
        StateHasChanged();
    }
}
