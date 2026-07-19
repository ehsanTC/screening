using System.Text.Json;

namespace Screening.Domain.Answers;

public record MultiOptionAnswer(List<string> SelectedOptions) : Answer
{
    public override string TypeDiscriminator => nameof(MultiOptionAnswer);
    public override void WriteProperties(Utf8JsonWriter writer, JsonSerializerOptions options)
    {
        writer.WritePropertyName(nameof(SelectedOptions));
        JsonSerializer.Serialize(writer, SelectedOptions, options);
    }
}
