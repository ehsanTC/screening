using Screening.Domain.Questionnaire;
using Screening.Domain.Questionnaire.Abstraction;
using Screening.Domain.RiskCalculator.Models;

namespace Screening.Domain.RiskCalculator.Ascvd;

public class AscvdRiskInputBuilder : IRiskInputBuilder<AscvdRiskInput>
{
    private static readonly Dictionary<string, Sex> SexMap = new(StringComparer.OrdinalIgnoreCase)
    {
        ["Male"] = Sex.Male,
        ["Female"] = Sex.Female
    };

    private static readonly Dictionary<string, SdiCategory> WealthCategory = new(StringComparer.OrdinalIgnoreCase)
    {
        ["Rich"] = SdiCategory.LeastDeprived,
        ["Middle"] = SdiCategory.Middle,
        ["Poor"] = SdiCategory.MostDeprived
    };

    private static readonly string[] RequiredCodes =
    [
        QuestionCodes.Age,
        QuestionCodes.Gender,
        QuestionCodes.Cholesterol,
        QuestionCodes.Hdl,
        QuestionCodes.SystolicBloodPressure,
        QuestionCodes.Egfr,
        QuestionCodes.HighBloodPressure,
        QuestionCodes.Smoking,
        QuestionCodes.Diabetes,
        QuestionCodes.Height,
        QuestionCodes.Weight
    ];

    public AscvdRiskInput Build(QuestionnaireAnswerReader reader)
    {
        reader.EnsureCodesPresent(RequiredCodes);

        var input = new AscvdRiskInput
        {
            Age = reader.GetInt(QuestionCodes.Age),
            Sex = reader.GetEnum(QuestionCodes.Gender, SexMap) ?? Sex.Male,
            Smoker = reader.GetYesNo(QuestionCodes.Smoking),
            Diabetic = reader.GetYesNo(QuestionCodes.Diabetes),
            SystolicBp = reader.GetDouble(QuestionCodes.SystolicBloodPressure),
            HdlCholesterol = reader.GetDouble(QuestionCodes.Hdl),
            TotalCholesterol = reader.GetDouble(QuestionCodes.Cholesterol),
            HypertensionTreated = reader.GetYesNo(QuestionCodes.HighBloodPressure),
            HeightCm = reader.GetFloat(QuestionCodes.Height),
            WeightKg = reader.GetFloat(QuestionCodes.Weight),
            Egfr = reader.GetFloat(QuestionCodes.Egfr),
            LipidLowering = reader.GetYesNo(QuestionCodes.HighLipid),
            HbA1c = reader.GetFloatOrNull(QuestionCodes.HbA1c),
            Uacr = reader.GetFloatOrNull(QuestionCodes.Uacr),
            SdiCategory = reader.GetEnum(QuestionCodes.WealthLevel, WealthCategory)
        };

        return input;
    }
}
