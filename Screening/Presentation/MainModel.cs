using Screening.Domain.Models;
using Screening.Domain.RiskCalculator.Abstraction;

namespace Screening.Presentation;

public partial record MainModel
{
    private readonly IStringLocalizer _localizer;
    private readonly INavigator _navigator;
    
    public string? Title { get; }

    public MainModel(IStringLocalizer localizer, INavigator navigator)
    {
        _localizer = localizer;
        _navigator = navigator;
        Title += $"{localizer["ApplicationName"]}";
    }

    public IState<string> Name => State<string>.Value(this, () => string.Empty);

    public async Task GoToDiabetes() => await ShowQuestionnaire(QuestionnaireType.Diabetes);
    public async Task GoToAscvd() => await ShowQuestionnaire(QuestionnaireType.Ascvd);
    public async Task GoToFracture() => await ShowQuestionnaire(QuestionnaireType.Fracture);
}
