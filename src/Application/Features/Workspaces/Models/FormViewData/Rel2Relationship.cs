using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.FormViewData;

public class Rel2Relationship
{
    [JsonPropertyName("Binding")]
    public int Binding { get; set; }

    [JsonPropertyName("DesignerName")]
    public string? DesignerName { get; set; }

    [JsonPropertyName("HereCardinality")]
    public int HereCardinality { get; set; }

    [JsonPropertyName("HereName")]
    public string? HereName { get; set; }

    [JsonPropertyName("HereRequired")]
    public bool HereRequired { get; set; }

    [JsonPropertyName("IncludeObjectsFromFields")]
    public object? IncludeObjectsFromFields { get; set; }

    [JsonPropertyName("JoinCondition")]
    public object? JoinCondition { get; set; }

    [JsonPropertyName("LinkTable")]
    public LinkTableInfo? LinkTable { get; set; }

    [JsonPropertyName("MasterHere")]
    public bool MasterHere { get; set; }

    [JsonPropertyName("R148RelTag")]
    public string? R148RelTag { get; set; }

    [JsonPropertyName("System")]
    public bool System { get; set; }

    [JsonPropertyName("TableRefThere")]
    public string? TableRefThere { get; set; }

    [JsonPropertyName("ThereCardinality")]
    public int ThereCardinality { get; set; }

    [JsonPropertyName("ThereName")]
    public string? ThereName { get; set; }

    [JsonPropertyName("ThereRequired")]
    public bool ThereRequired { get; set; }

    [JsonPropertyName("UpdateFields")]
    public object? UpdateFields { get; set; }

    [JsonPropertyName("WhereCondition")]
    public object? WhereCondition { get; set; }
}
