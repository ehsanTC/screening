using System.Text.Json.Serialization;
using YamlDotNet.Serialization;

namespace Screening.Domain.Parser.Models;

public class YamlSaveConfig
{
    [JsonPropertyName("path")]
    [YamlMember(Alias = "path")] 
    public string Path { get; init; }
    
    [JsonPropertyName("cast")]
    [YamlMember(Alias = "cast")]
    public Cast CastTo { get; init; } = Cast.String;
    public Unit Unit { get; set; }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Cast
{
    String = 0,
    List,
    Bool,
    Int,
    Float
}
