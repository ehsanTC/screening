using Screening.Domain.Models;
using Screening.Domain.Questions;

namespace Screening.Domain.RiskCalculator.Abstraction;

public class RiskEvaluatorFactory : IRiskEvaluatorFactory
{
    private readonly IDictionary<QuestionnaireType, IRiskEvaluator> _evaluators;

    public RiskEvaluatorFactory(IEnumerable<IRiskEvaluator> evaluators)
    {
        ArgumentNullException.ThrowIfNull(evaluators);

        _evaluators = evaluators.ToDictionary(x => x.QuestionnaireType);
    }
    
    public IRiskEvaluator? Create(QuestionnaireType type) => 
        _evaluators.TryGetValue(type, out var evaluator)  ? evaluator : null;
}
