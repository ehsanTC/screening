using System.Text.Json;
using Screening.Domain.Models;

namespace Screening.Domain.Answers;

public record RepeatableKeyValueAnswer(List<KeyValue> LabResults, List<string> Disorders)
    : Answer
{
    public override string TypeDiscriminator => nameof(RepeatableKeyValueAnswer);

    public override void WriteProperties(Utf8JsonWriter writer, JsonSerializerOptions options)
    {
        writer.WritePropertyName(nameof(LabResults));
        JsonSerializer.Serialize(writer, LabResults, options);

        writer.WritePropertyName(nameof(Disorders));
        JsonSerializer.Serialize(writer, Disorders, options);
    }
}
