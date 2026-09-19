using Screening.Domain.Answers;

namespace Screening.Domain.Extensions;

public static class AnswerExtensions
{
    public static string ToReadableString(this Answer answer, AnswerFormatStrategy? strategy = null)
    {
        strategy ??= AnswerFormatStrategy.Default;

        var r = answer switch
        {
            TextAnswer textAnswer => textAnswer.Text,
            
            SingleOptionAnswer singleOptionAnswer => singleOptionAnswer.Item.Value,
            
            MultiOptionAnswer multiOptionAnswer =>
                strategy.FormatMultiOption(multiOptionAnswer.SelectedOptions),
            
            RepeatableKeyValueAnswer repeatableKeyValueAnswer =>
                strategy.FormatRepeatable(repeatableKeyValueAnswer.LabResults, repeatableKeyValueAnswer.Disorders),
            
            _ => string.Empty
        };
        return r;
    }
}
