using Screening.Domain.Parser.Models;
using Screening.Domain.Questions;
using Screening.Domain.RiskCalculator.Abstraction;

namespace Screening.Domain.Extensions;

public static class QuestionnaireScoreExtensions
{
    public static YamlResultCategory? GetCategory(this QuestionnaireEntity questionnaire, double score)
    {
        if (questionnaire?.ResultCategories is null or {Count: 0 })
            return null;

        return questionnaire.ResultCategories.FirstOrDefault(category => 
            Match(category, score));
    }

    public static YamlResultCategory? GetCategory(this QuestionnaireEntity questionnaire, RiskSeverity severity)
    {
        if (questionnaire?.ResultCategories is null or {Count: 0 })
            return null;

        return questionnaire.ResultCategories.FirstOrDefault(category =>
        {
            var condition = category.Conditions.SingleOrDefault()
                            ?? throw new NotSupportedException("Only one category is supported for Risk Severities.");

            return condition.Operator == ConditionOperator.Equals
                ? Enum.TryParse(condition.ExpectedValue.ToString(), true, out RiskSeverity expectedValue) && expectedValue == severity
                : throw new NotSupportedException("Only Equals condition is supported for Risk Severities.");
        });
    }

    private static bool Match(YamlResultCategory? category, double score) =>
        category?.Conditions?
            .All(condition => 
                double.TryParse(condition.ExpectedValue.ToString(), out var result) 
                && CompareValues(score, result, condition.Operator))
        ?? false;

    private static bool CompareValues(double left, double right, ConditionOperator @operator)
    {
        var comparison = left.CompareTo(right);

        return @operator switch
        {
            ConditionOperator.Equals => comparison == 0,
            ConditionOperator.NotEquals => comparison != 0,
            ConditionOperator.GreaterThan => comparison > 0,
            ConditionOperator.GreaterThanOrEquals => comparison >= 0,
            ConditionOperator.LessThan => comparison < 0,
            ConditionOperator.LessThanOrEquals => comparison <= 0,

            _ => throw new ArgumentOutOfRangeException(nameof(@operator), @operator, null)
        };
    }
}
