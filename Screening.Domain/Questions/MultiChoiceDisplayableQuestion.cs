using Screening.Domain.Answers;
using Screening.Domain.Parser.Models;

namespace Screening.Domain.Questions;

public class MultiChoiceDisplayableQuestion : IDisplayableQuestion<MultipleChoiceAnswer>
{
    public required YamlQuestion Question { get; init; }
    
    public List<string> Options { get; set; } = [];

    public bool IsAnswered => Answer?.SelectedOptions.Count > 0;

    Answer? IDisplayableQuestion.Answer => Answer;
    public MultipleChoiceAnswer? Answer { get; set; }

    public void Validate()
    {
        if (!IsAnswered)
        {
            throw new InvalidOperationException("Multi choice question must have at least one selected option.");
        }

        var selectedOptions = Answer?.SelectedOptions ?? [];
        if (selectedOptions.Any(selected => !Options.Contains(selected)))
        {
            throw new InvalidOperationException("Selected options must be from the available options.");
        }
    }
}
