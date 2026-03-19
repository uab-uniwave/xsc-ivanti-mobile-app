using System.Net.Http.Headers;
using Application.Services;
using Infrastructure.Ivanti;
using Infrastructure.Ivanti.Configuration;
using Infrastructure.Mapping;
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
        // HTTP CLIENT (Typed Client)
        // ======================================================

        services.AddHttpClient<IIvantiService, IvantiClient>((sp, client) =>
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
        });

        return services;
    }
}