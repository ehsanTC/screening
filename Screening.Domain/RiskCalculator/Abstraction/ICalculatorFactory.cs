using Screening.Domain.Models;

namespace Screening.Domain.RiskCalculator.Abstraction;


public interface ICalculatorFactory
{
    IRiskCalculator Create(QuestionnairePageRequest questionnairePageRequest);
}

public class CalculatorFactory : ICalculatorFactory
{
    public IRiskCalculator Create(QuestionnairePageRequest questionnairePageRequest) =>
        questionnairePageRequest.Name switch
        {
            QuestionnaireType.Diabetes => new DiabetesRiskCalculator(),
            QuestionnaireType.Ascvd => new AscvdRiskCalculator(),
            QuestionnaireType.Test  => new TestRiskRiskCalculator(),
            
            _ => throw new NotSupportedException(nameof(questionnairePageRequest.Name)),
        };
}
