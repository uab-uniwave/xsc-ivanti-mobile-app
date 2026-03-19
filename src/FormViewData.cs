
public class FormViewDataResponse
{
    [JsonPropertyName("d")]
    public FormViewData? Data { get; set; }
}

public class FormViewData
{
    [JsonPropertyName("__type")]
    public string? Type { get; set; }

    [JsonPropertyName("viewName")]
    public string? ViewName { get; set; }

    [JsonPropertyName("formDef")]
    public FormDefinition? FormDef { get; set; }

    [JsonPropertyName("layoutName")]
    public string? LayoutName { get; set; }

    [JsonPropertyName("detailsState")]
    public int DetailsState { get; set; }

    [JsonPropertyName("tabDefs")]
    public List<JsonElement> TabDefs { get; set; } = new();

    [JsonPropertyName("tabData")]
    public List<JsonElement> TabData { get; set; } = new();

    [JsonPropertyName("toolbarDef")]
    public ToolbarDefinition? ToolbarDef { get; set; }

    [JsonPropertyName("ObjectMatching")]
    public JsonElement? ObjectMatching { get; set; }

    [JsonPropertyName("savedFormViewName")]
    public string? SavedFormViewName { get; set; }

    [JsonPropertyName("enableCost")]
    public bool EnableCost { get; set; }

    [JsonPropertyName("costDef")]
    public JsonElement? CostDef { get; set; }

    [JsonPropertyName("enableSmartFill")]
    public bool EnableSmartFill { get; set; }

    [JsonPropertyName("auditObjectViews")]
    public bool AuditObjectViews { get; set; }

    [JsonPropertyName("costsummaryDef")]
    public string? CostSummaryDef { get; set; }

    [JsonPropertyName("showCostSummary")]
    public bool ShowCostSummary { get; set; }

    [JsonPropertyName("layoutCssClass")]
    public string? LayoutCssClass { get; set; }

    [JsonPropertyName("TabControlCssClass")]
    public string? TabControlCssClass { get; set; }

    [JsonPropertyName("MainFormCssClass")]
    public string? MainFormCssClass { get; set; }

    [JsonPropertyName("enableTicketClassification")]
    public bool EnableTicketClassification { get; set; }

    [JsonPropertyName("ticketClassificationConfigDef")]
    public JsonElement? TicketClassificationConfigDef { get; set; }
}

public class FormDefinition
{
    [JsonPropertyName("TableMeta")]
    public FormTableMeta? TableMeta { get; set; }

    [JsonPropertyName("ReferencedFields")]
    public Dictionary<string, FieldMetaData> ReferencedFields { get; set; } = new();

    [JsonPropertyName("LastModified")]
    public string? LastModified { get; set; }

    [JsonPropertyName("LocalizedFields")]
    public JsonElement? LocalizedFields { get; set; }

    [JsonPropertyName("FormCellExpressions")]
    public Dictionary<string, JsonElement> FormCellExpressions { get; set; } = new();

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? AdditionalData { get; set; }
}

public class FormTableMeta
{
    [JsonPropertyName("TableRef")]
    public string? TableRef { get; set; }

    [JsonPropertyName("DisplayName")]
    public string? DisplayName { get; set; }

    [JsonPropertyName("Fields")]
    public Dictionary<string, FieldMetaData> Fields { get; set; } = new();
}

public class ToolbarDefinition
{
    [JsonPropertyName("Items")]
    public JsonElement? Items { get; set; }

    [JsonPropertyName("Expressions")]
    public List<JsonElement> Expressions { get; set; } = new();

    [JsonPropertyName("JsonItems")]
    public List<List<JsonElement>> JsonItems { get; set; } = new();
}
