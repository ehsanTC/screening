namespace Screening.Domain.RiskCalculator.Common;

public static class Utility
{
    public static double Bmi(double heightCm, double weightKg) => 
        weightKg / Math.Pow(heightCm / 100.0, 2);
    
}
