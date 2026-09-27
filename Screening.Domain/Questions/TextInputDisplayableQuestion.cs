using System.Globalization;
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
        if (IsOptional)
            return;
        
        if (!IsAnswered)
            throw new InvalidOperationException("Text input question must have a value.");

        var response = Answer?.Text;
        var canParse = double.TryParse(response, NumberStyles.Float, CultureInfo.InvariantCulture, out _);
        
        if (IsNumeric && !canParse)
        {
            throw new InvalidOperationException("Answer must be a whole number.");
        }
    }

    public void OnSkipped()
    {
        var answer = GetDefaultAnswer();
        if (answer is not TextAnswer textAnswer)
            return;
        
        Answer = textAnswer;
    }

    public Answer? GetDefaultAnswer() => new TextAnswer(string.Empty);

    public bool IsOptional => !Question.IsRequired ?? false;
    public bool HasSaveConfigs => Question.SaveConfig != null;
}
