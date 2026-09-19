using Screening.Domain.RiskCalculator.Abstraction;

namespace Screening.Domain.RiskCalculator.General;

public class GeneralRiskInput : IRiskEvaluatorInput
{
    public bool HaveThyroid { get; set; }    
    public bool HaveVitaminDiDeficiency { get; set; }    
    public bool HaveDepressionAndAnxiety { get; set; }

    public bool BreastCancer { get; set; }
    public bool ColorectalCancer { get; set; }
    public bool StomachCancer { get; set; }
    public bool SkinCancer { get; set; }
    public bool LungCancer { get; set; }
    public bool ProstateCancer { get; set; }
    public bool LiverCancer { get; set; }
}
