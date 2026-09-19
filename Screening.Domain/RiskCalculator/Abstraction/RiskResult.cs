using Screening.Domain.Parser.Models;

namespace Screening.Domain.RiskCalculator.Abstraction;

public record RiskResult(
    string Score,
    RiskSeverity Severity,
    string Summary,
    string Title,
    string SubTitle,
    string RiskDescription,
    string Icon,
    string Color,
    PreventionSteps? PreventionSteps,
    string NextSteps,
    string FollowUp,
    string PatientEducation)
{
    public static RiskResult Empty { get; } = new(
        string.Empty, default, string.Empty, string.Empty, string.Empty,
        string.Empty, string.Empty, string.Empty, null, string.Empty,
        string.Empty, string.Empty);
    
    public bool IsEmpty =>
        string.IsNullOrEmpty(Score) &&
        Severity == default &&
        string.IsNullOrEmpty(Summary) &&
        string.IsNullOrEmpty(Title) &&
        string.IsNullOrEmpty(SubTitle) &&
        string.IsNullOrEmpty(RiskDescription) &&
        string.IsNullOrEmpty(Icon) &&
        string.IsNullOrEmpty(Color) &&
        PreventionSteps is null &&
        string.IsNullOrEmpty(NextSteps) &&
        string.IsNullOrEmpty(FollowUp) &&
        string.IsNullOrEmpty(PatientEducation);
}
