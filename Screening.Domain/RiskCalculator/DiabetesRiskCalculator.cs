using Screening.Domain.RiskCalculator.Common;
using Screening.Domain.RiskCalculator.Models;

namespace Screening.Domain.RiskCalculator;

public static class DiabetesRiskCalculator
{
    private const int MinAge = 18;
    private const int HighRiskThreshold = 5;

    /// <summary>
    /// Calculates the risk score for Type 2 diabetes using the American Diabetes Association (ADA) 60-Second Risk Test.
    /// This is a screening tool based on a simple point-based system, not a diagnostic test.
    /// High risk indicates need for formal testing (e.g., A1C, fasting glucose).
    /// </summary>
    public static DiabetesRiskResult CalculateType2DiabetesRisk(
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

        int score = CalculateRiskScore(
            age,
            sex,
            hadGestationalDiabetes,
            hasFamilyHistory,
            hasHighBloodPressure,
            isPhysicallyActive,
            heightCm,
            weightKg);

        return CreateResult(score);
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

    private static int CalculateRiskScore(
        int age,
        Sex sex,
        bool? hadGestationalDiabetes,
        bool hasFamilyHistory,
        bool hasHighBloodPressure,
        bool isPhysicallyActive,
        double heightCm,
        double weightKg)
    {
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
        double bmi = Utility.Bmi(heightCm, weightKg);

        return bmi switch
        {
            >= 35 => 3,
            >= 30 => 2,
            >= 25 => 1,
            _ => 0
        };
    }

    private static DiabetesRiskResult CreateResult(int score)
    {
        bool isHighRisk = score >= HighRiskThreshold;

        return new DiabetesRiskResult(
            Score: score,
            RiskLevel: isHighRisk ? "High risk" : "Low risk",
            Recommendation: isHighRisk
                ? "You are at increased risk for having prediabetes or type 2 diabetes. Talk to your doctor about getting tested (e.g., A1C or fasting blood glucose). Lifestyle changes can help prevent or delay type 2 diabetes."
                : "You have a lower risk for type 2 diabetes at this time. Maintain a healthy lifestyle to keep your risk low.");
    }
}
