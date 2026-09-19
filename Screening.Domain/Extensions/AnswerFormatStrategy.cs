using System.Text;
using Screening.Domain.Models;
using Screening.Domain.Parser.Models;

namespace Screening.Domain.Extensions;

public sealed class AnswerFormatStrategy
{
    public string JoinString { get; }
    public Func<List<YamlListItem>, string> FormatMultiOption { get; }
    public Func<List<KeyValue>, List<string>, string> FormatRepeatable { get; }

    public AnswerFormatStrategy(
        string joinString,
        Func<List<YamlListItem>, string> formatMultiOption,
        Func<List<KeyValue>, List<string>, string> formatRepeatable)
    {
        JoinString = joinString;
        FormatMultiOption = formatMultiOption;
        FormatRepeatable = formatRepeatable;
    }

    public static AnswerFormatStrategy Create(string joinString) => 
        Create(joinString, string.Empty);
    public static AnswerFormatStrategy Create(string joinString, string itemPrefix) =>
        new(
            joinString: joinString,

            formatMultiOption: options =>
                string.Join(joinString, options.Select(x => $"{itemPrefix}{x.Value}")),

            formatRepeatable: (labResults, disorders) =>
            {
                var sb = new StringBuilder();

                if (disorders is {Count: > 0})
                    sb.AppendLine(string.Join(joinString, disorders.Select(disorder => 
                        $"{itemPrefix}{disorder}")));
                
                if (labResults is  {Count: > 0})
                    sb.AppendLine(string.Join(joinString, 
                        labResults.Select(lr => $"{itemPrefix}{lr.Key} : {lr.Value}")));

                return sb.ToString();
            });

    public static AnswerFormatStrategy Default => Create(" and ");
    public static AnswerFormatStrategy AiFriendly => Create(Environment.NewLine, " - ");
}
