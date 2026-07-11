using Screening.Domain.Answers;
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

        checkBox.IsChecked = question.Answer?.SelectedOptions.Contains(option) == true;
    }

    private void OnOptionChecked(object sender, RoutedEventArgs e)
    {
        if (sender is not CheckBox checkBox ||
            checkBox.DataContext is not string option ||
            DataContext is not MultiChoiceDisplayableQuestion question)
        {
            return;
        }

        var selectedOptions = question.Answer?.SelectedOptions.ToList() ?? [];

        if (checkBox.IsChecked == true)
        {
            if (!selectedOptions.Contains(option))
            {
                selectedOptions.Add(option);
            }
        }
        else
        {
            selectedOptions.Remove(option);
        }

        var page = ControlUtility.FindAncestor<QuestionPage>(this);
        if (page?.DataContext is QuestionViewModel vm)
        {
            vm.Model.UpdateAnswer(new MultipleChoiceAnswer(selectedOptions));
        }
    }
}
