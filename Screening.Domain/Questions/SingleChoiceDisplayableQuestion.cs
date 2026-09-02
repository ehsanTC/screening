using Screening.Domain.Answers;
using Screening.Domain.Parser.Models;
using Screening.Domain.Questions.Abstraction;

namespace Screening.Domain.Questions;

public class SingleChoiceDisplayableQuestion : IDisplayableQuestion<SingleOptionAnswer>
{
    public required YamlQuestion Question { get; init; }
    public List<YamlListItem> AvailableOptions => Question?.Options?.ToList() ?? [];
    public bool IsAnswered => Answer is not null;
    Answer? IDisplayableQuestion.Answer
    {
        get => Answer;
        set => Answer = (SingleOptionAnswer)value!;
    }

    public SingleOptionAnswer? Answer { get; set; }

    public void Validate()
    {
        if (IsOptional)
            return;
        
        if (!IsAnswered)
        {
            // The question is not Optional and not Answered
            throw new InvalidOperationException("Single choice question must have a selected option.");
        }

        var options = Question.Options ?? [];
        var selectedOption = Answer?.Item;
        
        if (!options.Contains(selectedOption!))
        {
            throw new InvalidOperationException("Selected option must be one of the available options.");
        }
    }

    public void OnSkipped()
    {
        Answer = (SingleOptionAnswer) GetDefaultAnswer();
    }

    public Answer? GetDefaultAnswer() => new SingleOptionAnswer(Question.DefaultAnswer);

    public void SetAnswer(SingleOptionAnswer answer) => Answer = answer;
    public bool IsOptional => !Question?.IsRequired ?? false;
    public bool HasSaveConfigs => Question.SaveConfig != null;
}
