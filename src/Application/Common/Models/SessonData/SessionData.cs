using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Application.Common.Models.SessonData;


public class SessionData
{
    [JsonPropertyName("__type")]
    public string? Type { get; set; }

    [JsonPropertyName("SessionCsrfToken")]
    public string? SessionCsrfToken { get; set; }

    [JsonPropertyName("UserName")]
    public string? UserName { get; set; }

    [JsonPropertyName("TimezoneName")]
    public string? TimezoneName { get; set; }

    [JsonPropertyName("TimeZoneOffset")]
    public int? TimeZoneOffset { get; set; }

    [JsonPropertyName("IsPortalModeActivated")]
    public bool? IsPortalModeActivated { get; set; }

    [JsonPropertyName("AvailableLanguages")]
    public List<AvailableLanguage> AvailableLanguages { get; set; } = new();
}

public class AvailableLanguage
{
    [JsonPropertyName("IsoCode")]
    public string? IsoCode { get; set; }

    [JsonPropertyName("Name")]
    public string? Name { get; set; }

    [JsonPropertyName("NativeName")]
    public string? NativeName { get; set; }
}
