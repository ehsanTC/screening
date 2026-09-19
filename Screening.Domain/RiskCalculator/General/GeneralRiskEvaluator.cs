using Screening.Domain.Models;
using Screening.Domain.Questions;
using Screening.Domain.RiskCalculator.Abstraction;

namespace Screening.Domain.RiskCalculator.General;

public class GeneralRiskEvaluator : IRiskEvaluator
{
    public QuestionnaireType QuestionnaireType => QuestionnaireType.GeneralEvaluation;
    
    public RiskResult Evaluate(QuestionnaireEntity questionnaire) => RiskResult.Empty;
}
