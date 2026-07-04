namespace Screening.Presentation;

public sealed partial class QuestionPage : Page
{
    private CancellationTokenSource? _toastCancellation;

    public QuestionPage()
    {
        this.InitializeComponent();
    }

    private async void ShowToast(string message)
    {
        _toastCancellation?.Cancel();
        _toastCancellation = new CancellationTokenSource();
        var token = _toastCancellation.Token;

        ValidationToastText.Text = message;
        ValidationToast.Visibility = Visibility.Visible;

        try
        {
            await Task.Delay(TimeSpan.FromSeconds(3), token);
            ValidationToast.Visibility = Visibility.Collapsed;
        }
        catch (TaskCanceledException)
        {
            // A newer toast replaced this one.
        }
    }
}
