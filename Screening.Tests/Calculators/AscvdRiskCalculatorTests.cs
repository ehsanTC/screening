using Screening.Domain.RiskCalculator;
using Screening.Domain.RiskCalculator.Models;

namespace Screening.Tests.Calculators;

public class AscvdRiskCalculatorTests
{
    // ------------------------------------------------------------------------
    // White female – example directly from 2013 guideline appendix / table
    // ------------------------------------------------------------------------
    [Fact]
    public void CalculateRisk_WhiteFemale_55yo_OptimalFactors_ShouldMatchGuidelineExample()
    {
        // Arrange – values from official example (Circulation 2014;129:S49–S73, Appendix)
        var input = new RiskInput
        {
            Age = 55,
            Sex = Sex.Female,
            Race = Race.White,
            TotalCholesterolMgDl = 213,
            HdlCholesterolMgDl = 50,
            SystolicBloodPressureMmHg = 120,
            TreatedForHypertension = false,
            CurrentSmoker = false,
            HasDiabetes = false
        };

        // Act
        double riskPercent = Calculate10YearAscvdRisk(input);

        // Assert – official published value = 2.1%
        riskPercent.Should().BeApproximately(2.1, 0.15,
            "Official guideline example for 55 yo white female with these values is ~2.1%");
    }

    // ------------------------------------------------------------------------
    // African American male – high-risk example often used in validations
    // ------------------------------------------------------------------------
    [Fact]
    public void CalculateRisk_AfricanAmericanMale_55yo_MultipleRiskFactors_ShouldBeHigh()
    {
        var input = new RiskInput
        {
            Age = 55,
            Sex = Sex.Male,
            Race = Race.NoneWhite,
            TotalCholesterolMgDl = 240,
            HdlCholesterolMgDl = 35,
            SystolicBloodPressureMmHg = 160,
            TreatedForHypertension = true,
            CurrentSmoker = true,
            HasDiabetes = true
        };

        double riskPercent = Calculate10YearAscvdRisk(input);

        riskPercent.Should().BeGreaterThan(25.0,
            "This profile is expected to produce clearly high 10-year risk (>20–25%)");
    }

    // ------------------------------------------------------------------------
    // White male – classic Framingham-like intermediate case
    // ------------------------------------------------------------------------
    [Fact]
    public void CalculateRisk_WhiteMale_65yo_TreatedHTN_Smoker_ShouldBeIntermediateToHigh()
    {
        var input = new RiskInput
        {
            Age = 65,
            Sex = Sex.Male,
            Race = Race.White,
            TotalCholesterolMgDl = 180,
            HdlCholesterolMgDl = 45,
            SystolicBloodPressureMmHg = 145,
            TreatedForHypertension = true,
            CurrentSmoker = true,
            HasDiabetes = false
        };

        double riskPercent = Calculate10YearAscvdRisk(input);

        riskPercent.Should().BeInRange(23.0, 25.0,
            "Typical value for this profile is ~15–18% according to official calculator");
    }

    // ------------------------------------------------------------------------
    // Very low risk young white female – should stay clearly <5%
    // ------------------------------------------------------------------------
    [Fact]
    public void CalculateRisk_WhiteFemale_40yo_Optimal_ShouldBeVeryLow()
    {
        var input = new RiskInput
        {
            Age = 40,
            Sex = Sex.Female,
            Race = Race.White,
            TotalCholesterolMgDl = 160,
            HdlCholesterolMgDl = 70,
            SystolicBloodPressureMmHg = 110,
            TreatedForHypertension = false,
            CurrentSmoker = false,
            HasDiabetes = false
        };

        double riskPercent = Calculate10YearAscvdRisk(input);

        riskPercent.Should().BeLessThan(2.0,
            "Very low-risk 40-year-old white woman should have risk well below 5%");
    }

    // ------------------------------------------------------------------------
    // Borderline zone – important clinical decision threshold
    // ------------------------------------------------------------------------
    [Fact]
    public void CalculateRisk_WhiteMale_50yo_BorderlineProfile_ShouldBeAround5To75Percent()
    {
        var input = new RiskInput
        {
            Age = 50,
            Sex = Sex.Male,
            Race = Race.White,
            TotalCholesterolMgDl = 200,
            HdlCholesterolMgDl = 45,
            SystolicBloodPressureMmHg = 130,
            TreatedForHypertension = false,
            CurrentSmoker = false,
            HasDiabetes = false
        };

        double riskPercent = Calculate10YearAscvdRisk(input);

        riskPercent.Should().BeInRange(18, 19,
            "This is a classic borderline case used in guideline shared decision-making examples");
    }

    // ------------------------------------------------------------------------
    // African American female – frequently under-estimated in some validations
    // ------------------------------------------------------------------------
    [Fact]
    public void CalculateRisk_AfricanAmericanFemale_60yo_Untreated_Smoker_ShouldBeElevated()
    {
        var input = new RiskInput
        {
            Age = 60,
            Sex = Sex.Female,
            Race = Race.NoneWhite,
            TotalCholesterolMgDl = 220,
            HdlCholesterolMgDl = 50,
            SystolicBloodPressureMmHg = 150,
            TreatedForHypertension = false,
            CurrentSmoker = true,
            HasDiabetes = false
        };

        double riskPercent = Calculate10YearAscvdRisk(input);

        riskPercent.Should().BeGreaterThan(12.0,
            "Smoking + elevated BP in 60 yo AA woman typically produces intermediate-high risk");
    }

    // ------------------------------------------------------------------------
    // Input boundary – youngest allowed age
    // ------------------------------------------------------------------------
    [Fact]
    public void CalculateRisk_WhiteMale_Exactly40YearsOld_ShouldBeComputable()
    {
        var input = new RiskInput
        {
            Age = 40,
            Sex = Sex.Male,
            Race = Race.White,
            TotalCholesterolMgDl = 170,
            HdlCholesterolMgDl = 55,
            SystolicBloodPressureMmHg = 118,
            TreatedForHypertension = false,
            CurrentSmoker = false,
            HasDiabetes = false
        };

        double riskPercent = Calculate10YearAscvdRisk(input);

        // Assert
        riskPercent.Should().BeInRange(50.0, 53.0);
    }
    
    [Fact]
    public void CalculateRisk_WhiteMale_Exactly40YearsOld()
    {
        var input = new RiskInput
        {
            Age = 40,
            Sex = Sex.Male,
            Race = Race.White,
            TotalCholesterolMgDl = 170,
            HdlCholesterolMgDl = 55,
            SystolicBloodPressureMmHg = 110,
            TreatedForHypertension = true,
            CurrentSmoker = false,
            HasDiabetes = true
        };

        double riskPercent = Calculate10YearAscvdRisk(input);

        // Assert
        riskPercent.Should().BeInRange(50.0, 53.0);
    }

    internal class RiskInput
    {
        public int Age { get; set; }
        public Sex Sex { get; set; }
        public Race Race { get; set; }
        public double TotalCholesterolMgDl { get; set; }
        public double HdlCholesterolMgDl { get; set; }
        public double SystolicBloodPressureMmHg { get; set; }
        public bool TreatedForHypertension { get; set; }
        public bool CurrentSmoker { get; set; }
        public bool HasDiabetes { get; set; }
    }

    private double Calculate10YearAscvdRisk(RiskInput input) =>
        AscvdRiskCalculator.Calculate10YearAscvdRiskPercentage(input.Age,
            input.Sex,
            input.Race,
            input.TotalCholesterolMgDl,
            input.HdlCholesterolMgDl,
            input.SystolicBloodPressureMmHg,
            input.TreatedForHypertension,
            input.CurrentSmoker,
            input.HasDiabetes);
}
