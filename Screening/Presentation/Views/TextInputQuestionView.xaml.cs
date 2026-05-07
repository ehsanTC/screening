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
            
        var page = FindAncestor<QuestionPage>(this);
        if (page?.DataContext is QuestionViewModel viewModel)
        {
            viewModel.Model.RefreshState(); 
        }
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
