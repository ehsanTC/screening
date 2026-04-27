namespace Screening.Domain.Questions;

public class TextInputQuestion : IQuestion
{
    public string Text { get; init; }
    public string Value { get; init; }
    public IAnswer Answer => new TextAnswer() { Text = Value };
    public bool IsNumeric { get; init; } // e.g., for age
    public double? MinValue { get; init; }
    public double? MaxValue { get; init; }

    public bool IsAnswered => !string.IsNullOrEmpty(Value);
    

    public void Validate()
    {
        if (!IsAnswered)
        {
            throw new InvalidOperationException("Text input question must have a value.");
        }
        if (IsNumeric)
        {
            if (!double.TryParse(Value, out var numValue))
            {
                throw new InvalidOperationException("Value must be numeric.");
            }
            if (numValue < MinValue)
            {
                throw new InvalidOperationException($"Value must be at least {MinValue.Value}.");
            }
            if (numValue > MaxValue)
            {
                throw new InvalidOperationException($"Value must be at most {MaxValue.Value}.");
            }
        }
    }
}
