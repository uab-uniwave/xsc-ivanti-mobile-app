using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace HeatReplayClient;

public sealed class Program
{
    public static async Task<int> Main(string[] args)
    {
        var options = ReplayOptions.FromEnvironment();

        if (string.IsNullOrWhiteSpace(options.BaseUrl))
        {
            Console.Error.WriteLine("Set HEAT_BASE_URL, for example: https://stg-heat20254.synergy.lt");
            return 1;
        }

        Directory.CreateDirectory(options.OutputDirectory);

        using var handler = new HttpClientHandler
        {
            CookieContainer = new CookieContainer(),
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate | DecompressionMethods.Brotli
        };

        using var http = new HttpClient(handler)
        {
            BaseAddress = new Uri(options.BaseUrl.TrimEnd('/') + "/"),
            Timeout = TimeSpan.FromMinutes(2)
        };

        http.DefaultRequestHeaders.Accept.Clear();
        http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
        http.DefaultRequestHeaders.UserAgent.ParseAdd("HeatReplayClient/1.0");

        var client = new HeatApiClient(http, options);

        try
        {
            Console.WriteLine("1. InitializeSession");
            var init = await client.InitializeSessionAsync();
            await client.SaveResponseAsync("01_InitializeSession", init);

            Console.WriteLine("2. GetUserData");
            var userData = await client.GetUserDataAsync();
            await client.SaveResponseAsync("02_GetUserData", userData);

            Console.WriteLine("3. GetRoleWorkspaces");
            var roleWorkspaces = await client.GetRoleWorkspacesAsync();
            await client.SaveResponseAsync("03_GetRoleWorkspaces", roleWorkspaces);

            Console.WriteLine("4. GetWorkspaceData");
            var workspaceData = await client.GetWorkspaceDataAsync(
                options.WorkspaceObjectId,
                options.WorkspaceLayoutName);
            await client.SaveResponseAsync("04_GetWorkspaceData", workspaceData);

            Console.WriteLine("5. FindFormViewData (new record)");
            var formViewData = await client.FindFormViewDataAsync(
                options.FormLayoutName,
                options.FormViewName,
                isNewRecord: true,
                objectId: null);
            await client.SaveResponseAsync("05_FindFormViewData_New", formViewData);

            Console.WriteLine("Done. Responses saved to: " + Path.GetFullPath(options.OutputDirectory));
            return 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Replay failed:");
            Console.Error.WriteLine(ex);
            return 2;
        }
    }
}

public sealed record ReplayOptions
{
    public string BaseUrl { get; init; } = "";
    public string? RestApiKey { get; init; }
    public int TzOffsetMinutes { get; init; } = -120;
    public string OutputDirectory { get; init; } = "responses";
    public string WorkspaceObjectId { get; init; } = "Incident#";
    public string WorkspaceLayoutName { get; init; } = "IncidentLayout.ResponsiveAnalyst";
    public string FormLayoutName { get; init; } = "IncidentLayout.ResponsiveAnalyst";
    public string FormViewName { get; init; } = "responsive.analyst.new";

    public static ReplayOptions FromEnvironment() => new()
    {
        BaseUrl = Get("HEAT_BASE_URL", ""),
        RestApiKey = Get("HEAT_REST_API_KEY", null),
        TzOffsetMinutes = int.TryParse(Get("HEAT_TZOFFSET", "-120"), out var tz) ? tz : -120,
        OutputDirectory = Get("HEAT_OUTPUT_DIR", "responses"),
        WorkspaceObjectId = Get("HEAT_WORKSPACE_OBJECT_ID", "Incident#"),
        WorkspaceLayoutName = Get("HEAT_WORKSPACE_LAYOUT", "IncidentLayout.ResponsiveAnalyst"),
        FormLayoutName = Get("HEAT_FORM_LAYOUT", "IncidentLayout.ResponsiveAnalyst"),
        FormViewName = Get("HEAT_FORM_VIEW", "responsive.analyst.new")
    };

    private static string? Get(string key, string? fallback) =>
        Environment.GetEnvironmentVariable(key) ?? fallback;
}

public sealed class HeatApiClient
{
    private readonly HttpClient _http;
    private readonly ReplayOptions _options;
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true
    };

    public HeatApiClient(HttpClient http, ReplayOptions options)
    {
        _http = http;
        _options = options;
    }

    public string? CsrfToken { get; private set; }

    public async Task<JsonNode?> InitializeSessionAsync(CancellationToken cancellationToken = default)
    {
        using var request = BuildJsonPost(
            "HEAT/Services/Session.asmx/InitializeSession",
            new
            {
                _csrfToken = (string?)null
            },
            includeCsrf: false,
            includeRestApiKey: true);

        var json = await SendAsync(request, cancellationToken);
        CsrfToken = json?["d"]?["SessionCsrfToken"]?.GetValue<string>();
        return json;
    }

    public async Task<JsonNode?> GetUserDataAsync(CancellationToken cancellationToken = default)
    {
        EnsureCsrfToken();
        using var request = BuildJsonPost(
            "HEAT/Services/Session.asmx/GetUserData",
            new
            {
                tzoffset = _options.TzOffsetMinutes,
                _csrfToken = CsrfToken
            });

        return await SendAsync(request, cancellationToken);
    }

    public async Task<JsonNode?> GetRoleWorkspacesAsync(CancellationToken cancellationToken = default)
    {
        EnsureCsrfToken();
        using var request = BuildJsonPost(
            "HEAT/Services/Workspace.asmx/GetRoleWorkspaces",
            new
            {
                sRole = (string?)null,
                _csrfToken = CsrfToken
            });

        return await SendAsync(request, cancellationToken);
    }

    public async Task<JsonNode?> GetWorkspaceDataAsync(
        string objectId,
        string layoutName,
        CancellationToken cancellationToken = default)
    {
        EnsureCsrfToken();
        using var request = BuildJsonPost(
            "HEAT/Services/Workspace.asmx/GetWorkspaceData",
            new
            {
                ObjectId = objectId,
                LayoutName = layoutName,
                _csrfToken = CsrfToken
            });

        return await SendAsync(request, cancellationToken);
    }

    public async Task<JsonNode?> FindFormViewDataAsync(
        string layoutName,
        string viewName,
        bool isNewRecord,
        string? objectId,
        CancellationToken cancellationToken = default)
    {
        EnsureCsrfToken();
        using var request = BuildJsonPost(
            "HEAT/Services/Workspace.asmx/FindFormViewData",
            new
            {
                createdViewsOnClient = new { },
                isNewRecord,
                layoutName,
                objectId,
                viewName,
                _csrfToken = CsrfToken
            });

        return await SendAsync(request, cancellationToken);
    }

    public async Task SaveResponseAsync(string name, JsonNode? json, CancellationToken cancellationToken = default)
    {
        var safeName = string.Concat(name.Select(ch =>
            Path.GetInvalidFileNameChars().Contains(ch) ? '_' : ch));

        var filePath = Path.Combine(_options.OutputDirectory, $"{safeName}.json");
        var content = json?.ToJsonString(_jsonOptions) ?? "null";
        await File.WriteAllTextAsync(filePath, content, Encoding.UTF8, cancellationToken);
    }

    private HttpRequestMessage BuildJsonPost(
        string relativeUrl,
        object body,
        bool includeCsrf = true,
        bool includeRestApiKey = false)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, relativeUrl)
        {
            Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json")
        };

        if (includeRestApiKey && !string.IsNullOrWhiteSpace(_options.RestApiKey))
        {
            request.Headers.TryAddWithoutValidation("authorization", $"rest_api_key={_options.RestApiKey}");
        }

        if (includeCsrf && string.IsNullOrWhiteSpace(CsrfToken))
        {
            throw new InvalidOperationException("CSRF token is missing. Call InitializeSessionAsync first.");
        }

        return request;
    }

    private async Task<JsonNode?> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
        var text = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"HTTP {(int)response.StatusCode} {response.ReasonPhrase}{Environment.NewLine}{text}");
        }

        if (string.IsNullOrWhiteSpace(text))
            return null;

        try
        {
            return JsonNode.Parse(text);
        }
        catch (JsonException)
        {
            return new JsonObject
            {
                ["raw"] = text
            };
        }
    }

    private void EnsureCsrfToken()
    {
        if (string.IsNullOrWhiteSpace(CsrfToken))
            throw new InvalidOperationException("CSRF token is missing. Call InitializeSessionAsync first.");
    }
}
