using Screening.Domain.RiskCalculator.Models;

namespace Screening.Domain.RiskCalculator;

public static class AscvdRiskCalculator
{
    public static double Calculate10YearAscvdRiskPercentage(
        int age,
        Sex sex,
        Race race,
        double totalCholesterol_mgdl,
        double hdlCholesterol_mgdl,
        double systolicBP_mmHg,
        bool onHypertensionTreatment,
        bool isCurrentSmoker,
        bool hasDiabetes)
    {
        if (age is < 40 or > 79)
            throw new ArgumentException("Age must be 40–79 years");

        double lnAge = Math.Log(age);
        double lnTC = Math.Log(totalCholesterol_mgdl);
        double lnHDL = Math.Log(hdlCholesterol_mgdl);
        double lnSBP = Math.Log(systolicBP_mmHg);

        double individualSum = 0.0;
        double meanSum = 0.0;
        double baselineSurvival = 0.0;

        bool isAfricanAmerican = race == Race.NoneWhite;

        if (sex == Sex.Male)
        {
            // White and African American males use identical variable coefficients
            individualSum =
                12.344 * lnAge +
                11.853 * lnTC -
                2.664 * lnAge * lnTC -
                7.990 * lnHDL +
                1.769 * lnAge * lnHDL +
                (onHypertensionTreatment ? 1.797 : 13.872) * lnSBP +
                (onHypertensionTreatment ? 0 : -3.010) * lnAge * lnSBP +
                7.837 * (isCurrentSmoker ? 1 : 0) -
                1.795 * (isCurrentSmoker ? lnAge : 0) +
                0.658 * (hasDiabetes ? 1 : 0);

            meanSum = isAfricanAmerican ? 61.7646 : 61.18;
            baselineSurvival = isAfricanAmerican ? 0.8954 : 0.9144;
        }
        else // Female
        {
            if (isAfricanAmerican)
            {
                individualSum =
                    17.114 * lnAge +
                    0.940 * lnTC -
                    18.920 * lnHDL +
                    4.475 * lnAge * lnHDL +
                    (onHypertensionTreatment ? 29.291 * lnSBP - 6.432 * lnAge * lnSBP :
                                               27.820 * lnSBP - 6.087 * lnAge * lnSBP) +
                    0.691 * (isCurrentSmoker ? 1 : 0) +
                    0.874 * (hasDiabetes ? 1 : 0);

                meanSum = 86.61;
                baselineSurvival = 0.9533;
            }
            else // White female
            {
                double lnAgeSquared = lnAge * lnAge;

                individualSum =
                    -29.799 * lnAge +
                    4.884 * lnAgeSquared +
                    13.540 * lnTC -
                    3.114 * lnAge * lnTC -
                    13.578 * lnHDL +
                    3.149 * lnAge * lnHDL +
                    (onHypertensionTreatment ? 2.019 : 1.957) * lnSBP +
                    7.574 * (isCurrentSmoker ? 1 : 0) -
                    1.665 * (isCurrentSmoker ? lnAge : 0) +
                    0.661 * (hasDiabetes ? 1 : 0);

                meanSum = -29.18;
                baselineSurvival = 0.9665;
            }
        }

        double exponent = individualSum - meanSum;
        double riskFraction = 1.0 - Math.Pow(baselineSurvival, Math.Exp(exponent));
        double riskPercentage = riskFraction * 100.0;

        return Math.Max(0.1, Math.Min(99.9, riskPercentage));
    }
}
