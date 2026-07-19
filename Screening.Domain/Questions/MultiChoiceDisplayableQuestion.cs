using Screening.Domain.Answers;
using Screening.Domain.Parser.Models;
using Screening.Domain.Questions.Abstraction;

namespace Screening.Domain.Questions;

public class MultiChoiceDisplayableQuestion : IDisplayableQuestion<MultiOptionAnswer>
{
    public required YamlQuestion Question { get; init; }
    
    public List<string> AvailableOptions => Question?.Options?.ToList() ?? [];
    public bool IsAnswered => Answer?.SelectedOptions.Count > 0;

    Answer? IDisplayableQuestion.Answer
    {
        get => Answer;
        set => Answer = (MultiOptionAnswer)value!;
    }

    public MultiOptionAnswer? Answer { get; set; }

    public void Validate()
    {
        if (!IsAnswered)
        {
            throw new InvalidOperationException("Multi choice question must have at least one selected option.");
        }

        var options = Question.Options ?? [];
        var selectedOptions = Answer?.SelectedOptions ?? [];
        
        if (!selectedOptions.Any(selected => options.Contains(selected, StringComparer.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException("Selected options must be from the available options.");
        }
    }

    public void OnSkipped()
    {
        Answer = (MultiOptionAnswer) GetDefaultAnswer();
    }

    public Answer? GetDefaultAnswer() => new MultiOptionAnswer([]);

    public bool IsOptional => true;
    public bool HasSaveConfigs => Question.SaveConfig != null;
}
