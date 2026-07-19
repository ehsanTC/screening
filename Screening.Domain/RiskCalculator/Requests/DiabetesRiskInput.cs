using Screening.Domain.RiskCalculator.Abstraction;
using Screening.Domain.RiskCalculator.Models;

namespace Screening.Domain.RiskCalculator.Requests;

public class DiabetesRiskInput : IRiskEvaluatorInput
{
    public int Age { get; set; }
    public Sex Sex { get; set; }
    public bool? HadGestationalDiabetes { get; set; }
    public bool HasFamilyHistory { get; set; }
    public bool HasHighBloodPressure { get; set; }
    public bool IsPhysicallyActive { get; set; }
    public float HeightCm { get; set; }
    public float WeightKg { get; set; }
}
