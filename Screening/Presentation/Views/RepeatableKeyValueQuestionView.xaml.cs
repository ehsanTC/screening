using System.Collections.ObjectModel;
using Screening.Domain.Answers;
using Screening.Domain.Questions;
using Screening.Presentation.Model;
using Screening.Presentation.Utility;

namespace Screening.Presentation.Views;

public sealed partial class RepeatableKeyValueQuestionView : UserControl
{
    private RepeatableKeyValueDisplayableQuestion? Question => DataContext as RepeatableKeyValueDisplayableQuestion;

    private ObservableCollection<KeyValueEntry> _labResults = new();
    private ObservableCollection<DisorderEntry> _disorders = new();
    private int? _currentQuestionId;
    
    public RepeatableKeyValueQuestionView()
    {
        InitializeComponent();
        DataContextChanged += OnDataContextChanged;
    }

    private void OnDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
    {
        var question = Question;
        if (question is null) return;

        _labResults = new ObservableCollection<KeyValueEntry>(question.Answer?.LabResults ?? []);
        _disorders = new ObservableCollection<DisorderEntry>(
            (question.Answer?.Disorders ?? []).Select(name => new DisorderEntry { Name = name }));

        LabResultsControl.ItemsSource = _labResults;
        DisordersControl.ItemsSource = _disorders;
    }

    // ------------------------------------------------------------------------
    // Lab results
    // ------------------------------------------------------------------------

    private void OnAddResultClicked(object sender, RoutedEventArgs e)
    {
        _labResults.Add(new KeyValueEntry(string.Empty, string.Empty));
        PersistAnswer();
    }

    private void OnRemoveResultClicked(object sender, RoutedEventArgs e)
    {
        if (sender is not Button button || button.Tag is not KeyValueEntry entry) return;
        _labResults.Remove(entry);
        PersistAnswer();
    }

    private void OnResultTextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is not TextBox textBox ||
            textBox.DataContext is not KeyValueEntry oldEntry)
        {
            return;
        }

        var index = _labResults.IndexOf(oldEntry);
        if (index < 0) return;

        var field = (string)textBox.Tag; // use Tag purely as a field-name marker now
        var newEntry = field switch
        {
            "Name" => oldEntry with { Name = textBox.Text },
            "Value" => oldEntry with { Value = textBox.Text },
            _ => oldEntry
        };

        // Update item IN PLACE — do not reassign ItemsSource
        _labResults[index] = newEntry;

        // Re-tag the TextBox's underlying Tag reference isn't needed since
        // binding is by DataContext, not by Tag now.

        PersistAnswer();
    }

    // ------------------------------------------------------------------------
    // Disorders
    // ------------------------------------------------------------------------

    private void OnAddDisorderClicked(object sender, RoutedEventArgs e)
    {
        if (_disorders.Count > 0 && string.IsNullOrWhiteSpace(_disorders[^1].Name))
        {
            return;
        }

        _disorders.Add(new DisorderEntry());
        UpdateAddDisorderButtonState();
        PersistAnswer();
    }

    private void OnRemoveDisorderClicked(object sender, RoutedEventArgs e)
    {
        if (sender is not Button button || button.Tag is not DisorderEntry entry) return;
        _disorders.Remove(entry);
        UpdateAddDisorderButtonState();
        PersistAnswer();
    }

    private void OnDisorderTextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateAddDisorderButtonState();
        PersistAnswer();
    }
    
    private void UpdateAddDisorderButtonState()
    {
        AddDisorderButton.IsEnabled =
            _disorders.Count == 0 || !string.IsNullOrWhiteSpace(_disorders[^1].Name);
    }
    // ------------------------------------------------------------------------
    // Answer handling
    // ------------------------------------------------------------------------

    private async void PersistAnswer()
    {
        var question = Question;
        if (question is null) return;

        var answer = new RepeatableKeyValueAnswer(
            _labResults.ToList(),
            _disorders.Select(d => d.Name).ToList());

        var page = ControlUtility.FindAncestor<QuestionPage>(this);
        if (page?.DataContext is QuestionViewModel viewModel)
        {
            await viewModel.Model.UpdateAnswer(answer);
        }
    }
}
