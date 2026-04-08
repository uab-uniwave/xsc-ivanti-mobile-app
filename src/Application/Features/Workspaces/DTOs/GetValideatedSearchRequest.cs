using System;
using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.DTOs;

/// <summary>
/// Request DTO for GetValidatedSearch API endpoint.
/// Sent to Ivanti API to retrieve validated search definition and results.
/// </summary>
public class GetValideatedSearchRequest
{
    /// <summary>
    /// The ID of the saved search to retrieve.
    /// Obtained from WorkspaceData.SearchData.Favorites[x].Id
    /// </summary>
    [JsonPropertyName("SearchId")]
    public Guid? SearchId { get; init; }

    [JsonPropertyName("LayoutName")]
    public string? LayoutName { get; init; }

    [JsonPropertyName("ObjectId")]
    public string? ObjectId { get; init; }

    [JsonPropertyName("_csrfToken")]
    public string? CsrfToken { get; init; }
}
