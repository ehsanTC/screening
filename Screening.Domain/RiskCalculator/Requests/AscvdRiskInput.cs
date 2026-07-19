using Screening.Domain.RiskCalculator.Abstraction;
using Screening.Domain.RiskCalculator.Models;

namespace Screening.Domain.RiskCalculator.Requests;

public class AscvdRiskInput : IRiskEvaluatorInput
{
    public int Age { get; set; }
    public Sex Sex { get; set; }
    public Race Race { get; set; }
    public double TotalCholesterolMgdl { get; set; }
    public double HdlCholesterolMgdl { get; set; }
    public double SystolicBpMmHg { get; set; }
    public bool OnHypertensionTreatment { get; set; }
    public bool IsCurrentSmoker { get; set; }
    public bool HasDiabetes { get; set; }
}
