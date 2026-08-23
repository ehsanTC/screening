namespace Screening.Domain.RiskCalculator.Abstraction;

public record RiskResult(
    string Score,
    RiskSeverity Severity,
    string Message);
