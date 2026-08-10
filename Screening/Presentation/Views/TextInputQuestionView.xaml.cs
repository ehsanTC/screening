using System.Globalization;
using Screening.Domain.Answers;
using Screening.Domain.Questions;
using Screening.Presentation.Utility;
using Windows.ApplicationModel.Resources;

namespace Screening.Presentation.Views;

public sealed partial class TextInputQuestionView : UserControl
{
    private static readonly ResourceLoader ResourceLoader =
        ResourceLoader.GetForViewIndependentUse();
    
    public TextInputQuestionView() => InitializeComponent();

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is not TextBox textBox || 
            DataContext is not TextInputDisplayableQuestion question)
        {
            return;
        }

        var answer = new TextAnswer(textBox.Text);
        ShowValidationError(question, answer);
            
        var page = ControlUtility.FindAncestor<QuestionPage>(this);
        if (page?.DataContext is QuestionViewModel viewModel)
        {
            viewModel.Model.UpdateAnswer(answer);
        }
    }

    private void ShowValidationError(TextInputDisplayableQuestion question, TextAnswer answer)
    {
        var error = question.IsNumeric &&
                    !string.IsNullOrWhiteSpace(answer.Text) &&
                    !double.TryParse(answer.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out _)
            ? ResourceLoader.GetString("TextInputQuestionView_NotANumber")
            : string.Empty;

        ErrorTextBlock.Text = error;
        ErrorTextBlock.Visibility = string.IsNullOrEmpty(error)
            ? Visibility.Collapsed
            : Visibility.Visible;
    }
}
