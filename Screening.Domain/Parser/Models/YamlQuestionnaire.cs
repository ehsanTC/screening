using YamlDotNet.Serialization;

namespace Screening.Domain.Parser.Models;

public class YamlQuestionnaire
{
    [YamlMember(Alias = "title")]
    public string Title { get; set; }
 
    [YamlMember(Alias = "questions")]
    public List<YamlQuestion> Questions { get; set; }
}
