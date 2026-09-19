using Screening.Domain.Models;
using YamlDotNet.Serialization;

namespace Screening.Domain.Parser.Models;

public class YamlQuestionnaire
{
    [YamlMember(Alias = "title")]
    public string Title { get; set; }
 
    [YamlMember(Alias = "version")]
    public float Version { get; set; }
    
    [YamlMember(Alias = "type")]
    public QuestionnaireType Type { get; set; }
    
    [YamlMember(Alias = "questions")]
    public List<YamlQuestion> Questions { get; set; }

    [YamlMember(Alias = "results")] 
    public List<YamlResultCategory> Results { get; set; } = [];
}
