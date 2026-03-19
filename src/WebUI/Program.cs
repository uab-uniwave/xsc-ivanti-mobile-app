using MudBlazor.Services;
using WebUI.Components;
using Infrastructure;
using WebUI.Components.ViewModels; // IMPORTANT

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();

// Add Infrastructure services (THIS WAS MISSING)
builder.Services.AddInfrastructure(builder.Configuration);

// Add Razor Components
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register ViewModel
builder.Services.AddScoped<IncidentsViewModel>();

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