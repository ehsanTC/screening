using System.Text.Json;
using System.Text.Json.Serialization;

namespace Screening.Domain.Answers;

[JsonConverter(typeof(AnswerJsonConverter))]
public abstract record Answer
{
    public abstract string TypeDiscriminator { get; }
    public abstract void WriteProperties(Utf8JsonWriter writer, JsonSerializerOptions options);

}
