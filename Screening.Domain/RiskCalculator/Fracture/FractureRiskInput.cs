using Screening.Domain.RiskCalculator.Abstraction;
using Screening.Domain.RiskCalculator.Models;

namespace Screening.Domain.RiskCalculator.Fracture;

public class FractureRiskInput : IRiskEvaluatorInput
{
    public int Age { get; set; }
    public Sex Sex { get; set; }
    public float HeightCm { get; set; }
    public float WeightKg { get; set; }
    public bool? PreviousFracture { get; set; }
    public bool? ParentFracturedHip { get; set; }
    public bool? Smoking { get; init; }
    public bool? TakingCorticosteroids { get; set; }
    public bool? HaveRheumatoidArthritis { get; set; }
    public bool? HaveSecondaryOsteoporosis { get; set; }
    public bool? DrinkExcessAlcohol { get; set; }
    public BmdMeasure? BmdMeasure { get; set; }
    public float? BmdValue { get; set; }
}
