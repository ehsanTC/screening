using Screening.Domain.Answers;
using Screening.Domain.Parser.Models;
using Screening.Domain.Questions;
using Screening.Presentation.Utility;

namespace Screening.Presentation.Views;

public sealed partial class MultiChoiceQuestionView : UserControl
{
    public MultiChoiceQuestionView() => InitializeComponent();

    private void OnOptionLoaded(object sender, RoutedEventArgs e)
    {
        if (sender is not CheckBox checkBox ||
            checkBox.DataContext is not YamlListItem option ||
            DataContext is not MultiChoiceDisplayableQuestion question)
        {
            return;
        }

        checkBox.IsChecked = question.Answer?.SelectedOptions
            .Any(so => so.Equals(option));
    }

    private void OnOptionChecked(object sender, RoutedEventArgs e)
    {
        if (sender is not CheckBox checkBox ||
            checkBox.DataContext is not YamlListItem option ||
            DataContext is not MultiChoiceDisplayableQuestion question)
        {
            return;
        }

        var selectedOptions = question.Answer?.SelectedOptions.ToList() ?? [];

        if (checkBox.IsChecked == true)
        {
            if (!selectedOptions.Any(so => so.Text.Contains(option.Text)))
            {
                selectedOptions.Add(option);
            }
        }
        else
        {
            // TODO: Needs to check 
            selectedOptions.Remove(option);
        }

        var page = ControlUtility.FindAncestor<QuestionPage>(this);
        if (page?.DataContext is QuestionViewModel vm)
        {
            vm.Model.UpdateAnswer(new MultiOptionAnswer(selectedOptions));
        }
    }
}
