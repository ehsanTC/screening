using Screening.Domain.RiskCalculator.Abstraction;
using YamlDotNet.Serialization;

namespace Screening.Domain.Parser.Models;

public class YamlResultCategory
{
    [YamlMember(Alias = "conditions")]
    public List<YamlCondition> Conditions { get; set; }
    
    [YamlMember(Alias = "value")]
    public string Value { get; set; }

    [YamlMember(Alias = "text")]
    public string Text { get; set; }

    [YamlMember(Alias = "subtitle")]
    public string Subtitle { get; set; }

    [YamlMember(Alias = "description")]
    public string Description { get; set; }

    [YamlMember(Alias = "color")]
    public string Color { get; set; }

    [YamlMember(Alias = "icon")]
    public string Icon { get; set; }

    [YamlMember(Alias = "prevention")]
    public PreventionSteps Prevention { get; set; }

    [YamlMember(Alias = "summary")]
    public string Summary { get; set; }

    [YamlMember(Alias = "next_steps")]
    public string NextSteps { get; set; }

    [YamlMember(Alias = "follow_up")]
    public string FollowUp { get; set; }

    [YamlMember(Alias = "patient_education")]
    public string PatientEducation { get; set; }

    public RiskSeverity GetSeverity() =>
        Enum.TryParse<RiskSeverity>(Value, true, out var result) 
            ? result 
            : RiskSeverity.Unknown;
}
