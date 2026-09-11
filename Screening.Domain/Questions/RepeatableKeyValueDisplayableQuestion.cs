using Screening.Domain.Answers;
using Screening.Domain.Parser.Models;
using Screening.Domain.Questions.Abstraction;

namespace Screening.Domain.Questions;

public class RepeatableKeyValueDisplayableQuestion : IDisplayableQuestion<RepeatableKeyValueAnswer>
{
    public bool IsOptional => true;
    public bool HasSaveConfigs => Question.SaveConfig != null;
    public YamlQuestion Question { get; init; }
    public bool IsAnswered => Answer != null;
    public bool HasDisorder => Answer?.Disorders.Count > 0;

    public bool HasLabTest  => Answer?.LabResults.Count > 0;
    
    public RepeatableKeyValueAnswer? Answer { get; private set; }

    Answer? IDisplayableQuestion.Answer
    {
        get => Answer; 
        set => Answer =  value as RepeatableKeyValueAnswer;
    }
    public void Validate()
    {
        if (!HasDisorder && !HasLabTest)
            return;
        
        if (Answer is null)
            throw new InvalidOperationException("Answer is null");
    }

    public void OnSkipped()
    { }

    public Answer? GetDefaultAnswer() => null;
}
