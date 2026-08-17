using System.Diagnostics;
using YamlDotNet.Serialization;

namespace Screening.Domain.Parser.Models;

[DebuggerDisplay("{Text}")]
public class YamlQuestion
{
    [YamlMember(Alias = "id")]
    public int Id { get; set; }
    
    [YamlMember(Alias = "code")]
    public string Code { get; set; }

    [YamlMember(Alias = "text")]
    public string Text { get; set; }
    
    [YamlMember(Alias = "type")]
    public string Type { get; set; }
    
    [YamlMember(Alias = "options")]
    public List<YamlListItem>? Options { get; set; }
    
    [YamlMember(Alias = "defaultAnswer")]
    public YamlListItem? DefaultAnswer  { get; set; }
    
    [YamlMember(Alias = "showIf")]
    public List<YamlCondition>? DisplayConditions { get; set; }
    
    [YamlMember(Alias = "validation")] 
    public YamlValidation? Validation { get; set; }
    
    [YamlMember(Alias = "validation_error")]
    public string? ErrorMessage { get; set; }
    
    [YamlMember(Alias = "save")] 
    public YamlSaveConfig? SaveConfig { get; set; }
    
    [YamlMember(Alias = "required")]
    public bool? IsRequired { get; set; }
}
