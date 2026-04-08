using Microsoft.AspNetCore.Components;
using WebUI.Features.Authentication.ViewModels;

namespace WebUI.Features.Authentication.Pages;

/// <summary>
/// Code-behind for the SelectRole page.
/// Handles component lifecycle and delegates logic to SelectRoleViewModel.
/// </summary>
public partial class SelectRole : ComponentBase
{
    [Inject] private SelectRoleViewModel ViewModel { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await ViewModel.InitializeAsync();
    }
}
