using YamlDotNet.Serialization;

namespace Screening.Domain.Parser.Models;

public class YamlCondition
{
    [YamlMember(Alias = "questionId")]
    public int QuestionId { get; set; }
    
    [YamlMember(Alias = "operator")]
    public ConditionOperator Operator { get; set; } 
    
    [YamlMember(Alias = "value")]
    public object Value { get; set; }
}
