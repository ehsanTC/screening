using Screening.Domain.Answers;
using Screening.Domain.Parser.Models;

namespace Screening.Domain.Questions;

public interface IDisplayableQuestion 
{
    public YamlQuestion  Question { get; }
    bool IsAnswered { get; }
    Answer? Answer { get;}
    void Validate();
}

public interface IDisplayableQuestion<out TAnswer> : IDisplayableQuestion 
    where TAnswer : Answer
{
    new TAnswer? Answer { get; }
}
