namespace Screening.Domain.Answers;

public class TextAnswer : IAnswer
{
    public string Text { get; init; }
    public List<string> GetAnswers() => [Text];
}
