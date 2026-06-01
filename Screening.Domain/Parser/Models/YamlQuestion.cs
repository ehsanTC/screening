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
    
    [YamlMember(Alias = "options")]
    public List<string>? Options { get; set; }
    
    [YamlMember(Alias = "showIf")]
    public List<YamlCondition>? DisplayConditions { get; set; }
    
    [YamlMember(Alias = "validation")] 
    public YamlValidation? Validation { get; set; }
    
    [YamlMember(Alias = "validation_error")]
    public string? ErrorMessage { get; set; }
    
    [YamlMember(Alias = "save")] 
    public YamlSaveConfig? SaveConfig { get; set; }
}
