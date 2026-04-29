using Screening.Domain.Answers;

namespace Screening.Domain.Questions;

public interface IQuestion
{
    string Text { get; }
    bool IsAnswered { get; }
    IAnswer Answer { get; }
    void Validate();
}
