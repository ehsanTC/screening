using Screening.Domain.RiskCalculator.Abstraction;

namespace Screening.Presentation.Dialogs;

public partial record ResultDialogModel
{
    private readonly RiskResult _risk;

    public ResultDialogModel(RiskResult risk)
    {
        _risk = risk;
    }

    public RiskResult Risk => _risk;
}
