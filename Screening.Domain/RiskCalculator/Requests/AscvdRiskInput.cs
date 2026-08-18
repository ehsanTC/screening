using Screening.Domain.RiskCalculator.Abstraction;
using Screening.Domain.RiskCalculator.Common;
using Screening.Domain.RiskCalculator.Models;

namespace Screening.Domain.RiskCalculator.Requests;

public class AscvdRiskInput : IRiskEvaluatorInput
{
    public Sex Sex { get; init; }
    public double Age { get; init; }
    public double TotalCholesterol { get; init; } // mg/dL
    public double HdlCholesterol { get; init; }   // mg/dL
    public double SystolicBp { get; init; }
    public float HeightCm { get; set; }
    public float WeightKg { get; set; }
    public double Egfr { get; init; }
    public bool Diabetic { get; init; }
    public bool Smoker { get; init; }
    public bool HypertensionTreated { get; init; }
    public bool LipidLowering { get; init; }
    public double? Uacr { get; init; }
    public double? HbA1c { get; init; }
    public SdiCategory? SdiCategory { get; init; }
    
    public double? Bmi => Utility.Bmi(HeightCm, WeightKg);
}
