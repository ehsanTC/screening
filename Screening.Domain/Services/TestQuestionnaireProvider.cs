using Screening.Domain.Abstractions;
using Screening.Domain.Parser.Models;
using Screening.Domain.Questions;

namespace Screening.Domain.Services;

public class TestQuestionnaireProvider : IQuestionProvider
{
    public QuestionnaireContext GetQuestionnaire()
    {
        return new()
        {
            Title = "Test questionnaire",
            Questions = new List<IDisplayableQuestion>()
            {
                new TextInputDisplayableQuestion()
                {
                    Question = new YamlQuestion()
                    {
                        Id = 1,
                        Text = "What is your name"
                    },
                },
                new TextInputDisplayableQuestion()
                {
                    Question = new YamlQuestion()
                    {
                        Id = 2, Text = "What is your age?"
                    },
                    IsNumeric = true
                }
            }
        };
    }
}
