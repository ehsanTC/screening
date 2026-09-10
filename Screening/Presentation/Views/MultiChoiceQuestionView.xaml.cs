using Screening.Domain.Answers;
using Screening.Domain.Parser.Models;
using Screening.Domain.Questions;
using Screening.Presentation.Utility;

namespace Screening.Presentation.Views;

public sealed partial class MultiChoiceQuestionView : UserControl
{
    private bool _isInitializing;

    public MultiChoiceQuestionView() => InitializeComponent();

    private void OnOptionLoaded(object sender, RoutedEventArgs e)
    {
        if (sender is not CheckBox checkBox ||
            checkBox.DataContext is not YamlListItem option ||
            DataContext is not MultiChoiceDisplayableQuestion question)
        {
            return;
        }

        _isInitializing = true;

        try
        {
            checkBox.IsChecked =
                question.Answer?.SelectedOptions.Contains(option) == true;
        }
        finally
        {
            _isInitializing = false;
        }
    }

    private async void OnOptionChecked(object sender, RoutedEventArgs e)
    {
        if (_isInitializing)
        {
            return;
        }

        if (sender is not CheckBox checkBox ||
            checkBox.DataContext is not YamlListItem option ||
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
            await vm.Model.UpdateAnswer(new MultiOptionAnswer(selectedOptions));
        }
    }
}
