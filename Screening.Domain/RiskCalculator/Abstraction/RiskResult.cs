namespace Screening.Domain.RiskCalculator.Abstraction;

public record RiskResult(
    float Score,
    RiskSeverity Severity,
    string Message);
