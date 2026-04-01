using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Application.Models.WorkspaceData;

public class WorkspaceData
{
    [JsonPropertyName("__type")]
    public string? Type { get; set; }

    [JsonPropertyName("ObjectId")]
    public string? ObjectId { get; set; }

    [JsonPropertyName("ObjectDisplay")]
    public string? ObjectDisplay { get; set; }

    [JsonPropertyName("AllowDesign")]
    public bool AllowDesign { get; set; }

    [JsonPropertyName("SearchData")]
    public WorkspaceSearchData? SearchData { get; set; }


    [JsonPropertyName("LayoutData")]
    public WorkspaceLayoutData? LayoutData { get; set; }





    public class WorkspaceSearchData
    {
        [JsonPropertyName("previewGridName")]
        public string? PreviewGridName { get; set; }

        [JsonPropertyName("relatedObjects")]
        public List<List<string?>> RelatedObjects { get; set; } = new();

        [JsonPropertyName("favorites")]
        public List<WorkspaceFavorite> Favorites { get; set; } = new();

        [JsonPropertyName("allowFullTextSearch")]
        public bool AllowFullTextSearch { get; set; }

        [JsonPropertyName("canCreate")]
        public bool CanCreate { get; set; }

        [JsonPropertyName("fieldsTreeData")]
        public WorkspaceFieldsTreeData? FieldsTreeData { get; set; }
    }

    public class WorkspaceFavorite
    {
        [JsonPropertyName("Id")]
        public string? Id { get; set; }

        [JsonPropertyName("Name")]
        public string? Name { get; set; }

        [JsonPropertyName("isDefault")]
        public bool IsDefault { get; set; }

        [JsonPropertyName("CanEdit")]
        public bool CanEdit { get; set; }
    }

    public class WorkspaceFieldsTreeData
    {
        [JsonPropertyName("TableDef")]
        public WorkspaceTableDef? TableDef { get; set; }
    }

    public class WorkspaceTableDef
    {
        [JsonPropertyName("DesignerName")]
        public string? DesignerName { get; set; }

        [JsonPropertyName("DisplayName")]
        public string? DisplayName { get; set; }

        [JsonPropertyName("TableRef")]
        public string? TableRef { get; set; }

        [JsonPropertyName("ReferenceKey")]
        public string? ReferenceKey { get; set; }

        [JsonPropertyName("Fields")]
        public List<WorkspaceFieldItem> Fields { get; set; } = new();
    }

    public class WorkspaceFieldItem
    {
        [JsonPropertyName("MetaData")]
        public WorkspaceFieldMetaData? MetaData { get; set; }

        [JsonPropertyName("ReferenceKey")]
        public string? ReferenceKey { get; set; }

        [JsonPropertyName("DataType")]
        public string? DataType { get; set; }

        [JsonPropertyName("DataWidth")]
        public int DataWidth { get; set; }

        [JsonPropertyName("DropType")]
        public int DropType { get; set; }

        [JsonPropertyName("DisplayAs")]
        public string? DisplayAs { get; set; }
    }

    public class WorkspaceFieldMetaData
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
        public WorkspaceFieldLink? Link { get; set; }

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
        public object? FieldSynonyms { get; set; }

        [JsonPropertyName("IsSemanticField")]
        public bool IsSemanticField { get; set; }

        [JsonPropertyName("SemanticFieldType")]
        public int SemanticFieldType { get; set; }

        [JsonPropertyName("IsContractField")]
        public bool IsContractField { get; set; }
    }

    public class WorkspaceFieldLink
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

    public class WorkspaceLayoutData
    {
        [JsonPropertyName("newRecordViews")]
        public Dictionary<string, string>? NewRecordViews { get; set; }

        [JsonPropertyName("oneNewRecordView")]
        public string? OneNewRecordView { get; set; }

        [JsonPropertyName("editRecordViews")]
        public Dictionary<string, string>? EditRecordViews { get; set; }

        [JsonPropertyName("oneEditRecordView")]
        public string? OneEditRecordView { get; set; }

        [JsonPropertyName("modalForms")]
        public Dictionary<string, ModalFormDefinition>? ModalForms { get; set; }
    }

    public class ModalFormDefinition
    {
        [JsonPropertyName("CreationForm")]
        public string? CreationForm { get; set; }

        [JsonPropertyName("EditForm")]
        public string? EditForm { get; set; }
    }

}