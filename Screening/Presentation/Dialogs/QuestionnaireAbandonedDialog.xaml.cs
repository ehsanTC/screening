namespace Screening.Presentation.Dialogs;

public sealed partial class QuestionnaireAbandonedDialog : ContentDialog
{
    public QuestionnaireAbandonedDialog()
    {
        this.InitializeComponent();
        PrimaryButtonClick += OnPrimaryButtonClicked;
    }

    private async void OnPrimaryButtonClicked(ContentDialog sender, ContentDialogButtonClickEventArgs args)
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
}

