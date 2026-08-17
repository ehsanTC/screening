using System.Text.Json;
using Screening.Domain.Parser.Models;

namespace Screening.Domain.Answers;

public record MultiOptionAnswer(List<YamlListItem> SelectedOptions) : Answer
{
    public override string TypeDiscriminator => nameof(MultiOptionAnswer);
    public override void WriteProperties(Utf8JsonWriter writer, JsonSerializerOptions options)
    {
        writer.WriteStartArray(nameof(SelectedOptions));
        
        SelectedOptions.ForEach(option =>
        {
            writer.WriteString(nameof(YamlListItem.Text),  option.Text);
            writer.WriteString(nameof(YamlListItem.Value), option.Value);
        });
        
        writer.WriteEndArray();
    }
}
