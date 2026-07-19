using Screening.Domain.Models;
using Screening.Domain.RiskCalculator.Abstraction;

namespace Screening.Domain.RiskCalculator;

public interface IRiskEvaluatorFactory
{
    IRiskEvaluator? Create(QuestionnaireType type);
}
