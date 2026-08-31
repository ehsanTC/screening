namespace Screening.Domain.RiskCalculator.Ascvd;

public class HeartRiskResult    
{
    public required double Cvd10Yr { get; init; }
    public required double Ascvd10Yr { get; init; }
    public required double? Hf10Yr { get; init; }
    public required double? Cvd30Yr { get; init; }
    public required double? Ascvd30Yr { get; init; }
    public required double? Hf30Yr { get; init; }
}
