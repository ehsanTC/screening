using Screening.Domain.Answers;

namespace Screening.Domain.Extensions;

public static class AnswerExtensions
{
    public static string ToAiReadableString(this Answer answer) =>
        answer switch
        {
            TextAnswer textAnswer => textAnswer.Text,
            SingleOptionAnswer singleOptionAnswer => singleOptionAnswer.Item.Value,
            MultiOptionAnswer multiOptionAnswer => string.Join(" and ",
                multiOptionAnswer.SelectedOptions.Select(x => x.Value)),

            _ => String.Empty
        };
}
