using System.Text.Json;
using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.FormViewData;

public class FormViewData
{
    [JsonPropertyName("__type")]
    public string? Type { get; set; }

    [JsonPropertyName("viewName")]
    public string? ViewName { get; set; }

    [JsonPropertyName("formDef")]
    public FormDefinition? FormDef { get; set; }
}

public class FormDefinition
{
    [JsonPropertyName("__type")]
    public string? Type { get; set; }

    [JsonPropertyName("TableMeta")]
    public TableMeta? TableMeta { get; set; }

    [JsonPropertyName("FormMeta")]
    public FormMeta? FormMeta { get; set; }

    [JsonPropertyName("RuleMeta")]
    public RuleMeta? RuleMeta { get; set; }

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
    public Dictionary<string, FieldMeta>? ReferencedFields { get; set; }

    [JsonPropertyName("LastModified")]
    public string? LastModified { get; set; }

    [JsonPropertyName("LocalizedFields")]
    public object? LocalizedFields { get; set; }

    [JsonPropertyName("FormCellExpressions")]
    public Dictionary<string, object>? FormCellExpressions { get; set; }

    
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? AdditionalData { get; set; }
}
public class ToolbarDef
{
    [JsonPropertyName("Items")]
    public object? Items { get; set; }

    [JsonPropertyName("Expressions")]
    public List<object>? Expressions { get; set; }

    [JsonPropertyName("JsonItems")]
    public List<List<object?>>? JsonItems { get; set; }
}

public class TableMeta
{
    [JsonPropertyName("TableRef")]
    public string? TableRef { get; set; }

    [JsonPropertyName("DisplayName")]
    public string? DisplayName { get; set; }

    [JsonPropertyName("Fields")]
    public Dictionary<string, FieldMeta> Fields { get; set; } = new();

    [JsonPropertyName("IsNeuronsWorkflowExecutedFieldRef")]
    public string? IsNeuronsWorkflowExecutedFieldRef { get; set; }

    [JsonPropertyName("IsRecordLockingEnabled")]
    public bool IsRecordLockingEnabled { get; set; }

    [JsonPropertyName("DisplayFieldRef")]
    public string? DisplayFieldRef { get; set; }

    [JsonPropertyName("TypeSelectorFieldRef")]
    public string? TypeSelectorFieldRef { get; set; }

    [JsonPropertyName("IsInChangeControl")]
    public bool IsInChangeControl { get; set; }

    [JsonPropertyName("CalculatedRules")]
    public Dictionary<string, CalculatedRule>? CalculatedRules { get; set; }

    [JsonPropertyName("FieldsEncryption")]
    public Dictionary<string, object>? FieldsEncryption { get; set; }

    [JsonPropertyName("ValidatedFields")]
    public Dictionary<string, ValidatedField>? ValidatedFields { get; set; }

    [JsonPropertyName("Relationships")]
    public Dictionary<string, Relationship>? Relationships { get; set; }

    [JsonPropertyName("Rel2")]
    public Dictionary<string, Rel2Relationship>? Rel2 { get; set; }

    [JsonPropertyName("PushToRelationships")]
    public object? PushToRelationships { get; set; }

    [JsonPropertyName("DerivedTypesRefs")]
    public List<string> DerivedTypesRefs { get; set; } = new();

    [JsonPropertyName("CompositeContracts")]
    public List<object> CompositeContracts { get; set; } = new();

    [JsonPropertyName("SearchPreviewFields")]
    public List<SearchPreviewField>? SearchPreviewFields { get; set; }

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? AdditionalData { get; set; }
}

public class CalculatedRule
{
    [JsonPropertyName("Calculated")]
    public int Calculated { get; set; }

    [JsonPropertyName("CalculatedExpression")]
    public Expression? CalculatedExpression { get; set; }

    [JsonPropertyName("Description")]
    public string? Description { get; set; }

    [JsonPropertyName("Name")]
    public string? Name { get; set; }

    [JsonPropertyName("RecalculateOnLoad")]
    public bool RecalculateOnLoad { get; set; }
}

public class ValidatedField
{
    [JsonPropertyName("Condition")]
    public Expression? Condition { get; set; }

    [JsonPropertyName("IsStateTransition")]
    public bool IsStateTransition { get; set; }

    [JsonPropertyName("ValidatedIdFieldRef")]
    public string? ValidatedIdFieldRef { get; set; }

    [JsonPropertyName("ValidatorRef")]
    public string? ValidatorRef { get; set; }
}

public class Relationship
{
    [JsonPropertyName("Binding")]
    public int Binding { get; set; }

    [JsonPropertyName("CompositeTypeFilter")]
    public List<string>? CompositeTypeFilter { get; set; }

    [JsonPropertyName("Condition")]
    public Expression? Condition { get; set; }

    [JsonPropertyName("DesignerName")]
    public string? DesignerName { get; set; }

    [JsonPropertyName("Direction")]
    public int Direction { get; set; }

    [JsonPropertyName("DisplayName")]
    public string? DisplayName { get; set; }

    [JsonPropertyName("ForeignKey")]
    public ForeignKeyInfo? ForeignKey { get; set; }

    [JsonPropertyName("FullTextSearchIndex")]
    public bool FullTextSearchIndex { get; set; }

    [JsonPropertyName("HereCardinality")]
    public int HereCardinality { get; set; }

    [JsonPropertyName("HerePreventDelete")]
    public bool HerePreventDelete { get; set; }

    [JsonPropertyName("HereRequired")]
    public bool HereRequired { get; set; }

    [JsonPropertyName("Implementation")]
    public int Implementation { get; set; }

    [JsonPropertyName("IncludeObjectsFromFields")]
    public List<IncludeObjectsFromField>? IncludeObjectsFromFields { get; set; }

    [JsonPropertyName("IsASymmetric")]
    public bool IsASymmetric { get; set; }

    [JsonPropertyName("IsCommonlyUsed")]
    public bool IsCommonlyUsed { get; set; }

    [JsonPropertyName("IsDeprecated")]
    public bool IsDeprecated { get; set; }

    [JsonPropertyName("LinkTableFieldMapping")]
    public object? LinkTableFieldMapping { get; set; }

    [JsonPropertyName("LinkTableName")]
    public string? LinkTableName { get; set; }

    [JsonPropertyName("OwnerTargetSubtypeFieldRef")]
    public string? OwnerTargetSubtypeFieldRef { get; set; }

    [JsonPropertyName("Permissions")]
    public int? Permissions { get; set; }

    [JsonPropertyName("Tag")]
    public string? Tag { get; set; }

    [JsonPropertyName("TargetDefaultSubtype")]
    public string? TargetDefaultSubtype { get; set; }

    [JsonPropertyName("ThereCardinality")]
    public int ThereCardinality { get; set; }

    [JsonPropertyName("ThereRequired")]
    public bool ThereRequired { get; set; }

    [JsonPropertyName("UpdateFields")]
    public List<UpdateField>? UpdateFields { get; set; }

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? AdditionalData { get; set; }
}

public class ForeignKeyInfo
{
    [JsonPropertyName("PKField")]
    public string? PKField { get; set; }

    [JsonPropertyName("FKField")]
    public string? FKField { get; set; }

    [JsonPropertyName("FKCategoryField")]
    public string? FKCategoryField { get; set; }
}

public class IncludeObjectsFromField
{
    [JsonPropertyName("Field")]
    public string? Field { get; set; }
}

public class UpdateField
{
    [JsonPropertyName("LeftField")]
    public string? LeftField { get; set; }

    [JsonPropertyName("RightField")]
    public string? RightField { get; set; }

    [JsonPropertyName("RightValue")]
    public string? RightValue { get; set; }

    [JsonPropertyName("PushValueOnlyWhenTargetEmpty")]
    public bool PushValueOnlyWhenTargetEmpty { get; set; }

    [JsonPropertyName("PreserveUserEdits")]
    public bool PreserveUserEdits { get; set; }
}

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

public class LinkTableInfo
{
    [JsonPropertyName("LinkTableName")]
    public string? LinkTableName { get; set; }

    [JsonPropertyName("LinkDataTableRef")]
    public string? LinkDataTableRef { get; set; }

    [JsonPropertyName("LinkDataUpNameField")]
    public string? LinkDataUpNameField { get; set; }

    [JsonPropertyName("LinkDataDownNameField")]
    public string? LinkDataDownNameField { get; set; }

    [JsonPropertyName("LinkConstraintsTableRef")]
    public string? LinkConstraintsTableRef { get; set; }
}

public class SearchPreviewField
{
    [JsonPropertyName("FieldRef")]
    public string? FieldRef { get; set; }

    [JsonPropertyName("DisplayName")]
    public string? DisplayName { get; set; }
}

public class FieldMeta
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
    public FieldLink? Link { get; set; }

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

public class FieldLink
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

public class FormMeta
{
    [JsonPropertyName("Cells")]
    public List<FormCell> Cells { get; set; } = new();

    [JsonPropertyName("Controls")]
    public Dictionary<string, FormControl> Controls { get; set; } = new();

    [JsonPropertyName("DisplayName")]
    public string? DisplayName { get; set; }

    [JsonPropertyName("Name")]
    public string? Name { get; set; }

    [JsonPropertyName("TableRef")]
    public string? TableRef { get; set; }
}

public class FormCell
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

public class FormControl
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
public class RuleMeta
{
    [JsonPropertyName("TableRef")]
    public string? TableRef { get; set; }

    [JsonPropertyName("DefaultValues")]
    public List<RuleValue> DefaultValues { get; set; } = new();

    [JsonPropertyName("BeforeSaveValues")]
    public List<RuleValue> BeforeSaveValues { get; set; } = new();

    [JsonPropertyName("AutoFill")]
    public Dictionary<string, Dictionary<string, AutoFillRule>> AutoFill { get; set; } = new();

    [JsonPropertyName("ReadOnlyExpressions")]
    public Dictionary<string, ExpressionRule> ReadOnlyExpressions { get; set; } = new();

    [JsonPropertyName("RequiredExpressions")]
    public Dictionary<string, ExpressionRule> RequiredExpressions { get; set; } = new();

    [JsonPropertyName("FinalStateRule")]
    public FinalStateRule? FinalStateRule { get; set; }

    // fallback safety
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? AdditionalData { get; set; }
}
public class RuleValue
{
    public string? Name { get; set; }

    public string? FieldName { get; set; }

    public bool Disable { get; set; }

    public Expression? Expression { get; set; }

    public string? Description { get; set; }
}
public class Expression
{
    public string? Name { get; set; }

    public string? Source { get; set; }

    public int ValidationStatus { get; set; }

    public bool IsFullExpression { get; set; }

    public List<string> FieldRefs { get; set; } = new();

    public ExpressionTree? Tree { get; set; }

    public string? Description { get; set; }
}

public class ExpressionTree
{
    public string? FunctionName { get; set; }

    public bool? IsMethod { get; set; }

    public int? Kind { get; set; }

    public object? Value { get; set; }

    public List<ExpressionTree>? Arguments { get; set; }

    public ExpressionTree? Left { get; set; }

    public ExpressionTree? Right { get; set; }

    public ExpressionTree? WhenTrue { get; set; }

    public ExpressionTree? WhenFalse { get; set; }

    public ExpressionTree? Condition { get; set; }

    public int? Op { get; set; }

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? AdditionalData { get; set; }
}
public class AutoFillRule
{
    public bool OnlyEmpty { get; set; }

    public bool Cascade { get; set; }

    public bool Disable { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public Expression? AutoFillExpression { get; set; }
}

public class FinalStateRule
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public bool Disable { get; set; }

    public Expression? Expression { get; set; }

    public List<string> Exceptions { get; set; } = new();
}

public class ExpressionRule
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public List<string> FieldRefs { get; set; } = new();

    public bool IsFullExpression { get; set; }

    public ExpressionTree? Tree { get; set; }

    public string? Source { get; set; }

    public int ValidationStatus { get; set; }
}
