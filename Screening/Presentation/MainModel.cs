namespace Screening.Presentation;

public partial record MainModel
{
    private INavigator _navigator;

    public MainModel(
        IStringLocalizer localizer,
        IOptions<AppConfig> appInfo,
        INavigator navigator)
    {
        _navigator = navigator;
        Title = "Main";
        Title += $" - {localizer["ApplicationName"]}";
        Title += $" - {appInfo?.Value?.Environment}";
    }

    public string? Title { get; }

    public IState<string> Name => State<string>.Value(this, () => string.Empty);

    public Task GoToSecond1()
    {
        return GoToSecond("Diabetes type 2");
    }

    public Task GoToSecond2()
    {
        return GoToSecond("Cardiovascular");
    }

    private async Task GoToSecond(string screeningType)
    {
        await _navigator.NavigateViewModelAsync<SecondModel>(this, data: new Entity(screeningType));
    }
}
