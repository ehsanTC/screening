namespace Screening.Presentation;

public partial record MainModel
{
    private INavigator _navigator;

    public MainModel(
        IStringLocalizer localizer,
        INavigator navigator)
    {
        _navigator = navigator;
        Title += $"{localizer["ApplicationName"]}";
    }

    public string? Title { get; }

    public IState<string> Name => State<string>.Value(this, () => string.Empty);
    
    public async Task GoToQuestions()
    {
        await _navigator.NavigateViewModelAsync<QuestionModel>(this, data: null);
    }
}
