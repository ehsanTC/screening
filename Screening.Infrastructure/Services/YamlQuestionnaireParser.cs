using Screening.Domain.Parser.Models;
using YamlDotNet.Serialization;

namespace Screening.Infrastructure.Services;

public class YamlQuestionnaireParser : IQuestionnaireParser
{
    private readonly IDeserializer _deserializer = new DeserializerBuilder()
        .WithNamingConvention(YamlDotNet.Serialization.NamingConventions.CamelCaseNamingConvention.Instance)
        .IgnoreUnmatchedProperties()
        .Build();
    
    public YamlQuestionnaire Parse(string content) => 
        _deserializer.Deserialize<YamlQuestionnaire>(content);
}
