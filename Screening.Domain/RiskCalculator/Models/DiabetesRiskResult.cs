namespace Screening.Domain.RiskCalculator.Models;

public record DiabetesRiskResult(
    int Score,
    string RiskLevel,
    string Recommendation);
