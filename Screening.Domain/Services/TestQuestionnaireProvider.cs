using Screening.Domain.Abstractions;
using Screening.Domain.Parser.Models;
using Screening.Domain.Questions;
using Screening.Domain.Questions.Abstraction;

namespace Screening.Domain.Services;

public class TestQuestionnaireProvider : IQuestionnaireProvider
{
    public Questionnaire GetQuestionnaire()
    {
        return new()
        {
            Title = "Test questionnaire",
            Questions = new List<IDisplayableQuestion>()
            {
                new MultiChoiceDisplayableQuestion()
                {
                    Question = new YamlQuestion()
                    {
                        Id = 1,
                        Text = "Which colours do you prefer?",
                        Options = ["Blue", "Yellow", "Green"]
                    },
                },
                new SingleChoiceDisplayableQuestion()
                {
                    Question = new YamlQuestion()
                    {
                        Id = 2,
                        Text = "Which Semester?", Options = ["First", "Seconds"]
                    },
                },
                new TextInputDisplayableQuestion()
                {
                    Question = new YamlQuestion()
                    {
                        Id = 3,
                        Text = "What is your name"
                    },
                },
                new TextInputDisplayableQuestion()
                {
                    Question = new YamlQuestion()
                    {
                        Id = 4, Text = "What is your age?"
                    },
                    IsNumeric = true
                }
            }
        };
    }
}
