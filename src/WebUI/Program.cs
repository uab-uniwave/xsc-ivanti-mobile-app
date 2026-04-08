using MudBlazor.Services;
using WebUI.Components;
using Infrastructure;
using WebUI.Features.Incidents.ViewModels;
using WebUI.Features.Authentication.ViewModels;
using WebUI.Services;
using Application.Interfaces.Navigation;
using Application.Interfaces.Storage;

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();

// Add Infrastructure services
builder.Services.AddInfrastructure(builder.Configuration);

// Add Razor Components
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register ViewModels
builder.Services.AddScoped<IncidentsViewModel>();
builder.Services.AddScoped<LoginViewModel>();
builder.Services.AddScoped<RoleSelectionViewModel>();

// Register Navigation Service
builder.Services.AddScoped<INavigationService, NavigationService>();

// Register Client Storage Service (localStorage)
builder.Services.AddScoped<IClientStorageService, LocalStorageService>();

// Register Legacy IvantiNavigationService for backward compatibility
builder.Services.AddScoped<IvantiNavigationService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();