using Screening.Domain.Extensions;
using Screening.Domain.Models;
using Screening.Domain.Questionnaire;
using Screening.Domain.Questionnaire.Abstraction;
using Screening.Domain.Questions;
using Screening.Domain.RiskCalculator.Abstraction;
using Screening.Domain.RiskCalculator.Common;
using Screening.Domain.RiskCalculator.Models;

namespace Screening.Domain.RiskCalculator.Obesity;

public class ObesityRiskEvaluator(IRiskInputBuilder<ObesityRiskInput> inputBuilder) : IRiskEvaluator
{
    private const int MinAge = 20;

    private const double ObeseBmiThreshold = 30;
    private const double OverweightBmiThreshold = 25;

    private const double MaleWaistCutoffCm = 102;   
    private const double FemaleWaistCutoffCm = 88;  

    private readonly IRiskInputBuilder<ObesityRiskInput> _inputBuilder = inputBuilder 
        ?? throw new ArgumentNullException(nameof(inputBuilder));

    private int CalculateObesityLipidRisk(
        int age,
        Sex sex,
        double heightCm,
        double weightKg,
        double? waistCm,
        bool hasFamilyHistory,
        bool hasDiabetes,
        bool hasHighBloodPressure,
        bool isCurrentSmoker,
        bool isPastSmoker)
    {
        ValidateInputs(age, heightCm, weightKg);

        var bmi = Utility.Bmi(heightCm, weightKg);
        var isWaistHigh = IsWaistHigh(sex, waistCm);
        var riskFactorCount = CountRiskFactors(hasFamilyHistory, hasDiabetes, hasHighBloodPressure, isCurrentSmoker, isPastSmoker);

        return GetBmiPoints(bmi ?? 0) + (isWaistHigh ? 1 : 0) + riskFactorCount;
    }

    private static void ValidateInputs(int age, double heightCm, double weightKg)
    {
        if (age < MinAge)
            throw new ArgumentException($"This screening tool is for adults {MinAge}+", nameof(age));

        if (heightCm <= 0)
            throw new ArgumentException("Height must be positive", nameof(heightCm));

        if (weightKg <= 0)
            throw new ArgumentException("Weight must be positive", nameof(weightKg));
    }

    private static bool IsWaistHigh(Sex sex, double? waistCm)
    {
        if (waistCm is not { } waist)
            return false;

        var cutoff = sex == Sex.Male ? MaleWaistCutoffCm : FemaleWaistCutoffCm;
        return waist > cutoff;
    }

    private static int CountRiskFactors(
        bool hasFamilyHistory,
        bool hasDiabetes,
        bool hasHighBloodPressure,
        bool isCurrentSmoker,
        bool isPastSmoker)
    {
        var factors = new[] { hasFamilyHistory, hasDiabetes, hasHighBloodPressure, isCurrentSmoker, isPastSmoker };
        return factors.Count(f => f);
    }

    private static int GetBmiPoints(double bmi) => bmi switch
    {
        >= ObeseBmiThreshold => 2,
        >= OverweightBmiThreshold => 1,
        _ => 0
    };

    private static RiskSeverity GetSeverity(int score) => score switch
    {
        >= 6 => RiskSeverity.High,
        >= 4 => RiskSeverity.Medium,
        >= 2 => RiskSeverity.BorderLine,
        >= 0 => RiskSeverity.Low,
        _ => RiskSeverity.Unknown
    };

    private static RiskResult CreateResult(int score, QuestionnaireEntity questionnaire)
    {
        var severity = GetSeverity(score);

        var category = questionnaire.GetCategory(score);
        return new RiskResult(
            Score: score.ToString("F1"),
            Severity: severity,
            Summary: string.Format(category.Summary, score),
            Title: category.Text,
            SubTitle: category.Subtitle,
            RiskDescription: category.Description,
            Icon: category.Icon,
            Color: category.Color,
            PreventionSteps: category.Prevention,
            NextSteps: category.NextSteps,
            FollowUp: category.FollowUp,
            PatientEducation: category.PatientEducation
        );
    }

    public QuestionnaireType QuestionnaireType => QuestionnaireType.Obesity;

    public RiskResult Evaluate(QuestionnaireEntity questionnaire)
    {
        var reader = new QuestionnaireAnswerReader(questionnaire);
        var input = _inputBuilder.Build(reader);

        var score = CalculateObesityLipidRisk(
            input.Age,
            input.Sex,
            input.HeightCm,
            input.WeightKg,
            input.WaistCm,
            input.HasFamilyHistory,
            input.HasDiabetes,
            input.HasHighBloodPressure,
            input.IsCurrentSmoker,
            input.IsPastSmoker);

        return CreateResult(score, questionnaire);
    }
}
