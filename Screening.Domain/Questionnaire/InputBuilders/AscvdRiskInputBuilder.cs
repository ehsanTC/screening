using Screening.Domain.Questionnaire.Abstraction;
using Screening.Domain.RiskCalculator.Models;
using Screening.Domain.RiskCalculator.Requests;

namespace Screening.Domain.Questionnaire.InputBuilders;

public class AscvdRiskInputBuilder : IRiskInputBuilder<AscvdRiskInput>
{
    private static readonly Dictionary<string, Sex> SexMap = new()
    {
        ["Male"] = Sex.Male,
        ["Female"] = Sex.Female
    };

    private static readonly Dictionary<string, Race> RaceMap = new()
    {
        ["White"] = Race.White,
        ["African American / Black"] = Race.NoneWhite
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

        return new AscvdRiskInput
        {
            Age = reader.GetInt(QuestionCodes.Age),
            Sex = reader.GetEnum(QuestionCodes.Gender, SexMap),
            Race = reader.GetEnum(QuestionCodes.Race, RaceMap),
            TotalCholesterolMgdl = reader.GetDouble(QuestionCodes.Cholesterol),
            HdlCholesterolMgdl = reader.GetDouble(QuestionCodes.Hdl),
            SystolicBpMmHg = reader.GetDouble(QuestionCodes.SystolicBloodPressure),
            OnHypertensionTreatment = reader.GetYesNo(QuestionCodes.HighBloodPressure),
            IsCurrentSmoker = reader.GetYesNo(QuestionCodes.Smoking),
            HasDiabetes = reader.GetYesNo(QuestionCodes.Diabetes)
        };
    }
}
