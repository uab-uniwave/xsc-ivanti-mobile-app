using Application.Features.Workspaces.Models.GridDataHandler;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.DTOs;

/// <summary>
/// 
/// </summary>
public class GetGridDataHandlerResponse
{
    [JsonPropertyName("d")]
    public JsonElement D { get; set; }

    /// <summary>
    /// Gets or sets the metadata information associated with the grid.
    /// </summary>
    [JsonPropertyName("metaData")]
    public GridDataMetaData? MetaData { get; set; }

    /// <summary>
    /// Gets or sets the collection of data rows, where each row is represented as a dictionary mapping column names to
    /// their corresponding JSON values.
    /// </summary>
    /// <remarks>Each dictionary in the collection represents a single row, with keys as column names and
    /// values as JSON elements. The property may be null if no data is available.</remarks>
    [JsonPropertyName("rows")]
    public List<Dictionary<string, JsonElement>>? Rows { get; set; }
}