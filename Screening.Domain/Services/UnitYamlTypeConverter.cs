using Screening.Domain.Parser.Models;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace Screening.Domain.Services;

public class UnitYamlTypeConverter : IYamlTypeConverter
{
    public bool Accepts(Type type) => type == typeof(Unit);

    public object ReadYaml(IParser parser, Type type, ObjectDeserializer rootDeserializer)
    {
        var scalar = (Scalar) parser.Current;
        var value = scalar.Value;
        parser.MoveNext();

        return value.ToLowerInvariant() switch
        {
            "m" or "meter" => Unit.Meter,
            "cm" or "centimeter" => Unit.Centimeter,
            "kg" or "kilograms" or "kilogram" => Unit.KiloGrams,
            
            _ => throw new InvalidOperationException($"Unknown unit: {value}")
        };
    }

    public void WriteYaml(IEmitter emitter, object? value, Type type, ObjectSerializer serializer)
    {
        var text = value switch
        {
            Unit.Centimeter => "Centimeter",
            Unit.Meter => "Meter",
            _ => throw new InvalidOperationException($"Unknown unit: {value}")
        };

        emitter.Emit(new Scalar(text));
    }
}

