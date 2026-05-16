using Screening.Domain.Exceptions;
using Screening.Domain.Parser.Models;

namespace Screening.Domain.Questions;

public class QuestionnaireViewModel
{
    public string? Title { get; set; }
    public IReadOnlyList<IDisplayableQuestion> Questions { get; set; } = [];

    public bool ShowQuestionAtIndex(int questionIndex)
    {
        if (questionIndex < 0 || questionIndex >= Questions.Count)
            throw new ArgumentOutOfRangeException(nameof(questionIndex));

        return ShowQuestionById(Questions[questionIndex].Question.Id);
    }

    public bool ShowQuestionById(int questionId)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(questionId);

        var sourceQuestion = Questions.SingleOrDefault(x => x.Question?.Id == questionId);

        if (sourceQuestion == null)
            throw new QuestionNotFoundException($"Question with id {questionId} not found");

        var conditions = sourceQuestion?.Question?.DisplayConditions ?? [];

        if (conditions?.Count == 0)
            return true;

        var isMet = true;

        // Condition exists
        foreach (var condition in conditions)
        {
            CheckConditionQuestion(condition.QuestionId);
            var targetQuestion = Questions
                .First(q => q.Question.Id == condition.QuestionId);

            if (!targetQuestion.IsAnswered)
                return false;

            var answers = targetQuestion.Answer?.GetAnswers();
            isMet &= CompareAnswerWithCondition(condition, answers);
        }

        return isMet;
    }

    private static bool CompareAnswerWithCondition(YamlCondition? condition, List<string>? answers)
    {
        if (condition == null)
            throw new ArgumentNullException(nameof(condition));

        if (answers == null)
            throw new ArgumentNullException(nameof(answers));

        if (answers.Count > 1)
            throw new NotSupportedException("Multiple answers are not supported.");

        var conditionValue = condition.ExpectedValue.ToString();
        return CompareStrings(answers[0], conditionValue, condition.Operator);
    }

    private static bool CompareStrings(string answer, string? conditionValue, ConditionOperator @operator)
    {
        var comparison = int.TryParse(answer, out var left) && int.TryParse(conditionValue, out var right)
            ? left.CompareTo(right)
            : string.Compare(answer, conditionValue, StringComparison.Ordinal);

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

    private void CheckConditionQuestion(int questionId)
    {
        if (Questions.All(q => q.Question.Id != questionId))
            throw new InvalidYamlDataException($"Question with ID {questionId} doesn't exist.");
    }
}
