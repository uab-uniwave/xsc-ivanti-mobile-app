using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.ValidatedSearch;

public class SearchRights
{
    [JsonPropertyName("CanEdit")]
    public bool CanEdit { get; set; }

    [JsonPropertyName("CanDelete")]
    public bool CanDelete { get; set; }

    [JsonPropertyName("CanShare")]
    public bool CanShare { get; set; }
}
