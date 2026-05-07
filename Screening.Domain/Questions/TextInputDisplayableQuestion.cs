using Screening.Domain.Answers;
using Screening.Domain.Parser.Models;

namespace Screening.Domain.Questions;

public class TextInputDisplayableQuestion : IDisplayableQuestion
{
    public required YamlQuestion Question { get; init; }
    public string Response { get; set; } = string.Empty;
    public IAnswer Answer => new TextAnswer() { Text = Response };
    public bool IsNumeric { get; init; }

    public bool IsAnswered => !string.IsNullOrEmpty(Response);

    public void Validate()
    {
        if (!IsAnswered)
        {
            throw new InvalidOperationException("Text input question must have a value.");
        }

        if (IsNumeric && !double.TryParse(Response, out _))
        {
            throw new InvalidOperationException("Answer must be numeric.");
        }
    }
}
