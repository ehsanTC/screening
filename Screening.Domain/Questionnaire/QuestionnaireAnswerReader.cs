using Screening.Domain.Answers;
using Screening.Domain.Exceptions;

namespace Screening.Domain.Questionnaire;

public class QuestionnaireAnswerReader
{
    private readonly Dictionary<string, Answer> _answersByCode;

    public QuestionnaireAnswerReader(Domain.Questions.QuestionnaireEntity questionnaireEntity)
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
        if (!_answersByCode.TryGetValue(code, out Answer? answer) || answer is null)
            throw new MissingAnswerException(code); // TODO : return null instead of exception
        
        return answer;
    }

    public int GetInt(string code) =>
        int.Parse(((TextAnswer)GetAnswer(code)).Text);
    
    public double GetDouble(string code) =>
        double.Parse(((TextAnswer)GetAnswer(code)).Text);

    public float GetFloat(string code) => 
        float.Parse(((TextAnswer)GetAnswer(code)).Text);

    public bool GetYesNo(string code) =>
        ((SingleOptionAnswer)GetAnswer(code)).SelectedOption == "Yes";

    public string GetBinarySelection(string code) => 
        ((SingleOptionAnswer)GetAnswer(code)).SelectedOption;

    public TEnum GetBinarySelection<TEnum>(string code) where TEnum : struct, Enum
        => Enum.Parse<TEnum>(GetBinarySelection(code));

    public TEnum GetEnum<TEnum>(string code, IReadOnlyDictionary<string, TEnum> optionMap)
        where TEnum : struct
    {
        var selected = ((SingleOptionAnswer)GetAnswer(code)).SelectedOption;
        if (!optionMap.TryGetValue(selected, out var value))
            throw new NotSupportedException(string.Concat(code, selected));
        
        return value;
    }

    public bool GetBoolean(string code)
    {
        var answer = GetAnswer(code);
        if (answer is TextAnswer textAnswer)
            return ConvertToBool(textAnswer.Text);
        
        if (answer is SingleOptionAnswer singleOptionAnswer)
            return ConvertToBool(singleOptionAnswer.SelectedOption);
        
        throw new NotSupportedException(string.Concat(code, answer));
    }

    private static bool ConvertToBool(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return false; // Or throw an ArgumentException

        switch (input.Trim().ToLowerInvariant())
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
                throw new FormatException($"String '{input}' is not a valid boolean representation.");
        }
    }
}
