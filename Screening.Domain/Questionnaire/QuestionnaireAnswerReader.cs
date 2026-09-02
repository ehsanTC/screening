using Screening.Domain.Answers;
using Screening.Domain.Exceptions;
using Screening.Domain.Parser.Models;
using Screening.Domain.Questions;

namespace Screening.Domain.Questionnaire;

public class QuestionnaireAnswerReader
{
    private readonly Dictionary<string, Answer> _answersByCode;

    public QuestionnaireAnswerReader(QuestionnaireEntity questionnaireEntity)
    {
        ArgumentNullException.ThrowIfNull(questionnaireEntity);

        _answersByCode = questionnaireEntity
            .Questions
            .ToDictionary(q => q.Question.Code, q => q.Answer!);
    }

    public void EnsureCodesPresent(IEnumerable<string> codes)
    {
        var missing = codes.Where(c => !_answersByCode.ContainsKey(c)).ToList();
        if (missing.Count > 0)
            throw new MissingAnswerException(missing);
    }

    private Answer? GetAnswer(string code)
    {
        if (!_answersByCode.TryGetValue(code, out Answer? answer) || answer == null)
            return null;
        
        return answer;
    }

    public int? GetInt(string code) =>
        int.TryParse(((TextAnswer)GetAnswer(code))?.Text, out var result) ?  result : null;
    
    public double? GetDouble(string code) =>
        double.TryParse(((TextAnswer)GetAnswer(code))?.Text, out var result) ?  result : null;

    public float? GetFloat(string code) => 
        float.TryParse(((TextAnswer)GetAnswer(code))?.Text, out var result) ?  result : null;

    public bool GetYesNo(string code) =>
        ((SingleOptionAnswer)GetAnswer(code)).Item.Value.Equals("Yes", StringComparison.OrdinalIgnoreCase);

    public string GetBinarySelection(string code) => 
        ((SingleOptionAnswer)GetAnswer(code)).Item.Value;

    public TEnum GetBinarySelection<TEnum>(string code) where TEnum : struct, Enum
    {
        var selection = GetBinarySelection(code);
        
        // TODO: if the selection is farsi like "mard". the equivalent value (Male) should 
        // be placed. The best approach is adding a OptionValue to selectionOption in the 
        // yaml file
        return Enum.Parse<TEnum>(selection);
    }

    public TEnum GetEnum<TEnum>(string code, IReadOnlyDictionary<string, TEnum> optionMap)
        where TEnum : struct, Enum
    {
        var selected = ((SingleOptionAnswer)GetAnswer(code)).Item;
        if (!optionMap.TryGetValue(selected.Value, out var value))
            throw new NotSupportedException(string.Concat(code, selected));
        
        return value;
    }

    public bool? GetBoolean(string code)
    {
        var answer = GetAnswer(code);
        return answer switch
        {
            TextAnswer textAnswer => ConvertToBool(textAnswer.Text),
            SingleOptionAnswer singleOptionAnswer => ConvertToBool(singleOptionAnswer.Item.Value),
            _ => null
        };
    }

    private static bool ConvertToBool(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return false; // Or throw an ArgumentException

        var lowerInvariant = input.Trim().ToLowerInvariant();
        switch (lowerInvariant)
        {
            case "true":
            case "yes":
            case "1":
            case "y":
                return true;
            case "false":
            case "no":
            case "0":
            case "n":
                return false;
            default:
            {
                throw new FormatException($"String '{input}' is not a valid boolean representation.");
            }
        }
    }

    public YamlListItem? GetSingleItem(string code)
    {
        var answer = GetAnswer(code);
        if (answer is SingleOptionAnswer selectedItem)
            return selectedItem.Item;
        
        return null;
    }
}
