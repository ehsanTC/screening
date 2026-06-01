using Screening.Domain.Parser.Models;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Screening.Domain.Services;

public class YamlQuestionnaireParser : IQuestionnaireParser
{
    private readonly IDeserializer _deserializer = new DeserializerBuilder()
        .WithNamingConvention(CamelCaseNamingConvention.Instance)
        .WithTypeConverter(new UnitYamlTypeConverter())
        .IgnoreUnmatchedProperties()
        .Build();
    
    public YamlQuestionnaire Parse(string content) => 
        _deserializer.Deserialize<YamlQuestionnaire>(content);
}
