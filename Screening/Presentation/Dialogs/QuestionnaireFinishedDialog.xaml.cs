
namespace Screening.Presentation.Dialogs;

public sealed partial class QuestionnaireFinishedDialog : ContentDialog
{
    public QuestionnaireFinishedDialog()
    {
        this.InitializeComponent();
        PrimaryButtonClick += OnPrimaryButtonClicked;
    }

    public async void OnPrimaryButtonClicked(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        try
        {
            var navigator = this.Navigator();
            if (navigator is not null)
            {
                await navigator.NavigateBackWithResultAsync(this, data: true);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Failed to navigate back with result: {ex}");
        }
    }

    private void ActionButton_Click(object sender, RoutedEventArgs e)
    {
        OnPrimaryButtonClicked(null, null);
    }
}

