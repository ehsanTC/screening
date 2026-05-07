using YamlDotNet.Serialization;

namespace Screening.Domain.Parser.Models;

public class YamlCondition
{
    [YamlMember(Alias = "questionId")]
    public int QuestionId { get; set; }
    
    [YamlMember(Alias = "operator")]
    public ConditionOperator Operator { get; set; } 
    
    [YamlMember(Alias = "value")]
    public object ExpectedValue { get; set; }

    public override string ToString()
    {
        var op = Operator switch
        {
            ConditionOperator.Equals => "==",
            ConditionOperator.NotEquals => "!=",
            ConditionOperator.GreaterThan => ">",
            ConditionOperator.LessThan => "<",
            ConditionOperator.GreaterThanOrEquals => ">=",
            ConditionOperator.LessThanOrEquals => "<=",
            
            _ => Operator.ToString()
        };
        
        return $"Question(#{QuestionId}) {op} {ExpectedValue}";
    }
}
