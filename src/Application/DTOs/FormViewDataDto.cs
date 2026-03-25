using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Models.FormViewData;

namespace Application.Dtos;

public class FormViewDataDto
{
    [JsonPropertyName("__type")]
    public string? Type { get; set; }

    [JsonPropertyName("viewName")]
    public string? ViewName { get; set; }

    [JsonPropertyName("formDef")]
    public FormDefinitionDto? FormDef { get; set; }
}

public class FormDefinitionDto
{
    [JsonPropertyName("__type")]
    public string? Type { get; set; }

    [JsonPropertyName("TableMeta")]
    public TableMetaDto? TableMeta { get; set; }

    [JsonPropertyName("FormMeta")]
    public FormMetaDto? FormMeta { get; set; }

    [JsonPropertyName("RuleMeta")]
    public RuleMetaDto? RuleMeta { get; set; }
    [JsonPropertyName("BusObjectReadOnlyRules")]
    public List<string>? BusObjectReadOnlyRules { get; set; }

    [JsonPropertyName("BusObjectRequiredRules")]
    public List<string>? BusObjectRequiredRules { get; set; }

    [JsonPropertyName("LinkIdMap")]
    public Dictionary<string, string>? LinkIdMap { get; set; }

    [JsonPropertyName("LinkValidationFields")]
    public Dictionary<string, object>? LinkValidationFields { get; set; }

    [JsonPropertyName("FieldValidationTableRights")]
    public Dictionary<string, int>? FieldValidationTableRights { get; set; }

    [JsonPropertyName("FieldsNotEditable")]
    public Dictionary<string, object>? FieldsNotEditable { get; set; }

    [JsonPropertyName("FormAllowView")]
    public bool FormAllowView { get; set; }

    [JsonPropertyName("FormAllowInsert")]
    public bool FormAllowInsert { get; set; }

    [JsonPropertyName("FormAllowUpdate")]
    public bool FormAllowUpdate { get; set; }

    [JsonPropertyName("FormAllowEditInFinalState")]
    public bool FormAllowEditInFinalState { get; set; }

    [JsonPropertyName("ReferencedFields")]
    public Dictionary<string, FieldMetaDto>? ReferencedFields { get; set; }

    [JsonPropertyName("LastModified")]
    public string? LastModified { get; set; }

    [JsonPropertyName("LocalizedFields")]
    public object? LocalizedFields { get; set; }

    [JsonPropertyName("FormCellExpressions")]
    public Dictionary<string, object>? FormCellExpressions { get; set; }


    [JsonExtensionData]
    public Dictionary<string, JsonElement>? AdditionalData { get; set; }
}
public class ToolbarDefDto
{
    [JsonPropertyName("Items")]
    public object? Items { get; set; }

    [JsonPropertyName("Expressions")]
    public List<object>? Expressions { get; set; }

    [JsonPropertyName("JsonItems")]
    public List<List<object?>>? JsonItems { get; set; }
}

public class TableMetaDto
{
    [JsonPropertyName("TableRef")]
    public string? TableRef { get; set; }

    [JsonPropertyName("DisplayName")]
    public string? DisplayName { get; set; }

    [JsonPropertyName("Fields")]
    public Dictionary<string, FieldMetaDto> Fields { get; set; } = new();

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? AdditionalData { get; set; }
}
    
public class FieldMetaDto
{
    [JsonPropertyName("Annotations")]
    public Dictionary<string, object>? Annotations { get; set; }

    [JsonPropertyName("CommonlySearched")]
    public bool CommonlySearched { get; set; }

    [JsonPropertyName("ComputeHash")]
    public bool ComputeHash { get; set; }

    [JsonPropertyName("DataType")]
    public int DataType { get; set; }

    [JsonPropertyName("Description")]
    public string? Description { get; set; }

    [JsonPropertyName("DesignerName")]
    public string? DesignerName { get; set; }

    [JsonPropertyName("DisplayName")]
    public string? DisplayName { get; set; }

    [JsonPropertyName("FieldName")]
    public string? FieldName { get; set; }

    [JsonPropertyName("FieldType")]
    public int FieldType { get; set; }

    [JsonPropertyName("Format")]
    public string? Format { get; set; }

    [JsonPropertyName("HideFromUI")]
    public bool HideFromUI { get; set; }

    [JsonPropertyName("AllowedTagAttributeSetName")]
    public string? AllowedTagAttributeSetName { get; set; }

    [JsonPropertyName("AllowedTagAttributeSetValue")]
    public string? AllowedTagAttributeSetValue { get; set; }

    [JsonPropertyName("IsCommonlyUsed")]
    public bool IsCommonlyUsed { get; set; }

    [JsonPropertyName("IsFrsOwned")]
    public bool IsFrsOwned { get; set; }

    [JsonPropertyName("IsInternal")]
    public bool IsInternal { get; set; }

    [JsonPropertyName("IsLocalizable")]
    public bool IsLocalizable { get; set; }

    [JsonPropertyName("Length")]
    public int Length { get; set; }

    [JsonPropertyName("Link")]
    public FieldLinkDto? Link { get; set; }

    [JsonPropertyName("Nullable")]
    public bool Nullable { get; set; }

    [JsonPropertyName("Permissions")]
    public int? Permissions { get; set; }

    [JsonPropertyName("Scale")]
    public int Scale { get; set; }

    [JsonPropertyName("SourceFieldName")]
    public string? SourceFieldName { get; set; }

    [JsonPropertyName("SourceTableRef")]
    public string? SourceTableRef { get; set; }

    [JsonPropertyName("Stored")]
    public bool Stored { get; set; }

    [JsonPropertyName("System")]
    public bool System { get; set; }

    [JsonPropertyName("Unique")]
    public bool Unique { get; set; }

    [JsonPropertyName("FieldSynonyms")]
    public List<string>? FieldSynonyms { get; set; }

    [JsonPropertyName("IsSemanticField")]
    public bool IsSemanticField { get; set; }

    [JsonPropertyName("SemanticFieldType")]
    public int SemanticFieldType { get; set; }

    [JsonPropertyName("IsContractField")]
    public bool IsContractField { get; set; }

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? AdditionalData { get; set; }
}

public class FieldLinkDto
{
    [JsonPropertyName("LinkIdFieldRef")]
    public string? LinkIdFieldRef { get; set; }

    [JsonPropertyName("LinkCategoryFieldRef")]
    public string? LinkCategoryFieldRef { get; set; }

    [JsonPropertyName("LinkTableRef")]
    public string? LinkTableRef { get; set; }

    [JsonPropertyName("TargetType")]
    public int TargetType { get; set; }
}





public class FormMetaDto
{
    [JsonPropertyName("Cells")]
    public List<FormCellDto> Cells { get; set; } = new();

    [JsonPropertyName("Controls")]
    public Dictionary<string, FormControlDto> Controls { get; set; } = new();

    [JsonPropertyName("DisplayName")]
    public string? DisplayName { get; set; }

    [JsonPropertyName("Name")]
    public string? Name { get; set; }

    [JsonPropertyName("TableRef")]
    public string? TableRef { get; set; }
}

public class FormCellDto
{
    public bool SectionBreak { get; set; }

    public int Row { get; set; }

    public int Column { get; set; }

    public int RowSpan { get; set; }

    public int ColSpan { get; set; }

    public List<string> ControlNames { get; set; } = new();

    public int ControlPlacement { get; set; }

    public string? CellStyle { get; set; }

    public string? CellStyleExpression { get; set; }

    public string? CellVAlign { get; set; }

    public object? Margins { get; set; }
}

public class FormControlDto
{
    [JsonPropertyName("Name")]
    public string? Name { get; set; }

    [JsonPropertyName("FieldRef")]
    public string? FieldRef { get; set; }

    [JsonPropertyName("LinkFieldRef")]
    public string? LinkFieldRef { get; set; }

    [JsonPropertyName("Label")]
    public string? Label { get; set; }

    [JsonPropertyName("Category")]
    public int? Category { get; set; }

    [JsonPropertyName("Editable")]
    public bool? Editable { get; set; }

    [JsonPropertyName("Visible")]
    public bool? Visible { get; set; }

    [JsonPropertyName("Searchable")]
    public bool? Searchable { get; set; }

    [JsonPropertyName("Scannable")]
    public bool? Scannable { get; set; }

    [JsonPropertyName("Height")]
    public int? Height { get; set; }

    [JsonPropertyName("Width")]
    public int? Width { get; set; }

    [JsonPropertyName("TabIndex")]
    public int? TabIndex { get; set; }

    [JsonPropertyName("Style")]
    public string? Style { get; set; }

    [JsonPropertyName("StyleExpression")]
    public string? StyleExpression { get; set; }

    [JsonPropertyName("LabelStyleExpression")]
    public string? LabelStyleExpression { get; set; }

    [JsonPropertyName("VisibleExpression")]
    public string? VisibleExpression { get; set; }

    [JsonPropertyName("DisabledExpression")]
    public string? DisabledExpression { get; set; }

    // 👉 catch ALL unknown fields safely
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? AdditionalData { get; set; }
}

public class RuleMetaDto
{
    [JsonPropertyName("TableRef")]
    public string? TableRef { get; set; }

    [JsonPropertyName("DefaultValues")]
    public List<RuleValueDto> DefaultValues { get; set; } = new();

    [JsonPropertyName("BeforeSaveValues")]
    public List<RuleValueDto> BeforeSaveValues { get; set; } = new();

    [JsonPropertyName("AutoFill")]
    public Dictionary<string, Dictionary<string, AutoFillRuleDto>> AutoFill { get; set; } = new();

    [JsonPropertyName("ReadOnlyExpressions")]
    public Dictionary<string, ExpressionRuleDto> ReadOnlyExpressions { get; set; } = new();

    [JsonPropertyName("RequiredExpressions")]
    public Dictionary<string, ExpressionRuleDto> RequiredExpressions { get; set; } = new();

    [JsonPropertyName("FinalStateRule")]
    public FinalStateRuleDto? FinalStateRule { get; set; }

    // fallback safety
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? AdditionalData { get; set; }
}
public class RuleValueDto
{
    public string? Name { get; set; }

    public string? FieldName { get; set; }

    public bool Disable { get; set; }

    public ExpressionDto? Expression { get; set; }

    public string? Description { get; set; }
}

public class ExpressionTreeDto
{
    public string? FunctionName { get; set; }

    public bool? IsMethod { get; set; }

    public int? Kind { get; set; }

    public object? Value { get; set; }

    public List<ExpressionTreeDto>? Arguments { get; set; }

    public ExpressionTreeDto? Left { get; set; }

    public ExpressionTreeDto? Right { get; set; }

    public ExpressionTreeDto? WhenTrue { get; set; }

    public ExpressionTreeDto? WhenFalse { get; set; }

    public ExpressionTreeDto? Condition { get; set; }

    public int? Op { get; set; }

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? AdditionalData { get; set; }
}
public class AutoFillRuleDto
{
    public bool OnlyEmpty { get; set; }

    public bool Cascade { get; set; }

    public bool Disable { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public ExpressionDto? AutoFillExpression { get; set; }
}
public class ExpressionRuleDto
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public List<string> FieldRefs { get; set; } = new();

    public bool IsFullExpression { get; set; }

    public ExpressionTreeDto? Tree { get; set; }

    public string? Source { get; set; }

    public int ValidationStatus { get; set; }
}


public class FinalStateRuleDto
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public bool Disable { get; set; }

    public ExpressionDto? Expression { get; set; }

    public List<string> Exceptions { get; set; } = new();
}




public class ExpressionDto
{
    public string? Name { get; set; }

    public string? Source { get; set; }

    public int ValidationStatus { get; set; }

    public bool IsFullExpression { get; set; }

    public List<string> FieldRefs { get; set; } = new();

    public ExpressionTreeDto? Tree { get; set; }

    public string? Description { get; set; }
}

