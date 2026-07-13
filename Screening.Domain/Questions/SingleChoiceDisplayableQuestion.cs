using Screening.Domain.Answers;
using Screening.Domain.Parser.Models;
using Screening.Domain.Questions.Abstraction;

namespace Screening.Domain.Questions;

public class SingleChoiceDisplayableQuestion : IDisplayableQuestion<SingleOptionAnswer>
{
    public required YamlQuestion Question { get; init; }
    public List<string> AvailableOptions => Question?.Options?.ToList() ?? [];
    public bool IsAnswered => !string.IsNullOrEmpty(Answer?.SelectedOption);
    Answer? IDisplayableQuestion.Answer
    {
        get => Answer;
        set => Answer = (SingleOptionAnswer)value!;
    }

    public SingleOptionAnswer? Answer { get; set; }

    public void Validate()
    {
        if (!IsAnswered)
        {
            throw new InvalidOperationException("Single choice question must have a selected option.");
        }

        var options = Question.Options ?? [];
        var selectedOption = Answer?.SelectedOption;
        
        if (!options.Contains(selectedOption!))
        {
            throw new InvalidOperationException("Selected option must be one of the available options.");
        }
    }

    public void SetAnswer(SingleOptionAnswer answer) => Answer = answer;
    public bool IsOptional => false;
    public bool HasSaveConfigs => Question.SaveConfig != null;
}
