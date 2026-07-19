using Screening.Domain.Questions.Abstraction;

namespace Screening.Domain.Questionnaire;

public class SkipEventArgs : EventArgs
{
    public int QuestionIndex { get; init; }
    public IDisplayableQuestion Question { get; init; }

    public SkipEventArgs(int  questionIndex, IDisplayableQuestion question)
    {
        QuestionIndex = questionIndex;
        Question = question;
    }
}
