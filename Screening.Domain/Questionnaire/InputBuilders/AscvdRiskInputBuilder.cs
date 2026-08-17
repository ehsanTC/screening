using Screening.Domain.Questionnaire.Abstraction;
using Screening.Domain.RiskCalculator.Models;
using Screening.Domain.RiskCalculator.Requests;

namespace Screening.Domain.Questionnaire.InputBuilders;

public class AscvdRiskInputBuilder : IRiskInputBuilder<AscvdRiskInput>
{
    private static readonly Dictionary<string, Sex> SexMap = new(StringComparer.OrdinalIgnoreCase)
    {
        ["Male"] = Sex.Male,
        ["Female"] = Sex.Female
    };

    private static readonly Dictionary<string, Race> RaceMap = new(StringComparer.OrdinalIgnoreCase)
    {
        ["White"] = Race.White,
        ["Black"] = Race.NoneWhite
    };

    private static readonly string[] RequiredCodes =
    [
        QuestionCodes.Age, 
        QuestionCodes.Gender,
        QuestionCodes.Race,
        QuestionCodes.Cholesterol, 
        QuestionCodes.Hdl,
        QuestionCodes.SystolicBloodPressure,
        QuestionCodes.HighBloodPressure,
        QuestionCodes.Smoking,
        QuestionCodes.Diabetes
    ];

    public AscvdRiskInput Build(QuestionnaireAnswerReader reader)
    {
        reader.EnsureCodesPresent(RequiredCodes);

        var ews = new AscvdRiskInput
        {
            Age =         reader.GetInt(QuestionCodes.Age),
            Sex =         reader.GetEnum(QuestionCodes.Gender, SexMap),
            Race =        reader.GetEnum(QuestionCodes.Race, RaceMap),
            HasDiabetes = reader.GetYesNo(QuestionCodes.Diabetes),
            SystolicBpMmHg = reader.GetDouble(QuestionCodes.SystolicBloodPressure),
            IsCurrentSmoker = reader.GetYesNo(QuestionCodes.Smoking),
            HdlCholesterolMgdl = reader.GetDouble(QuestionCodes.Hdl),
            TotalCholesterolMgdl = reader.GetDouble(QuestionCodes.Cholesterol),
            OnHypertensionTreatment = reader.GetYesNo(QuestionCodes.HighBloodPressure),
        };

        return ews;
    }
}
