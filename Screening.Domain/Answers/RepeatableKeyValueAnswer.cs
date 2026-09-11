using System.Text.Json;

namespace Screening.Domain.Answers;

public record RepeatableKeyValueAnswer(List<KeyValueEntry> LabResults, List<string> Disorders)
    : Answer
{
    public override string TypeDiscriminator => nameof(RepeatableKeyValueAnswer);

    public override void WriteProperties(Utf8JsonWriter writer, JsonSerializerOptions options)
    {
        writer.WriteStartObject(nameof(RepeatableKeyValueAnswer));
        writer.WritePropertyName(nameof(LabResults));
        JsonSerializer.Serialize(writer, LabResults, options);
        
        writer.WritePropertyName(nameof(Disorders));
        JsonSerializer.Serialize(writer, Disorders, options);
        writer.WriteEndObject();
    }
}

public record KeyValueEntry(string Name, string Value);
