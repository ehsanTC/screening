using Screening.Domain.Parser.Models;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace Screening.Domain.Services;

public class YamlQuestionTypeConverter : IYamlTypeConverter
{
    private static readonly Dictionary<string, YamlQuestionType> Map = new(StringComparer.OrdinalIgnoreCase)
    {
        ["string"] = YamlQuestionType.String,
        ["number"] = YamlQuestionType.Number,
        ["single_choice"] = YamlQuestionType.SingleOption,
        ["multi_choice"] = YamlQuestionType.MultiOption,
        ["repeatable_key_value"] = YamlQuestionType.RepeatableKeyValue
    };

    public bool Accepts(Type type) => type == typeof(YamlQuestionType);

    public object? ReadYaml(IParser parser, Type type, ObjectDeserializer rootDeserializer)
    {
        var value = ((Scalar)parser.Current!).Value;
        parser.MoveNext();

        return Map.TryGetValue(value, out var result)
            ? result
            : YamlQuestionType.Unknown;
    }

    public void WriteYaml(IEmitter emitter, object? value, Type type, ObjectSerializer serializer)
    {
        var reverse = Map.First(kv => kv.Value.Equals(value)).Key;
        emitter.Emit(new Scalar(reverse));
    }
}
