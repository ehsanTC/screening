using Screening.Domain.RiskCalculator.Abstraction;
using Screening.Domain.RiskCalculator.Common;
using Screening.Domain.RiskCalculator.Models;

namespace Screening.Domain.RiskCalculator.Liver;

public class LiverRiskInput : IRiskEvaluatorInput
{
    public int Age { get; set; }
    public float HeightCm { get; set; }
    public int WeightKg { get; set; }
    public Sex Gender { get; set; }
    public bool HaveDiabetes { get; set; }
    public bool HaveMetabolicSyndrome { get; set; }
    public bool? UltrasonicFattyLiver { get; set; }
    public int? Alt { get; set; }
    public int? Ast { get; set; }
    public int? PlateletCount { get; set; }
    public double? Albumin { get; set; }

    public double? Bmi => Utility.Bmi(HeightCm, WeightKg);
}
