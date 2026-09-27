using Screening.Domain.Parser.Models;
using Screening.Domain.Questionnaire;
using Screening.Domain.Questionnaire.Abstraction;
using Screening.Domain.RiskCalculator.Models;

namespace Screening.Domain.RiskCalculator.Fracture;

public class FractureRiskInputBuilder : IRiskInputBuilder<FractureRiskInput>
{
    private static readonly string[] RequiredCodes =
    [
        QuestionCodes.Age,
        QuestionCodes.Gender,
        QuestionCodes.Height,
        QuestionCodes.Weight,
    ];
    
    public FractureRiskInput Build(QuestionnaireAnswerReader reader)
    {
        reader.EnsureCodesPresent(RequiredCodes);

        var input = new FractureRiskInput()
        {
            Age = reader.GetInt(QuestionCodes.Age),
            Sex = reader.GetBinarySelection<Sex>(QuestionCodes.Gender),
            HeightCm = reader.GetFloat(QuestionCodes.Height) * 100,
            WeightKg = reader.GetInt(QuestionCodes.Weight),
            PreviousFracture = reader.GetBoolean(QuestionCodes.PreviousFracture) ?? false,
            ParentFracturedHip = reader.GetBoolean(QuestionCodes.ParentHipFracture) ?? false,
            Smoking = reader.GetBoolean(QuestionCodes.Smoking) ?? false,
            TakingCorticosteroids = reader.GetBoolean(QuestionCodes.Corticosteroids) ?? false,
            HaveRheumatoidArthritis = reader.GetBoolean(QuestionCodes.RheumatoidArthritis) ?? false,
            HaveSecondaryOsteoporosis =  reader.GetBoolean(QuestionCodes.SecondaryOsteoporosis) ?? false,
            DrinkExcessAlcohol = reader.GetBoolean(QuestionCodes.AlcoholOver3) ?? false,
            BmdValue = reader.GetFloatOrNull(QuestionCodes.Bmd), 
            BmdMeasure = GetMeasure(reader.GetSingleItem(QuestionCodes.FemoralNeckMeasure))
        };

        return input;
    }

    private static BmdMeasure? GetMeasure(YamlListItem? item)
    {
        if (item == null)
            return null;

        foreach (var measure in BmdMeasure.All)
        {
            if (string.Equals(measure.Name, item.Value, StringComparison.InvariantCultureIgnoreCase))
                return measure;
        }
        
        return null;
    }
}
