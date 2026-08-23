using Screening.Domain.Extensions;
using Screening.Domain.Models;
using Screening.Domain.Questionnaire;
using Screening.Domain.Questionnaire.Abstraction;
using Screening.Domain.Questions;
using Screening.Domain.RiskCalculator.Abstraction;
using Screening.Domain.RiskCalculator.Common;
using Screening.Domain.RiskCalculator.Models;
using Screening.Domain.RiskCalculator.Requests;

namespace Screening.Domain.RiskCalculator;

public class DiabetesRiskEvaluator : IRiskEvaluator
{
    private const int MinAge = 18;
    private const int HighRiskThreshold = 5;

    private readonly IRiskInputBuilder<DiabetesRiskInput>  _inputBuilder;

    public DiabetesRiskEvaluator(IRiskInputBuilder<DiabetesRiskInput> inputBuilder)
    {
        _inputBuilder = inputBuilder ?? throw new ArgumentNullException(nameof(inputBuilder));
    }

    /// <summary>
    /// Calculates the risk score for Type 2 diabetes using the American Diabetes Association (ADA) 60-Second Risk Test.
    /// This is a screening tool based on a simple point-based system, not a diagnostic test.
    /// High risk indicates need for formal testing (e.g., A1C, fasting glucose).
    /// </summary>
    private int CalculateType2DiabetesRisk(
        int age,
        Sex sex,
        bool? hadGestationalDiabetes,
        bool hasFamilyHistory,
        bool hasHighBloodPressure,
        bool isPhysicallyActive,
        double heightCm,
        double weightKg)
    {
        ValidateInputs(age, heightCm, weightKg);

        int score = 0;
        score += GetAgePoints(age);
        score += GetSexPoints(sex);
        score += GetGestationalDiabetesPoints(sex, hadGestationalDiabetes);
        score += hasFamilyHistory ? 1 : 0;
        score += hasHighBloodPressure ? 1 : 0;
        score += isPhysicallyActive ? 0 : 1;
        score += GetBmiPoints(heightCm, weightKg);

        return score;
    }

    private static void ValidateInputs(int age, double heightCm, double weightKg)
    {
        if (age < MinAge)
            throw new ArgumentException($"This risk test is for adults {MinAge}+", nameof(age));

        if (heightCm <= 0)
            throw new ArgumentException("Height must be positive", nameof(heightCm));

        if (weightKg <= 0)
            throw new ArgumentException("Weight must be positive", nameof(weightKg));
    }

    private static int GetAgePoints(int age) => age switch
        {
            >= 60 => 3,
            >= 50 => 2,
            >= 40 => 1,
            _ => 0
        };

    private static int GetSexPoints(Sex sex) => sex == Sex.Male ? 1 : 0;

    private static int GetGestationalDiabetesPoints(Sex sex, bool? hadGestationalDiabetes) => 
        sex == Sex.Female && hadGestationalDiabetes == true ? 1 : 0;

    private static int GetBmiPoints(double heightCm, double weightKg)
    {
        var bmi = Utility.Bmi(heightCm, weightKg);

        return bmi switch
        {
            >= 35 => 3,
            >= 30 => 2,
            >= 25 => 1,
            _ => 0
        };
    }

    private static RiskResult CreateResult(int score, QuestionnaireEntity questionnaire)
    {
        bool isHighRisk = score >= HighRiskThreshold;

        return new RiskResult(
            Score: score.ToString("F1"),
            Severity: isHighRisk ? RiskSeverity.High : RiskSeverity.Low,
            Message:  questionnaire.GetMessage(score)
        );
    }
    public QuestionnaireType QuestionnaireType => QuestionnaireType.Diabetes;
    public RiskResult Evaluate(QuestionnaireEntity questionnaire)
    {
        var reader = new QuestionnaireAnswerReader(questionnaire);
        var input = _inputBuilder.Build(reader);
        
        var score = CalculateType2DiabetesRisk(input.Age,
            input.Sex,
            input.HadGestationalDiabetes,
            input.HasFamilyHistory,
            input.HasHighBloodPressure,
            input.IsPhysicallyActive,
            input.HeightCm,
            input.WeightKg);

        return CreateResult(score, questionnaire);
    }
}
