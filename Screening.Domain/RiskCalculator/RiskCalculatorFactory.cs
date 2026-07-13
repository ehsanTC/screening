using Screening.Domain.Models;
using Screening.Domain.RiskCalculator.Abstraction;

namespace Screening.Domain.RiskCalculator;

public class RiskCalculatorFactory : IRiskCalculatorFactory
{
    public IRiskCalculator Create(QuestionnaireType type) =>
        type switch
        {
            QuestionnaireType.Diabetes => new DiabetesRiskCalculator(),
            QuestionnaireType.Ascvd => new AscvdRiskCalculator(),
            // QuestionnaireType.Fracture => new FractureRiskCalculator()   TODO

            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
}
