namespace Screening.Domain.Questions;

public class SingleChoiceQuestion : IQuestion
{
    public string Text { get; init; }
    public List<string> Options { get; init; } = [];
    public string SelectedOption { get; init; }

    public bool IsAnswered => !string.IsNullOrEmpty(SelectedOption);
    public IAnswer Answer { get; }

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
