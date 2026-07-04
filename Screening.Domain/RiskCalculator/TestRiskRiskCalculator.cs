using Screening.Domain.RiskCalculator.Abstraction;

namespace Screening.Domain.RiskCalculator;

public class TestRiskRiskCalculator : IRiskCalculator
{
    public object? Calculate(ICalculatorArg arg)
    {
        return 10;
    }
}
