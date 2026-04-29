using Screening.Domain.Answers;

namespace Screening.Domain.Questions;

public class MultipleChoiceQuestion : IQuestion
{
    public string Text { get; init; }
    public List<string> Options { get; init; } = [];
    public List<string> SelectedOptions { get; init; } = [];

    public bool IsAnswered => SelectedOptions.Count > 0;
    
    public IAnswer Answer => new MultipleChoiceAnswer() { SelectedOptions = SelectedOptions };

    public void Validate()
    {
        if (!IsAnswered)
        {
            throw new InvalidOperationException("Multiple choice question must have at least one selected option.");
        }
        foreach (var selected in SelectedOptions)
        {
            if (!Options.Contains(selected))
            {
                throw new InvalidOperationException("Selected options must be from the available options.");
            }
        }
    }
}
