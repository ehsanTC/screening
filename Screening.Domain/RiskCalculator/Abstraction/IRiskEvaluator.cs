using Screening.Domain.Models;
using Screening.Domain.Questions;

namespace Screening.Domain.RiskCalculator.Abstraction;

/// <summary>
/// Assess the risk for a disorder related to a questionnaire
/// </summary>
public interface IRiskEvaluator
{
    QuestionnaireType QuestionnaireType { get; }
    RiskResult Evaluate(QuestionnaireEntity questionnaire);
}
