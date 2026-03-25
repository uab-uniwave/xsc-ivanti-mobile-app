using Application.Common;
using Application.DTOs.Incident;
using Application.Services;
using WebUI.Components.Pages;

namespace WebUI.Components.ViewModels;

public sealed class IncidentsViewModel
{
    private readonly IIvantiClient _ivanti;

    public List<IncidentListItemDto> Items { get; private set; } = new();

    public bool IsLoading { get; private set; }
    public bool HasError { get; private set; }
    public string? ErrorMessage { get; private set; }

    public IncidentsViewModel(IIvantiClient ivanti)
    {
        _ivanti = ivanti;
    }
    public async Task LoadFirstPageAsync(CancellationToken ct)
    {
        IsLoading = true;
        HasError = false;
        ErrorMessage = null;

        try
        {
            // Initialize session (required for all other calls)
            var sessionResult = await _ivanti.InitializeSessionAsync(ct);
            if (sessionResult.IsFailure)
            {
                throw new InvalidOperationException($"Failed to initialize session: {sessionResult.Error}");
            }

            // Get user data
            var userDataResult = await _ivanti.GetUserDataAsync(ct);
            if (userDataResult.IsFailure)
            {
                throw new InvalidOperationException($"Failed to get user data: {userDataResult.Error}");
            }

            // Get role workspaces
            var workspaceRoleResult = await _ivanti.GetRoleWorkspacesAsync(ct);
            if (workspaceRoleResult.IsFailure)
            {
                throw new InvalidOperationException($"Failed to get role workspaces: {workspaceRoleResult.Error}");
            }

            // Get workspace data
            var workspaceDataResult = await _ivanti.GetWorkspaceDataAsync(ct);
            if (workspaceDataResult.IsFailure)
            {
                throw new InvalidOperationException($"Failed to get workspace data: {workspaceDataResult.Error}");
            }

            // Get form view data
            var formViewDataResult = await _ivanti.FindFormViewDataAsync(ct);
            if (formViewDataResult.IsFailure)
            {
                throw new InvalidOperationException($"Failed to get form view data: {formViewDataResult.Error}");
            }
            // Get form view data
            var formDataDefaultReult = await _ivanti.GetFormDefaultDataAsync(ct);
            if (formDataDefaultReult.IsFailure)
            {
                throw new InvalidOperationException($"Failed to get form view data: {formDataDefaultReult.Error}");
            }
        }

            //// Load incidents
            //        //var query = new PagedQuery
            //        //{
            //        //    Page = 1,
            //            PageSize = 20
            //        };

        //        var result = await _ivanti.GetIncidentsAsync(query, ct);

        //        if (result.IsFailure)
        //        {
        //            HasError = true;
        //            ErrorMessage = result.Error;
        //        }
        //        else
        //        {
        //            Items = result.Value!.Items.ToList();
        //        }
   
        catch (Exception ex)
        {
            HasError = true;
            ErrorMessage = ex.Message;
        }
        finally
        {
            IsLoading = false;
        }
    }
    public async Task LoadAsync()
    {
        IsLoading = true;
        HasError = false;
        ErrorMessage = null;

        try
        {
            var query = new PagedQuery
            {
                Page = 1,
                PageSize = 20
            };

            ////    var result = await _ivanti.GetIncidentsAsync(query, CancellationToken.None);

            //    if (result.IsFailure)
            //    {
            //        HasError = true;
            //        ErrorMessage = result.Error;
            //    }
            //    else
            //    {
            //        Items = result.Value!.Items.ToList();
            //    }
        }
        catch (Exception ex)
        {
            HasError = true;
            ErrorMessage = ex.Message;
        }
        finally
        {
            IsLoading = false;
        }
    }
}