using Screening.Domain.Parser.Models;

namespace Screening.Domain.Questions;

public static class QuestionFactory
{
    public static IDisplayableQuestion CreateQuestion(YamlQuestion yamlQuestion)
    {
        if (yamlQuestion is null)
            throw new ArgumentException("The question type is null.");

        return yamlQuestion.Type switch
        {
            var questionType when questionType.Equals("string", StringComparison.OrdinalIgnoreCase) =>
                CreateTextInput(yamlQuestion, false),

            var questionType when questionType.Equals("number", StringComparison.OrdinalIgnoreCase) =>
                CreateTextInput(yamlQuestion, true),

            var questionType when questionType.Equals("singlechoice", StringComparison.OrdinalIgnoreCase) =>
                CreateSingleChoice(yamlQuestion),

            var questionType when questionType.Equals("multichoice", StringComparison.OrdinalIgnoreCase) =>
                CreateMultiChoice(yamlQuestion),

            _ => throw new NotSupportedException("The question type is not supported.")
        };
    }

    private static IDisplayableQuestion CreateMultiChoice(YamlQuestion question) =>
        new MultiChoiceDisplayableQuestion()
        {
            Question = question,
            Options = question.Choices
        };

    private static IDisplayableQuestion CreateSingleChoice(YamlQuestion question) =>
        new SingleChoiceDisplayableQuestion()
        {
            Question = question,
            Options = question.Choices,
        };

    private static IDisplayableQuestion CreateTextInput(YamlQuestion question, bool isNumeric) =>
        new TextInputDisplayableQuestion()
        {
            Question = question,
            IsNumeric = isNumeric
        };
}
