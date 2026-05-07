using Screening.Domain.Answers;
using Screening.Domain.Parser.Models;

namespace Screening.Domain.Questions;

public class MultiChoiceDisplayableQuestion : IDisplayableQuestion
{
    public required YamlQuestion Question { get; init; }
    
    public List<string> Options { get; init; } = [];
    
    public List<string> SelectedOptions { get; set; } = [];

    public bool IsAnswered => SelectedOptions.Count > 0;
    
    public IAnswer? Answer => 
        IsAnswered ? new MultipleChoiceAnswer() { SelectedOptions = SelectedOptions } :  null;

    public void Validate()
    {
        if (!IsAnswered)
        {
            throw new InvalidOperationException("Multiple choice question must have at least one selected option.");
        }

        if (SelectedOptions.Any(selected => !Options.Contains(selected)))
        {
            throw new InvalidOperationException("Selected options must be from the available options.");
        }
    }
}
