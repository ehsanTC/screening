using Screening.Domain.Answers;
using Screening.Domain.Questions;
using Screening.Presentation.Utility;

namespace Screening.Presentation.Views;

public sealed partial class SingleChoiceQuestionView : UserControl
{
    public SingleChoiceQuestionView() => InitializeComponent();

    private void OnOptionLoaded(object sender, RoutedEventArgs e)
    {
        if (sender is not RadioButton radioButton ||
            radioButton.DataContext is not string option ||
            DataContext is not SingleChoiceDisplayableQuestion question)
        {
            return;
        }

        radioButton.IsChecked = 
            option.Equals(question?.Answer?.SelectedOption, StringComparison.OrdinalIgnoreCase);
    }

    private void OnOptionChecked(object sender, RoutedEventArgs e)
    {
        if (sender is not RadioButton radioButton ||
            radioButton.DataContext is not string option ||
            DataContext is not SingleChoiceDisplayableQuestion question)
        {
            return;
        }

        var page = ControlUtility.FindAncestor<QuestionPage>(this);
        if (page?.DataContext is QuestionViewModel viewModel)
        {
            viewModel.Model.UpdateAnswer(new SingleChoiceAnswer(option.Trim()));
        }
    }
}

