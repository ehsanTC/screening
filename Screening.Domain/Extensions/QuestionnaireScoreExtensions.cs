using Screening.Domain.Parser.Models;
using Screening.Domain.Questions;

namespace Screening.Domain.Extensions;

public static class QuestionnaireScoreExtensions
{
    public static YamlResult? GetCategory(this QuestionnaireEntity questionnaire, double score)
    {
        if (questionnaire?.ResultCategories == null)
            return null;

        return questionnaire.ResultCategories.FirstOrDefault(category => 
            Match(category, score));
    }

    private static string GetMessage(YamlResult category, double score) => 
        $"{string.Format(category.Summary, score)}{Environment.NewLine}{category.NextSteps}";

    public static string GetMessage(this QuestionnaireEntity questionnaire, double score)
    {
        var category = questionnaire.GetCategory(score);

        return category is not null 
            ? GetMessage(category, score)
            : string.Empty;
    }

    private static bool Match(YamlResult? category, double score)
    {
        return category?.Conditions?
                   .All(condition =>
                   {
                       if (!double.TryParse(condition.ExpectedValue.ToString(), out var result))
                           return false;

                       return CompareValues(score, result, condition.Operator);
                   })
               ?? false;
    }

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
