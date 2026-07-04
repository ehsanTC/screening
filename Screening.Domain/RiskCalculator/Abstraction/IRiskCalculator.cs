namespace Screening.Domain.RiskCalculator.Abstraction;

public interface IRiskCalculator
{
    object? Calculate(ICalculatorArg arg);
}
