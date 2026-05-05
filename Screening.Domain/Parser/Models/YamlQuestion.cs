using YamlDotNet.Core.Tokens;
using YamlDotNet.Serialization;

namespace Screening.Domain.Parser.Models;

public class YamlQuestion
{
    [YamlMember(Alias = "id")]
    public int Id { get; set; }
    
    [YamlMember(Alias = "text")]
    public string Text { get; set; }
    
    [YamlMember(Alias = "type")]
    public string Type { get; set; }
    
    [YamlMember(Alias = "choices")]
    public List<string> Choices { get; set; }
    
    [YamlMember(Alias = "showIf")]
    public List<YamlCondition> DisplayConditions { get; set; }
}
