using Screening.Domain.Answers;
using Screening.Domain.Parser.Models;
using Screening.Domain.Questions.Abstraction;

namespace Screening.Domain.Questions;

public class TextInputDisplayableQuestion : IDisplayableQuestion<TextAnswer>
{
    public required YamlQuestion Question { get; init; }
    public bool IsNumeric { get; init; }
    public bool IsAnswered => !string.IsNullOrEmpty(Answer?.Text);

    Answer? IDisplayableQuestion.Answer
    {
        get => Answer;
        set => Answer = (TextAnswer)value!;
    }

    public TextAnswer? Answer { get; set; }

    public void Validate()
    {
        if (!IsAnswered)
        {
            throw new InvalidOperationException("Text input question must have a value.");
        }

        var response = Answer?.Text;
        if (IsNumeric && !double.TryParse(response, out _))
        {
            throw new InvalidOperationException("Answer must be a whole number.");
        }
    }

    public bool IsOptional => false;
    public bool HasSaveConfigs => Question.SaveConfig != null;
}
