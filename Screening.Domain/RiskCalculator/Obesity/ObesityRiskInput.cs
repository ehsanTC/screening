using Screening.Domain.RiskCalculator.Abstraction;
using Screening.Domain.RiskCalculator.Models;

namespace Screening.Domain.RiskCalculator.Obesity;

public class ObesityRiskInput : IRiskEvaluatorInput
{
    public required int Age { get; init; }
    public required Sex Sex { get; init; }
    public required double HeightCm { get; init; }
    public required double WeightKg { get; init; }
 
    public double? WaistCm { get; init; }
 
    public required bool HasFamilyHistory { get; init; }
    public required bool HasDiabetes { get; init; }
    public required bool HasHighBloodPressure { get; init; }
    public required bool IsCurrentSmoker { get; init; }
    public required bool IsPastSmoker { get; init; }
}
