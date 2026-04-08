using System.Net.Http.Headers;
using Application.Interfaces.Authentication;
using Application.Interfaces.Workspaces;
using Application.Services;
using Infrastructure.Authentication;
using Infrastructure.Ivanti;
using Infrastructure.Ivanti.Configuration;
using Infrastructure.Mapping;
using Infrastructure.Workspaces;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Serilog;

namespace Infrastructure;

public static class DependencyInjection
{
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

            return new IvantiEndpoints(options.BaseUrl);
        });

        // ======================================================
        // HTTP CLIENT (Typed Client with Logging)
        // ======================================================

        services.AddScoped<HttpClientLoggingHandler>();

        // Register IvantiClient as the typed client for IIvantiClient
        // so the configured HttpClient (BaseAddress + headers) is injected
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
            client.DefaultRequestHeaders.TryAddWithoutValidation(
                "Cookie", options.Cookie);
        })
        .AddHttpMessageHandler<HttpClientLoggingHandler>();

        // ======================================================
        // APPLICATION SERVICES
        // ======================================================

        // Register IAuthenticationService with its own HttpClient for login page access
        services.AddHttpClient<IAuthenticationService, AuthenticationService>((sp, client) =>
        {
            var options = sp
                .GetRequiredService<IOptions<IvantiOptions>>()
                .Value;

            if (string.IsNullOrWhiteSpace(options.BaseUrl))
                throw new InvalidOperationException("Ivanti BaseUrl not configured.");

            client.BaseAddress = new Uri(options.BaseUrl);
            client.DefaultRequestHeaders.TryAddWithoutValidation(
                "Cookie", options.Cookie);
        });

        // Register IWorkspaceService
        services.AddScoped<IWorkspaceService, WorkspaceService>();

        return services;
    }
}
