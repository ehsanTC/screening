using Screening.Domain.Questions;
using Screening.Presentation.Utility;

namespace Screening.Presentation.Views;

public sealed partial class MultiChoiceQuestionView : UserControl
{
    public MultiChoiceQuestionView() => InitializeComponent();

    private void OnOptionLoaded(object sender, RoutedEventArgs e)
    {
        if (sender is not CheckBox checkBox ||
            checkBox.DataContext is not string option ||
            DataContext is not MultiChoiceDisplayableQuestion question)
        {
            return;
        }

        // Check if the current option is already in the selected list
        checkBox.IsChecked = question.SelectedOptions.Contains(option);
    }

    private void OnOptionChecked(object sender, RoutedEventArgs e)
    {
        if (sender is not CheckBox checkBox ||
            checkBox.DataContext is not string option ||
            DataContext is not MultiChoiceDisplayableQuestion question)
        {
            return;
        }

        // Add or remove based on current state
        if (checkBox.IsChecked == true)
        {
            if (!question.SelectedOptions.Contains(option))
                question.SelectedOptions.Add(option);
        }
        else
        {
            question.SelectedOptions.Remove(option);
        }

        var page = ControlUtility.FindAncestor<QuestionPage>(this);
        if (page?.DataContext is QuestionViewModel viewModel)
        {
            viewModel.Model.RefreshState();
        }
    }
}

