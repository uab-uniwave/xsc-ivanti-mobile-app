using System.Text.Json;
using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.GridDataHandler;

/// <summary>
/// Metadata returned by the Ivanti GridDataHandler response.
/// Contains grid layout definition, field mappings, column definitions, paging, and sort info.
/// </summary>
public class GridDataMetaData
{
    /// <summary>
    /// Whether the user has permission to view this grid.
    /// </summary>
    [JsonPropertyName("allowView")]
    public bool AllowView { get; set; }

    /// <summary>
    /// Grid definition name (e.g., "Incident.ResponsiveAnalyst.List").
    /// </summary>
    [JsonPropertyName("gridDefName")]
    public string? GridDefName { get; set; }

    /// <summary>
    /// Timestamp of the last modification to the grid definition.
    /// </summary>
    [JsonPropertyName("lastModified")]
    public string? LastModified { get; set; }

    /// <summary>
    /// Whether noise words were removed from the search query.
    /// </summary>
    [JsonPropertyName("noiseWordsRemoved")]
    public bool NoiseWordsRemoved { get; set; }

    /// <summary>
    /// Whether the search results are ranked by relevance.
    /// </summary>
    [JsonPropertyName("resultsAreRanked")]
    public bool ResultsAreRanked { get; set; }

    /// <summary>
    /// JSON property name that contains the row data (typically "rows").
    /// </summary>
    [JsonPropertyName("root")]
    public string? Root { get; set; }

    /// <summary>
    /// JSON property name that contains the row identifier (typically "id").
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("pagingInfo")]
    public GridDataPagingInfo? PagingInfo { get; set; }

    /// <summary>
    /// Whether to show group detail rows when grouping is enabled.
    /// </summary>
    [JsonPropertyName("showGroupDetails")]
    public bool ShowGroupDetails { get; set; }

    [JsonPropertyName("sortInfo")]
    public GridDataSortInfo? SortInfo { get; set; }

    /// <summary>
    /// Whether records should open in a new browser tab.
    /// </summary>
    [JsonPropertyName("openInNewTab")]
    public bool OpenInNewTab { get; set; }

    /// <summary>
    /// Related object reference, if any. Structure varies by context.
    /// </summary>
    [JsonPropertyName("relatedObject")]
    public JsonElement? RelatedObject { get; set; }

    [JsonPropertyName("fields")]
    public List<GridDataField>? Fields { get; set; }

    [JsonPropertyName("columns")]
    public List<GridDataMetaColumn>? Columns { get; set; }

    /// <summary>
    /// Object type identifier for the grid rows (e.g., "Incident#").
    /// </summary>
    [JsonPropertyName("objectType")]
    public string? ObjectType { get; set; }

    /// <summary>
    /// Expression used for conditional CSS class styling of entire grid rows.
    /// </summary>
    [JsonPropertyName("rowClassExpression")]
    public GridDataClassExpresin? RowClassExpression { get; set; }
}
