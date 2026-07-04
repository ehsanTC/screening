using Screening.Domain.Models;
using Screening.Domain.RiskCalculator.Models;
using Screening.Domain.Services;
using Screening.Domain.Services.Abstraction;

namespace Screening.Presentation;

public partial record MainModel
{
    private readonly INavigator _navigator;
    private readonly IQuestionnaireProviderFactory _questionnaireProviderFactory;

    public MainModel(
        IStringLocalizer localizer,
        INavigator navigator,
        IQuestionnaireProviderFactory  questionnaireProviderFactory)
    {
        _navigator = navigator;
        Title += $"{localizer["ApplicationName"]}";
        _questionnaireProviderFactory = questionnaireProviderFactory;
    }

    public string? Title { get; }

    public IState<string> Name => State<string>.Value(this, () => string.Empty);
    
    public async Task GoToDiabetesQuestionnaire()
    {
        await _navigator.NavigateViewModelAsync<QuestionModel>(this, 
            data: new QuestionnairePageRequest(QuestionnaireType.Baseline));
    }
}
