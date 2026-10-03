using System.Text.Json;
using Screening.Domain.Parser.Models;

namespace Screening.Domain.Answers;

public record SingleOptionAnswer(YamlListItem Item) : Answer
{
    public override string TypeDiscriminator => nameof(SingleOptionAnswer);
    public override void WriteProperties(Utf8JsonWriter writer, JsonSerializerOptions options)
    {
        writer.WriteString(nameof(YamlListItem.Text), Item.Text);
        writer.WriteString(nameof(YamlListItem.Value), Item.Value);
    }
}
