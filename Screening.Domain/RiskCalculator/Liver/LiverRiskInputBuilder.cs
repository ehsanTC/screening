using Screening.Domain.Questionnaire;
using Screening.Domain.Questionnaire.Abstraction;
using Screening.Domain.RiskCalculator.Models;

namespace Screening.Domain.RiskCalculator.Liver;

public class LiverRiskInputBuilder : IRiskInputBuilder<LiverRiskInput>
{
    private static readonly string[] Codes =
    [
        QuestionCodes.Age,
        QuestionCodes.Gender,
        QuestionCodes.Height,
        QuestionCodes.Weight,
        QuestionCodes.Diabetes,
        QuestionCodes.MetabolicSyndrome,
    ];

    public LiverRiskInput Build(QuestionnaireAnswerReader reader)
    {
        reader.EnsureCodesPresent(Codes);

        return new LiverRiskInput()
        {
            Age = reader.GetInt(QuestionCodes.Age),
            Gender = reader.GetBinarySelection<Sex>(QuestionCodes.Gender),
            HeightCm = reader.GetFloat(QuestionCodes.Height) * 100,
            WeightKg = reader.GetInt(QuestionCodes.Weight),
            HaveDiabetes = reader.GetBoolean(QuestionCodes.Diabetes) ?? false,
            HaveMetabolicSyndrome = reader.GetBoolean(QuestionCodes.MetabolicSyndrome) ?? false,
            Alt = reader.GetIntOrNull(QuestionCodes.Alt),
            Ast = reader.GetIntOrNull(QuestionCodes.Ast),
            PlateletCount = reader.GetIntOrNull(QuestionCodes.PlateletCount),
            Albumin = reader.GetDoubleOrNull(QuestionCodes.Albumin),
            UltrasonicFattyLiver = reader.GetBoolean(QuestionCodes.UltrasonicFattyLiver)
        };
    }
}
