using Screening.Domain.RiskCalculator.Abstraction;

namespace Screening.Domain.Questionnaire.Abstraction;

public interface IRiskInputBuilder<out TInput> where TInput : IRiskEvaluatorInput
{
    TInput Build(QuestionnaireAnswerReader reader);
}
