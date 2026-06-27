using Screening.Domain.Answers;
using Screening.Domain.Parser.Models;

namespace Screening.Domain.Questions;

public class MultiChoiceDisplayableQuestion : IDisplayableQuestion<MultipleChoiceAnswer>
{
    public required YamlQuestion Question { get; init; }
    
    public List<string> Options { get; set; } = [];

    public bool IsAnswered => Options.Count > 0;

    Answer? IDisplayableQuestion.Answer => Answer;
    public MultipleChoiceAnswer? Answer { get; set; }

    public void Validate()
    {
        var options = Answer?.SelectedOptions;
        if (Options.Any(selected => options.Contains(selected)))
        {
            throw new InvalidOperationException("Selected options must be from the available options.");
        }
    }
}
