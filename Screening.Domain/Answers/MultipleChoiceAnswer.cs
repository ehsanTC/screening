namespace Screening.Domain.Answers;

public class MultipleChoiceAnswer : IAnswer
{
    public List<string> SelectedOptions { get; init; }
    public List<string> GetAnswers() => SelectedOptions;
}
