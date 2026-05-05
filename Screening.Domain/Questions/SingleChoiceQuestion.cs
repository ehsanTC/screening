using Screening.Domain.Answers;
using Screening.Domain.Parser.Models;

namespace Screening.Domain.Questions;

public class SingleChoiceQuestion : IQuestionViewModel
{
    public List<string> Options { get; init; } = [];
    public string? SelectedOption { get; set; }

    public required YamlQuestion Question { get; init; }
    public bool IsAnswered => !string.IsNullOrEmpty(SelectedOption);

    public IAnswer? Answer => IsAnswered ? new TextAnswer { Text = SelectedOption } : null;

    public void Validate()
    {
        if (!IsAnswered)
        {
            throw new InvalidOperationException("Single choice question must have a selected option.");
        }

        if (!Options.Contains(SelectedOption))
        {
            throw new InvalidOperationException("Selected option must be one of the available options.");
        }
    }
}
