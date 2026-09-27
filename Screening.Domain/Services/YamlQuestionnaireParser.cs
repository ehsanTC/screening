using Screening.Domain.Parser.Models;
using Screening.Domain.Services.Abstraction;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Screening.Domain.Services;

public class YamlQuestionnaireParser : IQuestionnaireParser
{
    private readonly IDeserializer _deserializer = new DeserializerBuilder()
        .WithNamingConvention(UnderscoredNamingConvention.Instance)
        .WithTypeConverter(new UnitYamlTypeConverter())
        .WithTypeConverter(new YamlQuestionTypeConverter())
        .IgnoreUnmatchedProperties()
        .Build();
    
    public YamlQuestionnaire Parse(string content)
    {
        try
        {
            return _deserializer.Deserialize<YamlQuestionnaire>(content);
        }
        catch (Exception e)
        {
            Console.Error.WriteLine(e);
            throw;
        }
    }
}
