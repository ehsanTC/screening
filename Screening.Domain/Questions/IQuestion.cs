namespace Screening.Domain.Questions;

public interface IQuestion
{
    string Text { get; }
    bool IsAnswered { get; }
    IAnswer Answer { get; }
    void Validate();
}

public interface IAnswer
{ }

public class TextAnswer : IAnswer
{
    public string Text { get; init; }
}


