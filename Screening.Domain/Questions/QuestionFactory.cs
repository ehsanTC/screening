using Screening.Domain.Parser.Models;

namespace Screening.Domain.Questions;

public static class QuestionFactory
{
    public static IQuestionViewModel CreateQuestion(YamlQuestion yamlQuestion)
    {
        if (yamlQuestion is null)
            throw new ArgumentException("The question type is null.");

        return yamlQuestion.Type switch
        {
            var t when t.Equals("string", StringComparison.OrdinalIgnoreCase) =>
                CreateTextInput(yamlQuestion, false),

            var t when t.Equals("number", StringComparison.OrdinalIgnoreCase) =>
                CreateTextInput(yamlQuestion, true),

            var t when t.Equals("singlechoice", StringComparison.OrdinalIgnoreCase) =>
                CreateSingleChoice(yamlQuestion),

            var t when t.Equals("multichoice", StringComparison.OrdinalIgnoreCase) =>
                CreateMultiChoice(yamlQuestion),

            _ => throw new NotSupportedException("The question type is not supported.")
        };
    }

    private static IQuestionViewModel CreateMultiChoice(YamlQuestion question) =>
        new MultiChoiceQuestion()
        {
            Question = question,
            Options = question.Choices
        };

    private static IQuestionViewModel CreateSingleChoice(YamlQuestion question) =>
        new SingleChoiceQuestion()
        {
            Question = question,
            Options = question.Choices,
        };

    private static IQuestionViewModel CreateTextInput(YamlQuestion question, bool isNumeric) =>
        new TextInputQuestion()
        {
            Question = question,
            IsNumeric = isNumeric
        };
}
