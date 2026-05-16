using Screening.Domain.Questions;

namespace Screening.Presentation.Views;

public sealed partial class TextInputQuestionView : UserControl
{
    public TextInputQuestionView() => InitializeComponent();

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is not TextBox textBox || DataContext is not TextInputDisplayableQuestion question)
        {
            return;
        }

        question.Response = textBox.Text;
        ShowValidationError(question);
            
        var page = FindAncestor<QuestionPage>(this);
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

    private static T? FindAncestor<T>(DependencyObject child) 
        where T : class, DependencyObject
    {
        var parent = VisualTreeHelper.GetParent(child);
        while (parent != null)
        {
            if (parent is T ancestor)
                return ancestor;
            parent = VisualTreeHelper.GetParent(parent);
        }

        return null;
    }
}
