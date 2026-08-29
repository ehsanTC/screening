using Screening.Domain.Models;

namespace Screening.Domain.RiskCalculator.Abstraction;

public interface IRiskEvaluatorFactory
{
    IRiskEvaluator? Create(QuestionnaireType type);
}
