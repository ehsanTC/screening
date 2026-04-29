namespace Screening.Domain.Answers;

public class SingleChoiceAnswer : IAnswer
{
    public string SelectedOption { get; init; }
    
    public List<string> GetAnswers() => [SelectedOption];
}
