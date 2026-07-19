using Screening.Domain.Questionnaire.Abstraction;
using Screening.Domain.RiskCalculator.Models;
using Screening.Domain.RiskCalculator.Requests;

namespace Screening.Domain.Questionnaire.InputBuilders;

public class DiabetesRiskInputBuilder : IRiskInputBuilder<DiabetesRiskInput>
{
    private static readonly string[] RequiredCodes =
    [
        QuestionCodes.Age,
        QuestionCodes.Gender,
        QuestionCodes.GestationalDiabetes,
        QuestionCodes.DiabetesFamilyHistory,
        QuestionCodes.HighBloodPressure,
        QuestionCodes.ActiveLifeStyle,
        QuestionCodes.Height,
        QuestionCodes.Weight
    ];
    
    public DiabetesRiskInput Build(QuestionnaireAnswerReader reader)
    {
        reader.EnsureCodesPresent(RequiredCodes);

        var input = new DiabetesRiskInput()
        {
            Age = reader.GetInt(QuestionCodes.Age),
            Sex = reader.GetBinarySelection<Sex>(QuestionCodes.Gender),
            HadGestationalDiabetes = reader.GetBoolean(QuestionCodes.GestationalDiabetes),
            HasFamilyHistory = reader.GetBoolean(QuestionCodes.DiabetesFamilyHistory),
            HasHighBloodPressure = reader.GetBoolean(QuestionCodes.HighBloodPressure),
            IsPhysicallyActive = reader.GetBoolean(QuestionCodes.ActiveLifeStyle),
            HeightCm = reader.GetFloat(QuestionCodes.Height) * 100,
            WeightKg = reader.GetInt(QuestionCodes.Weight)
        };

        return input;
    }
}
