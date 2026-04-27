using RiskCalculator;
using RiskCalculator.Models;
using Xunit;

namespace Screening.Tests.Calculators;

public class Type2DiabetesRiskTests
{
    // Low-risk young adult with optimal factors
    [Fact]
    public void CalculateRisk_YoungActiveFemale_NoRiskFactors_ShouldReturnLowRisk_WhenAllAnswersOptimal()
    {
        var input = new DiabetesRiskInput
        {
            Age = 35,
            Sex = Sex.Female,
            HadGestationalDiabetes = false,
            HasFamilyHistory = false,
            HasHighBloodPressure = false,
            IsPhysicallyActive = true,
            HeightCm = 165,
            WeightKg = 55 // BMI ≈20.2 → 0 points
        };

        var result = CalculateType2DiabetesRisk(input);

        result.Score.Should().Be(0);
        result.RiskLevel.Should().Be("Low risk");
        result.Recommendation.Should().Contain("lower risk");
    }

    // Boundary case exactly at threshold
    [Fact]
    public void CalculateRisk_MiddleAgedMale_Overweight_ShouldReturnHighRisk_WhenScoreExactlyFive()
    {
        var input = new DiabetesRiskInput
        {
            Age = 45, // 1 point (40–49)
            Sex = Sex.Male, // 1 point
            HadGestationalDiabetes = null,
            HasFamilyHistory = true, // 1 point
            HasHighBloodPressure = true, // 1 point
            IsPhysicallyActive = true, // 0 points
            HeightCm = 175,
            WeightKg = 85 // BMI ≈27.8 → 1 point
        };

        var result = CalculateType2DiabetesRisk(input);

        result.Score.Should().Be(5);
        result.RiskLevel.Should().Be("High risk");
        result.Recommendation.Should().Contain("increased risk");
    }

    // High-risk older female with multiple factors
    [Fact]
    public void CalculateRisk_OlderFemale_MultipleRiskFactors_ShouldReturnHighRisk_WhenScoreWellAboveThreshold()
    {
        var input = new DiabetesRiskInput
        {
            Age = 62, // 3 points (60+)
            Sex = Sex.Female,
            HadGestationalDiabetes = true, // 1 point
            HasFamilyHistory = true, // 1 point
            HasHighBloodPressure = true, // 1 point
            IsPhysicallyActive = false, // 1 point
            HeightCm = 160,
            WeightKg = 90 // BMI ≈35.2 → 3 points
        };

        var result = CalculateType2DiabetesRisk(input);

        result.Score.Should().Be(10);
        result.RiskLevel.Should().Be("High risk");
        result.Recommendation.Should().Contain("increased risk");
    }

    // Male vs female difference (gestational diabetes ignored for male)
    [Fact]
    public void CalculateRisk_SameProfileDifferentGender_ShouldAddMalePoint_WhenComparingMaleAndFemale()
    {
        var maleInput = new DiabetesRiskInput
        {
            Age = 55, Sex = Sex.Male,
            HadGestationalDiabetes = null,
            HasFamilyHistory = false, HasHighBloodPressure = false,
            IsPhysicallyActive = true,
            HeightCm = 170, WeightKg = 70 // BMI ≈24.2 → 0 points
        };

        var femaleInput = maleInput with { Sex = Sex.Female };

        var maleResult = CalculateType2DiabetesRisk(maleInput);
        var femaleResult = CalculateType2DiabetesRisk(femaleInput);

        maleResult.Score.Should().Be(femaleResult.Score + 1); // Male gets +1 point
    }

    // Gestational diabetes only affects females
    [Fact]
    public void CalculateRisk_GestationalDiabetes_ShouldAddPointOnlyForFemale_WhenHistoryPresent()
    {
        var baseInput = new DiabetesRiskInput
        {
            Age = 45, Sex = Sex.Female,
            HadGestationalDiabetes = true,
            HasFamilyHistory = false, HasHighBloodPressure = false,
            IsPhysicallyActive = true,
            HeightCm = 165, WeightKg = 60 // BMI ≈22 → 0 points
        };

        var resultWithHistory = CalculateType2DiabetesRisk(baseInput);
        var resultWithoutHistory =
            CalculateType2DiabetesRisk(baseInput with { HadGestationalDiabetes = false });

        resultWithHistory.Score.Should().Be(resultWithoutHistory.Score + 1);
    }

    // BMI categories across ranges
    [Fact]
    public void CalculateRisk_VariousBMI_ShouldAssignCorrectPoints_WhenWeightChanges()
    {
        var baseInput = new DiabetesRiskInput { Age = 50, Sex = Sex.Male, HeightCm = 180 };

        // BMI <25 → 0 points
        var low = CalculateType2DiabetesRisk(baseInput with { WeightKg = 75 }); // BMI ≈23.1
        low.Score.Should().BeLessThan(5);

        // BMI 25–29.9 → 1 point
        var overweight = CalculateType2DiabetesRisk(baseInput with { WeightKg = 85 }); // BMI ≈26.2
        overweight.Score.Should().Be(low.Score + 1);

        // BMI 30–34.9 → 2 points
        var obese1 = CalculateType2DiabetesRisk(baseInput with { WeightKg = 100 }); // BMI ≈30.9
        obese1.Score.Should().Be(overweight.Score + 1);

        // BMI ≥35 → 3 points
        var obese2 = CalculateType2DiabetesRisk(baseInput with { WeightKg = 115 }); // BMI ≈35.5
        obese2.Score.Should().Be(obese1.Score + 1);
    }
    
    private static DiabetesRiskResult CalculateType2DiabetesRisk(DiabetesRiskInput input) =>
        DiabetesRiskCalculator.CalculateType2DiabetesRisk(input.Age, input.Sex, input.HadGestationalDiabetes,
            input.HasFamilyHistory,  input.HasHighBloodPressure, input.IsPhysicallyActive, input.HeightCm, input.WeightKg);
}

// Minimal input structure for test readability
public record DiabetesRiskInput
{
    public int Age { get; init; }
    public Sex Sex { get; init; }
    public bool? HadGestationalDiabetes { get; init; }
    public bool HasFamilyHistory { get; init; }
    public bool HasHighBloodPressure { get; init; }
    public bool IsPhysicallyActive { get; init; }
    public double HeightCm { get; init; }
    public double WeightKg { get; init; }
}
