using YamlDotNet.Serialization;

namespace Screening.Domain.Parser.Models;

public class YamlSaveConfig
{
    [YamlMember(Alias = "path")] 
    public string Path { get; init; }
    
    [YamlMember(Alias = "cast")] 
    public Cast CastTo { get; init; } = Cast.String;
    public Unit Unit { get; set; }
    
    [YamlMember(Alias = "default")]
    public string? DefaultValue { get; set; }
}

public enum Cast
{
    String = 0,
    List,
    Bool,
    Int,
    Float
}
