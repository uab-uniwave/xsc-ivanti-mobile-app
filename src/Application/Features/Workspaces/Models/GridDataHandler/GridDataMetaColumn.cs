using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.GridDataHandler;

/// <summary>
/// Represents a column definition in the Ivanti GridDataHandler response.
/// Maps to the "columns" array items within "metaData".
/// Also used as the application-layer column model after mapping.
/// </summary>
public class GridDataMetaColumn
{
    /// <summary>
    /// Column header text displayed to the user.
    /// </summary>
    [JsonPropertyName("header")]
    public string? Header { get; set; }

    /// <summary>
    /// Column width in pixels as defined by the Ivanti grid layout.
    /// </summary>
    [JsonPropertyName("width")]
    public int? Width { get; set; }

    /// <summary>
    /// Field name used to look up values in each data row.
    /// </summary>
    [JsonPropertyName("dataIndex")]
    public string? DataIndex { get; set; }

    /// <summary>
    /// Whether the column supports sorting.
    /// </summary>
    [JsonPropertyName("sortable")]
    public bool Sortable { get; set; }

    /// <summary>
    /// Whether the column supports grouping.
    /// </summary>
    [JsonPropertyName("groupable")]
    public bool Groupable { get; set; }

    /// <summary>
    /// Optional custom renderer name for the column.
    /// </summary>
    [JsonPropertyName("columnRenderer")]
    public string? ColumnRenderer { get; set; }

    /// <summary>
    /// Number of rows this column spans in the grid layout.
    /// </summary>
    [JsonPropertyName("rowSpan")]
    public int RowSpan { get; set; }

    /// <summary>
    /// Unique column identifier within the grid definition.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Optional CSS class expression used for conditional styling of column cells.
    /// </summary>
    [JsonPropertyName("classExpression")]
    public GridDataClassExpresin? ClassExpression { get; set; }

    /// <summary>
    /// Optional image expression used for conditional image rendering in column cells.
    /// Shares the same structure as <see cref="ClassExpression"/>.
    /// </summary>
    [JsonPropertyName("imageExpression")]
    public GridDataClassExpresin? ImageExpression { get; set; }

    /// <summary>
    /// Data type of the column (e.g., "string", "int", "boolean", "datetime").
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    //=====================================================================
    // Application-layer properties (set during mapping, not from JSON)
    //=====================================================================

    /// <summary>
    /// Display title for the column. Typically set to <see cref="Header"/> during mapping.
    /// </summary>
    [JsonIgnore]
    public string? Title { get; set; }

    /// <summary>
    /// Column name. Typically set to <see cref="Header"/> during mapping.
    /// </summary>
    [JsonIgnore]
    public string? Name { get; set; }

    /// <summary>
    /// Whether this column should be hidden from the UI.
    /// Set during mapping; not part of the Ivanti JSON response.
    /// </summary>
    [JsonIgnore]
    public bool Hidden { get; set; }
}
