using Screening.Domain.Questions;
using Screening.Presentation.Utility;

namespace Screening.Presentation.Views;

public sealed partial class TextInputQuestionView : UserControl
{
    public TextInputQuestionView() => InitializeComponent();

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is not TextBox textBox || 
            DataContext is not TextInputDisplayableQuestion question)
        {
            return;
        }

        question.Response = textBox.Text;
        ShowValidationError(question);
            
        var page = ControlUtility.FindAncestor<QuestionPage>(this);
        if (page?.DataContext is QuestionViewModel viewModel)
        {
            viewModel.Model.RefreshState(); 
        }
    }

    private void ShowValidationError(TextInputDisplayableQuestion question)
    {
        var error = question.IsNumeric &&
                    !string.IsNullOrWhiteSpace(question.Response) &&
                    !int.TryParse(question.Response, out _)
            ? "Enter a whole number."
            : string.Empty;

        ErrorTextBlock.Text = error;
        ErrorTextBlock.Visibility = string.IsNullOrEmpty(error)
            ? Visibility.Collapsed
            : Visibility.Visible;
    }
}
