using Screening.Domain.Models;

namespace Screening.Domain.RiskCalculator.Abstraction;


public interface ICalculatorFactory
{
    IRiskCalculator Create(QuestionnaireType questionnaireType);
}

public class CalculatorFactory : ICalculatorFactory
{
    public IRiskCalculator Create(QuestionnaireType questionnaireType) =>
        questionnaireType.Name switch
        {
            QuestionnaireType.Type.Diabetes => new DiabetesRiskCalculator(),
            QuestionnaireType.Type.Ascvd => new AscvdRiskCalculator(),
            QuestionnaireType.Type.Test  => new TestRiskRiskCalculator(),
            
            _ => throw new NotSupportedException(nameof(questionnaireType.Name)),
        };
}
