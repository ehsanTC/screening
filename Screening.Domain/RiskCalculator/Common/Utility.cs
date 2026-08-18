namespace Screening.Domain.RiskCalculator.Common;

public static class Utility
{
    public static double? Bmi(double heightCm, double weightKg)
    {
        if (heightCm <= 0)
            return null;
        
        return weightKg / Math.Pow(heightCm / 100.0, 2);
    }
}
