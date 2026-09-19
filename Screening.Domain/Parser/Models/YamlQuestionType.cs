using System.Runtime.Serialization;

namespace Screening.Domain.Parser.Models;

public enum YamlQuestionType
{
    Unknown,
    
    [EnumMember(Value = "string")] String,
    [EnumMember(Value = "number")] Number,
    [EnumMember(Value = "single_choice")] SingleOption,
    [EnumMember(Value = "multi_choice")] MultiOption,
    [EnumMember(Value = "repeatable_key_value")] RepeatableKeyValue
}
