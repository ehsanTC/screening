using System.Text.Json;
using System.Text.Json.Serialization;

namespace Screening.Domain.Answers;

public class AnswerJsonConverter : JsonConverter<Answer>
{
    private const string Discriminator = "type";
    
    public override void Write(Utf8JsonWriter writer, Answer value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString(Discriminator, value.TypeDiscriminator);
        value.WriteProperties(writer, options);
        writer.WriteEndObject();
    }

    public override Answer Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;
        var type = root.GetProperty(Discriminator).GetString();
        var rawText = root.GetRawText();

        return type switch
        {
            nameof(TextAnswer) => JsonSerializer.Deserialize<TextAnswer>(rawText, options)!,
            nameof(SingleOptionAnswer) => JsonSerializer.Deserialize<SingleOptionAnswer>(rawText, options)!,
            nameof(MultiOptionAnswer) => JsonSerializer.Deserialize<MultiOptionAnswer>(rawText, options)!,
            
            _ => throw new JsonException($"Unknown answer type discriminator: '{type}'")
        };
    }
}
