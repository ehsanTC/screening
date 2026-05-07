using Screening.Domain.Answers;
using Screening.Domain.Parser.Models;

namespace Screening.Domain.Questions;

public interface IDisplayableQuestion
{
    public YamlQuestion  Question { get; }
    bool IsAnswered { get; }
    IAnswer? Answer { get; }
    void Validate();
}
