using System.Text.Json;

namespace Screening.Domain.Answers;

public record SingleOptionAnswer(string SelectedOption) : Answer
{
    public override string TypeDiscriminator => nameof(SingleOptionAnswer);
    public override void WriteProperties(Utf8JsonWriter writer, JsonSerializerOptions options)
    {
        writer.WriteString(nameof(SelectedOption), SelectedOption);
    }
}
