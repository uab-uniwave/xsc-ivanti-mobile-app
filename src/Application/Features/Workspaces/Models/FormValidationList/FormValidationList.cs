using Application.Features.Workspaces.Models.FormDefaultData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.FormValidationList
{
    public class FormValidationList
    {
        [JsonPropertyName("NamedValidators")]
        public string? NamedValidators { get; set; }

        [JsonPropertyName("ValidatorsOverride")]
        public string? ValidatorsOverride { get; init; }

        [JsonPropertyName("MasterFormValues")]
        public FormDataModel? MasterFormValues { get; set; }

        [JsonPropertyName("objectId")]
        public string? ObjectId { get; set; }
    }

}
