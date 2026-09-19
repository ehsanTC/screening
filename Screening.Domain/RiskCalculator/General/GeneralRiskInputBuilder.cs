using Screening.Domain.Parser.Models;
using Screening.Domain.Questionnaire;
using Screening.Domain.Questionnaire.Abstraction;

namespace Screening.Domain.RiskCalculator.General;

public class GeneralRiskInputBuilder : IRiskInputBuilder<GeneralRiskInput>
{
    public GeneralRiskInput Build(QuestionnaireAnswerReader reader)
    {
        ArgumentNullException.ThrowIfNull(reader);

        return new GeneralRiskInput()
        {
            HaveThyroid = GetDisordersValue(QuestionCodes.ThyroidDisorder),
            HaveVitaminDiDeficiency = GetDisordersValue(QuestionCodes.VitaminDiDeficiency),
            HaveDepressionAndAnxiety = GetDisordersValue(QuestionCodes.HaveDepressionAndAnxiety),
            
            BreastCancer     = GetCancerValue(QuestionCodes.BreastCancer),
            ColorectalCancer = GetCancerValue(QuestionCodes.ColorectalCancer),
            StomachCancer    = GetCancerValue(QuestionCodes.StomachCancer),
            SkinCancer       = GetCancerValue(QuestionCodes.SkinCancer),
            LungCancer       = GetCancerValue(QuestionCodes.LungCancer),
            ProstateCancer   = GetCancerValue(QuestionCodes.ProstateCancer),
            LiverCancer      = GetCancerValue(QuestionCodes.LiverCancer)
        };
        
        bool GetDisordersValue(string code) => ContainsCode(reader.GetMultipleItem(QuestionCodes.Disorders), code);
        bool GetCancerValue(string cancerCode) => ContainsCode(reader.GetMultipleItem(QuestionCodes.Cancers), cancerCode);
    }

    private bool ContainsCode(IEnumerable<YamlListItem> items, string code) =>
        items.Any(item => 
            string.Equals(item.Value, code, StringComparison.InvariantCultureIgnoreCase));
}
