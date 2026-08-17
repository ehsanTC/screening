using System.Text.Json;
using System.Text.Json.Serialization;
using Screening.Domain.Parser.Models;

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

        if (!root.TryGetProperty(Discriminator, out var typeProp))
            throw new JsonException($"Missing '{Discriminator}' property.");

        var type = typeProp.GetString();
        if (string.IsNullOrWhiteSpace(type))
            throw new JsonException($"'{Discriminator}' is null or empty.");

        return type switch
        {
            nameof(TextAnswer) => new TextAnswer(
                root.GetProperty(nameof(TextAnswer.Text)).GetString() ?? string.Empty),

            nameof(SingleOptionAnswer) => new SingleOptionAnswer(
                new YamlListItem()
                {
                    Text = root.GetProperty(nameof(YamlListItem.Text)).GetString() ?? string.Empty, 
                    Value = root.GetProperty(nameof(YamlListItem.Value)).GetString() ?? string.Empty 
                }),

            nameof(MultiOptionAnswer) => new MultiOptionAnswer(
                root.GetProperty(nameof(MultiOptionAnswer.SelectedOptions))
                    .EnumerateArray()
                    .Select(el => new YamlListItem
                    {
                        Text = el.GetProperty(nameof(YamlListItem.Text)).GetString() ?? string.Empty,
                        Value = el.GetProperty(nameof(YamlListItem.Value)).GetString() ?? string.Empty
                    })
                    .ToList()),

            _ => throw new JsonException($"Unknown answer type discriminator: '{type}'")
        };
    }
}
