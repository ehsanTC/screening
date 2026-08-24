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
    PreventionSteps  PreventionSteps,
    string NextSteps,
    string FollowUp,
    string PatientEducation);
