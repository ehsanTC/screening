using Screening.Domain.RiskCalculator.Abstraction;
using Screening.Domain.RiskCalculator.Models;

namespace Screening.Domain.RiskCalculator.Requests;

public class DiabetesRiskArgs : ICalculatorArg
{
    public int Age { get; set; }
    public Sex Sex { get; set; }
    public bool? HadGestationalDiabetes { get; set; }
    public bool HasFamilyHistory { get; set; }
    public bool HasHighBloodPressure { get; set; }
    public bool IsPhysicallyActive { get; set; }
    public double HeightCm { get; set; }
    public double WeightKg { get; set; }
}
