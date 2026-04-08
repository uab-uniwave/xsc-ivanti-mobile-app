using System.Text.Json;
using System.Text.Json.Serialization;

namespace Application.Features.Workspaces.Models.FormViewData;

/// <summary>
/// Converts JSON values that may be either strings or objects to strings.
/// When an object is encountered (e.g., complex expressions from Ivanti API),
/// it is treated as null rather than causing deserialization to fail.
/// This handles cases where form controls like "Symptom" return complex objects
/// instead of string values for expression properties.
/// </summary>
public class FormViewStringOrNullConverter : JsonConverter<string?>
{
    public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.String:
                return reader.GetString();
            case JsonTokenType.Null:
                return null;
            case JsonTokenType.StartObject:
                // Skip the entire object to properly advance the reader past all tokens.
                // Ivanti API sometimes returns complex objects for expression properties instead of strings.
                reader.Skip();
                return null;
            case JsonTokenType.StartArray:
                // Skip the entire array to properly advance the reader past all tokens.
                reader.Skip();
                return null;
            default:
                return null;
        }
    }

    public override void Write(Utf8JsonWriter writer, string? value, JsonSerializerOptions options)
    {
        if (value == null)
        {
            writer.WriteNullValue();
        }
        else
        {
            writer.WriteStringValue(value);
        }
    }
}
