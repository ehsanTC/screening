using Screening.Domain.Models;
using Screening.Domain.RiskCalculator.Abstraction;

namespace Screening.Presentation;

public partial record MainModel
{
    private readonly INavigator _navigator;

    public MainModel(
        IStringLocalizer localizer,
        INavigator navigator)
    {
        _navigator = navigator;
        Title += $"{localizer["ApplicationName"]}";
    }

    public string? Title { get; }

    public IState<string> Name => State<string>.Value(this, () => string.Empty);
    
    public async Task GoToDiabetesQuestionnaire()
    {
        var result = await _navigator.NavigateViewModelForResultAsync<QuestionModel, RiskResult>(this, 
            data: new QuestionnairePageRequest(QuestionnaireType.Diabetes,
                "Next", "Calculate"));

        if (result is { Success: true })
        {
            var r = result.Result.Result.SomeOrDefault();
            Console.WriteLine(r.Score + "  " + r.Message);
        }
    }
    public async Task GoToBaseline()
    {
        await _navigator.NavigateViewModelAsync<QuestionModel>(this, 
            data: new QuestionnairePageRequest(QuestionnaireType.Baseline,
                "Next", "Save"));
    }
}
