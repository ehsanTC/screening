using System.Globalization;
using Screening.Domain.Answers;
using Screening.Domain.Questions;
using Screening.Presentation.Utility;
using Windows.ApplicationModel.Resources;
using Windows.System;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml;

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

    private void OnTextBoxKeyDown(object sender, KeyRoutedEventArgs e)
    {
        // Intercept Enter key so it doesn't propagate and close the whole page on Android.
        if (e.Key == VirtualKey.Enter)
        {
            e.Handled = true;

            // Move focus away from the TextBox to hide the soft keyboard.
            // Setting focus to the UserControl itself will remove focus from the TextBox.
            _ = this.Focus(FocusState.Programmatic);
        }
    }

    private void OnTextBoxGotFocus(object sender, RoutedEventArgs e)
    {
        if (sender is TextBox tb)
        {
            // Select all text when the TextBox gains focus
            tb.SelectAll();
        }
    }

    private void OnTextBoxPointerPressed(object sender, PointerRoutedEventArgs e)
    {
        if (sender is TextBox tb)
        {
            // When the user taps the TextBox, ensure it gets focus and its text is selected.
            var focused = Microsoft.UI.Xaml.Input.FocusManager.GetFocusedElement() as TextBox;
            if (focused != tb)
            {
                tb.Focus(FocusState.Pointer);
                e.Handled = true;
                tb.SelectAll();
            }
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
