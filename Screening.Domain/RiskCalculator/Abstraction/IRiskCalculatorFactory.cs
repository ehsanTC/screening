using Screening.Domain.Models;

namespace Screening.Domain.RiskCalculator.Abstraction;

public interface IRiskCalculatorFactory
{
    IRiskCalculator Create(QuestionnaireType type);
}
