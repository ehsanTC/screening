using System.Text.Json;

namespace Screening.Domain.Answers;

public record TextAnswer(string Text) : Answer
{
    public override string TypeDiscriminator => nameof(TextAnswer);
    public override void WriteProperties(Utf8JsonWriter writer, JsonSerializerOptions options)
    {
        writer.WriteString(nameof(Text), Text);
    }
}
