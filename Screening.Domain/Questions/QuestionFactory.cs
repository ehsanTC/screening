using Screening.Domain.Parser.Models;
using Screening.Domain.Questions.Abstraction;

namespace Screening.Domain.Questions;

public static class QuestionFactory
{
    public static IDisplayableQuestion CreateQuestion(YamlQuestion yamlQuestion)
    {
        if (yamlQuestion is null)
            throw new ArgumentException("The question type is null.");

        return yamlQuestion.Type switch
        {
            YamlQuestionType.Number => CreateTextInput(yamlQuestion, true),
            YamlQuestionType.String => CreateTextInput(yamlQuestion, false),
            YamlQuestionType.SingleOption => CreateSingleChoice(yamlQuestion),
            YamlQuestionType.MultiOption => CreateMultiChoice(yamlQuestion),
            YamlQuestionType.RepeatableKeyValue => CreateRepeatableQuestion(yamlQuestion),

            _ => throw new NotSupportedException("The question type is not supported.")
        };
    }

    private static IDisplayableQuestion CreateRepeatableQuestion(YamlQuestion question) =>
        new RepeatableKeyValueDisplayableQuestion()
        {
            Question = question
        };

    private static IDisplayableQuestion CreateMultiChoice(YamlQuestion question) =>
        new MultiChoiceDisplayableQuestion()
        {
            Question = question,
        };

    private static IDisplayableQuestion CreateSingleChoice(YamlQuestion question) =>
        new SingleChoiceDisplayableQuestion()
        {
            Question = question,
        };

    private static IDisplayableQuestion CreateTextInput(YamlQuestion question, bool isNumeric) =>
        new TextInputDisplayableQuestion()
        {
            Question = question,
            IsNumeric = isNumeric
        };
}
