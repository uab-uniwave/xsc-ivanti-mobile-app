using System.Collections.Generic;
using System.Text.Json.Serialization;
using Application.Models.FormViewData;

namespace Application.Models.FormDefaultData;

public class FormDefaultData
{
    public class FormDefaultDataContainer
    {
        [JsonPropertyName("ObjectId")]
        public string? ObjectId { get; set; }

        [JsonPropertyName("Def")]
        public FormDefinition? Def { get; set; }

        [JsonPropertyName("Data")]
        public FormDataModel? FormData { get; set; }

        [JsonPropertyName("FinalStateStatus")]
        public object? FinalStateStatus { get; set; }
    }


    public class ReferencedFieldDefinition
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
        public object? Link { get; set; }

        [JsonPropertyName("Nullable")]
        public bool Nullable { get; set; }

        [JsonPropertyName("Permissions")]
        public int Permissions { get; set; }

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

    public class FormDataModel
    {
        [JsonPropertyName("__type")]
        public string? Type { get; set; }

        [JsonPropertyName("Objects")]
        public Dictionary<string, DataObject> Objects { get; set; } = new();

        [JsonPropertyName("ObjectRelationships")]
        public Dictionary<string, Dictionary<string, string>> ObjectRelationships { get; set; } = new();
    }

    public class DataObject
    {
        [JsonPropertyName("ObjectType")]
        public int ObjectType { get; set; }

        [JsonPropertyName("TableRef")]
        public string? TableRef { get; set; }

        [JsonPropertyName("RecordId")]
        public string? RecordId { get; set; }

        [JsonPropertyName("Values")]
        public Dictionary<string, string?> Values { get; set; } = new();

        [JsonPropertyName("PureValues")]
        public Dictionary<string, string?> PureValues { get; set; } = new();
    }
}