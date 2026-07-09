using Screening.Domain.Answers;
using Screening.Domain.Parser.Models;
using Screening.Domain.Questions.Abstraction;

namespace Screening.Domain.Questions;

public class SingleChoiceDisplayableQuestion : IDisplayableQuestion<SingleChoiceAnswer>
{
    public required YamlQuestion Question { get; init; }
    public List<string> AvailableOptions => Question?.Options?.ToList() ?? [];
    public bool IsAnswered => !string.IsNullOrEmpty(Answer?.SelectedOption);
    Answer? IDisplayableQuestion.Answer => Answer;

    public SingleChoiceAnswer? Answer { get; set; }

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

    public void SetAnswer(SingleChoiceAnswer answer) => Answer = answer;
    public bool IsOptional => false;
}
