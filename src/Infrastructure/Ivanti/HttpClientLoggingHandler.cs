using System.Text;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Ivanti;

/// <summary>
/// HTTP DelegatingHandler that logs all HTTP requests and responses.
/// Logs include URI, headers, and content for debugging and monitoring purposes.
/// </summary>
public sealed class HttpClientLoggingHandler : DelegatingHandler
{
    private readonly ILogger<HttpClientLoggingHandler> _logger;

    public HttpClientLoggingHandler(ILogger<HttpClientLoggingHandler> logger)
    {
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        try
        {
            // Log outgoing request
            await LogRequestAsync(request, cancellationToken);

            // Send the request
            var response = await base.SendAsync(request, cancellationToken);

            // Log incoming response
            await LogResponseAsync(response, cancellationToken);

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "HTTP request failed for {Method} {Uri}", 
                request.Method, request.RequestUri);
            throw;
        }
    }

    private async Task LogRequestAsync(HttpRequestMessage request, CancellationToken ct)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("===== HTTP REQUEST =====");
        stringBuilder.AppendLine($"Method: {request.Method}");
        stringBuilder.AppendLine($"URI: {request.RequestUri}");

        // Log headers
        stringBuilder.AppendLine("\n--- HEADERS ---");
        foreach (var header in request.Headers)
        {
            stringBuilder.AppendLine($"{header.Key}: {string.Join(", ", header.Value)}");
        }

        // Log content headers if present
        if (request.Content?.Headers != null)
        {
            foreach (var header in request.Content.Headers)
            {
                stringBuilder.AppendLine($"{header.Key}: {string.Join(", ", header.Value)}");
            }
        }

        // Log body content
        if (request.Content != null)
        {
            stringBuilder.AppendLine("\n--- BODY ---");
            try
            {
                var content = await request.Content.ReadAsStringAsync(ct);
                if (!string.IsNullOrWhiteSpace(content))
                {
                    stringBuilder.AppendLine(content.Length > 1000 
                        ? content.Substring(0, 1000) + "... [truncated]"
                        : content);
                }
                else
                {
                    stringBuilder.AppendLine("(empty)");
                }
            }
            catch (Exception ex)
            {
                stringBuilder.AppendLine($"(error reading content: {ex.Message})");
            }
        }
        else
        {
            stringBuilder.AppendLine("\n--- BODY ---");
            stringBuilder.AppendLine("(no content)");
        }

        stringBuilder.AppendLine("========================\n");

        _logger.LogInformation(stringBuilder.ToString());
    }

    private async Task LogResponseAsync(HttpResponseMessage response, CancellationToken ct)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("===== HTTP RESPONSE =====");
        stringBuilder.AppendLine($"Status Code: {(int)response.StatusCode} {response.StatusCode}");
        stringBuilder.AppendLine($"URI: {response.RequestMessage?.RequestUri}");

        // Log headers
        stringBuilder.AppendLine("\n--- HEADERS ---");
        foreach (var header in response.Headers)
        {
            // Sanitize sensitive headers
            var value = IsSensitiveHeader(header.Key)
                ? "***[REDACTED]***"
                : string.Join(", ", header.Value);
            stringBuilder.AppendLine($"{header.Key}: {value}");
        }

        // Log content headers if present
        if (response.Content?.Headers != null)
        {
            foreach (var header in response.Content.Headers)
            {
                stringBuilder.AppendLine($"{header.Key}: {string.Join(", ", header.Value)}");
            }
        }

        // Log body content
        if (response.Content != null)
        {
            stringBuilder.AppendLine("\n--- BODY ---");
            try
            {
                var content = await response.Content.ReadAsStringAsync(ct);
                if (!string.IsNullOrWhiteSpace(content))
                {
                    stringBuilder.AppendLine(content.Length > 1000 
                        ? content.Substring(0, 1000) + "... [truncated]"
                        : content);
                }
                else
                {
                    stringBuilder.AppendLine("(empty)");
                }
            }
            catch (Exception ex)
            {
                stringBuilder.AppendLine($"(error reading content: {ex.Message})");
            }
        }
        else
        {
            stringBuilder.AppendLine("\n--- BODY ---");
            stringBuilder.AppendLine("(no content)");
        }

        stringBuilder.AppendLine("==========================\n");

        var logLevel = response.IsSuccessStatusCode 
            ? LogLevel.Information 
            : LogLevel.Warning;

        _logger.Log(logLevel, stringBuilder.ToString());
    }

    /// <summary>
    /// Determines if a header should be redacted for security purposes.
    /// </summary>
    private static bool IsSensitiveHeader(string headerName)
    {
        var sensitiveHeaders = new[] 
        { 
            "Authorization", 
            "Cookie", 
            "X-API-Key", 
            "X-Auth-Token",
            "Password",
            "Token"
        };

        return sensitiveHeaders.Any(h => 
            h.Equals(headerName, StringComparison.OrdinalIgnoreCase));
    }
}
