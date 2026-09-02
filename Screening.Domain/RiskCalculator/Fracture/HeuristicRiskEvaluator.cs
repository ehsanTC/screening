using Screening.Domain.Extensions;
using Screening.Domain.Models;
using Screening.Domain.Questionnaire;
using Screening.Domain.Questionnaire.Abstraction;
using Screening.Domain.Questions;
using Screening.Domain.RiskCalculator.Abstraction;

namespace Screening.Domain.RiskCalculator.Fracture;

public class HeuristicRiskEvaluator(IRiskInputBuilder<FractureRiskInput> inputBuilder) : IRiskEvaluator
{
    public QuestionnaireType QuestionnaireType => QuestionnaireType.Fracture;
    public RiskResult Evaluate(QuestionnaireEntity questionnaire)
    {
        var reader = new QuestionnaireAnswerReader(questionnaire);
        var input = inputBuilder.Build(reader);

        var mofRisk = CalculateMofRiskLevel(input);
        var hipRisk = CalculateHipRiskLevel(input);

        var overallRisk = Max(mofRisk, hipRisk);

        var category = questionnaire.GetCategory(overallRisk);

        if (category is null)
        {
            throw new InvalidOperationException(
                $"No result category found for risk level '{overallRisk}'.");
        }

        return new RiskResult(
            Score: overallRisk.ToString(),
            Severity: category.GetSeverity(),
            Summary: string.Format(category.Summary, mofRisk.ToString(), hipRisk.ToString()),
            Title: category.Text,
            SubTitle: category.Subtitle,
            RiskDescription: category.Description,
            Icon: category.Icon,
            Color: category.Color,
            PreventionSteps: category.Prevention,
            NextSteps: category.NextSteps,
            FollowUp: category.FollowUp,
            PatientEducation: category.PatientEducation);
    }

    private static RiskSeverity Max(RiskSeverity r1, RiskSeverity r2) => r1 >= r2 ? r1 : r2;

    private static RiskSeverity CalculateMofRiskLevel(FractureRiskInput input)
    {
        var score = 0;
        score += input.Age switch
        {
            < 50 => 0,
            < 60 => 1,
            < 70 => 2,
            < 80 => 3,
            _ => 4
        };

        if (input.Bmi < 18.5)
            score += 2;

        if (input.PreviousFracture ?? false)
            score += 3;

        if (input.ParentFracturedHip ?? false)
            score += 2;

        if (input.TakingCorticosteroids ?? false)
            score += 2;

        if (input.HaveRheumatoidArthritis ?? false)
            score += 2;

        if (input.HaveSecondaryOsteoporosis ?? false)
            score += 2;

        if (input.Smoking ?? false)
            score += 1;

        if (input.DrinkExcessAlcohol ?? false)
            score += 1;

        return score switch
        {
            <= 2 => RiskSeverity.Low,
            <= 5 => RiskSeverity.Medium,
            _ => RiskSeverity.High
        };
    }
    
    private static RiskSeverity CalculateHipRiskLevel(FractureRiskInput input)
    {
        var score = 0;

        score += input.Age switch
        {
            < 50 => 0,
            < 60 => 2,
            < 70 => 4,
            < 80 => 6,
            _ => 8
        };

        if (input.Bmi < 18.5)
            score += 2;

        if (input.PreviousFracture ?? false)
            score += 3;

        if (input.ParentFracturedHip ?? false)
            score += 3;

        if (input.TakingCorticosteroids ?? false)
            score += 2;

        if (input.HaveRheumatoidArthritis ?? false)
            score += 2;

        if (input.HaveSecondaryOsteoporosis ?? false)
            score += 2;

        if (input.Smoking ?? false)
            score += 1;

        if (input.DrinkExcessAlcohol ?? false)
            score += 1;

        return score switch
        {
            <= 3 => RiskSeverity.Low,
            <= 7 => RiskSeverity.Medium,
            _ => RiskSeverity.High
        };
    }
}
