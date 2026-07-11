using Screening.Domain.Answers;
using Screening.Domain.Parser.Models;

namespace Screening.Domain.Questions.Abstraction;

public interface IDisplayableQuestion : IOptionalQuestion
{
    public YamlQuestion  Question { get; }
    bool IsAnswered { get; }
    Answer? Answer { get; set; }
    void Validate();
}

public interface IDisplayableQuestion<out TAnswer> : IDisplayableQuestion 
    where TAnswer : Answer
{
    new TAnswer? Answer { get; }
}
