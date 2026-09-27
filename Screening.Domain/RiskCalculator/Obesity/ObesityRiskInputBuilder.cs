using Screening.Domain.Questionnaire;
using Screening.Domain.Questionnaire.Abstraction;
using Screening.Domain.RiskCalculator.Models;

namespace Screening.Domain.RiskCalculator.Obesity;

public sealed class ObesityRiskInputBuilder : IRiskInputBuilder<ObesityRiskInput>
{
    private static readonly string[] RequiredCodes =
    [
        QuestionCodes.Age,
        QuestionCodes.Gender,
        QuestionCodes.Height,
        QuestionCodes.Weight,
        QuestionCodes.WaistCircumference,
        QuestionCodes.DyslipidemiaFamilyHistory,
        QuestionCodes.Diabetes,
        QuestionCodes.HighBloodPressure,
        QuestionCodes.Smoking,
        QuestionCodes.PreviouslySmoking
    ];
    
    public ObesityRiskInput Build(QuestionnaireAnswerReader reader)
    {
        reader.EnsureCodesPresent(RequiredCodes);
        
        return new ObesityRiskInput
        {
            Age = reader.GetInt(QuestionCodes.Age),
            Sex = reader.GetBinarySelection<Sex>(QuestionCodes.Gender),
            HeightCm = reader.GetDouble(QuestionCodes.Height) * 100,
            WeightKg = reader.GetDoubleOrNull(QuestionCodes.Weight) ?? 0,
            WaistCm = reader.GetDoubleOrNull(QuestionCodes.WaistCircumference),
            HasFamilyHistory = reader.GetBoolean(QuestionCodes.DyslipidemiaFamilyHistory) ?? false,
            HasDiabetes = reader.GetBoolean(QuestionCodes.Diabetes) ?? false,
            HasHighBloodPressure = reader.GetBoolean(QuestionCodes.HighBloodPressure) ?? false,
            IsCurrentSmoker = reader.GetBoolean(QuestionCodes.Smoking) ?? false,
            IsPastSmoker = reader.GetBoolean(QuestionCodes.PreviouslySmoking) ?? false,
        };
    }
}
