using System.Net;
using System.Net.Http.Headers;
using Application.Interfaces.Authentication;
using Application.Interfaces.State;
using Application.Interfaces.Workspaces;
using Application.Services;
using Infrastructure.Authentication;
using Infrastructure.Ivanti;
using Infrastructure.Ivanti.Configuration;
using Infrastructure.Mapping;
using Infrastructure.Workspaces;
using Infrastructure.State;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Serilog;

namespace Infrastructure;

public static class DependencyInjection
{
    // Static CookieContainer shared across all HttpClient instances for session management
    private static readonly CookieContainer SharedCookieContainer = new();

    // Flag to track if cookie container ref has been set
    private static bool _cookieRefSet = false;

    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration config)
    {
        // ======================================================
        // OPTIONS (Strongly Typed Config)
        // ======================================================

        services.AddOptions<IvantiOptions>()
           .Bind(config.GetSection("Ivanti"))
           .ValidateDataAnnotations();

        // ======================================================
        // SERILOG
        // ======================================================

        var serilogLogger = new LoggerConfiguration()
            .ReadFrom.Configuration(config)
            .Enrich.FromLogContext()
            .CreateLogger();

        Log.Logger = serilogLogger;

        services.AddSingleton<Serilog.ILogger>(serilogLogger);

        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.AddSerilog(serilogLogger, dispose: true);
        });

        // ======================================================
        // MAPSTER
        // ======================================================

        MapsterConfig.RegisterMappings();

        services.AddSingleton(TypeAdapterConfig.GlobalSettings);
        services.AddScoped<IMapper, ServiceMapper>();

        // ======================================================
        // IVANTI ENDPOINTS (Factory-Based)
        // ======================================================

        services.AddSingleton<IvantiEndpoints>(sp =>
        {
            var options = sp
                .GetRequiredService<IOptions<IvantiOptions>>()
                .Value;

            if (string.IsNullOrWhiteSpace(options.BaseUrl))
                throw new InvalidOperationException("Ivanti BaseUrl not configured.");

            // Initialize the cookie container manager for debugging and cleanup
            CookieContainerManager.Initialize(SharedCookieContainer, options.BaseUrl);

            return new IvantiEndpoints(options.BaseUrl);
        });

        // ======================================================
        // HTTP CLIENT (Typed Client with Shared Cookie Container)
        // ======================================================

        services.AddScoped<HttpClientLoggingHandler>();

        // Register IvantiClient as the typed client for IIvantiClient
        // Use a new handler per client but share the CookieContainer
        services.AddHttpClient<IIvantiClient, IvantiClient>((sp, client) =>
        {
            var options = sp
                .GetRequiredService<IOptions<IvantiOptions>>()
                .Value;

            if (string.IsNullOrWhiteSpace(options.BaseUrl))
                throw new InvalidOperationException("Ivanti BaseUrl not configured.");

            client.BaseAddress = new Uri(options.BaseUrl);
            client.DefaultRequestHeaders.TryAddWithoutValidation(
                "Authorization",
                $"rest_api_key={options.ApiKey}");
        })
        .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
        {
            CookieContainer = SharedCookieContainer,
            UseCookies = true,
            AllowAutoRedirect = false
        })
        .AddHttpMessageHandler<HttpClientLoggingHandler>();

        // ======================================================
        // APPLICATION SERVICES
        // ======================================================

        // Register IAuthenticationService - shares the same CookieContainer
        services.AddHttpClient<IAuthenticationService, AuthenticationService>((sp, client) =>
        {
            var options = sp
                .GetRequiredService<IOptions<IvantiOptions>>()
                .Value;

            if (string.IsNullOrWhiteSpace(options.BaseUrl))
                throw new InvalidOperationException("Ivanti BaseUrl not configured.");

            client.BaseAddress = new Uri(options.BaseUrl);
        })
        .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
        {
            CookieContainer = SharedCookieContainer,
            UseCookies = true,
            AllowAutoRedirect = false
        });

        // Register IWorkspaceService
        services.AddScoped<IWorkspaceService, WorkspaceService>();

        // Register Ivanti State Service (must be Scoped to persist within Blazor circuit)
        services.AddScoped<IIvantiStateService,IvantiStateService>();

        // Register ISessionValidator for session restoration workflow
        services.AddScoped<ISessionValidator, SessionValidator>();

        // Register ICookieManager for cookie restoration from localStorage
        services.AddSingleton<ICookieManager, CookieManager>();

        // Set the cookie container reference for debugging (only once)
        if (!_cookieRefSet)
        {
            SessionValidator.SetCookieContainerRef(SharedCookieContainer);
            _cookieRefSet = true;
        }

        return services;
    }
}
