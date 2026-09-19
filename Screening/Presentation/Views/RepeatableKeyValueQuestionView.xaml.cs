using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using Screening.Domain.Answers;
using Screening.Domain.Models;
using Screening.Domain.Questions;
using Screening.Presentation.Model;
using Screening.Presentation.Utility;

namespace Screening.Presentation.Views;

public sealed partial class RepeatableKeyValueQuestionView : UserControl
{
    private RepeatableKeyValueDisplayableQuestion? Question => DataContext as RepeatableKeyValueDisplayableQuestion;

    private ObservableCollection<LabResultEntry> _labResults = new();
    private ObservableCollection<DisorderEntry> _disorders = new();

    public RepeatableKeyValueQuestionView()
    {
        InitializeComponent();
        DataContextChanged += OnDataContextChanged;
    }

    private void OnDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
    {
        var question = Question;
        if (question is null) return;

        _labResults = new ObservableCollection<LabResultEntry>(
            (question.Answer?.LabResults ?? []).Select(r => new LabResultEntry { Name = r.Key, Value = r.Value }));

        _disorders = new ObservableCollection<DisorderEntry>(
            (question.Answer?.Disorders ?? []).Select(name => new DisorderEntry { Name = name }));

        HookCollectionEvents(_labResults);
        HookCollectionEvents(_disorders);

        LabResultsControl.ItemsSource = _labResults;
        DisordersControl.ItemsSource = _disorders;

        UpdateAddResultButtonState();
        UpdateAddDisorderButtonState();
    }

    // ------------------------------------------------------------------------
    // Lab results
    // ------------------------------------------------------------------------

    private void OnAddResultClicked(object sender, RoutedEventArgs e)
    {
        // guard: last result must be filled before adding another
        if (_labResults.Count > 0 && !IsFilled(_labResults[^1]))
            return;
        
        _labResults.Add(new LabResultEntry());
    }
    private static bool IsFilled(LabResultEntry entry) =>
        !string.IsNullOrWhiteSpace(entry.Name) && !string.IsNullOrWhiteSpace(entry.Value);

    private void UpdateAddResultButtonState()
    {
        AddResultButton.IsEnabled = _labResults.Count == 0 || IsFilled(_labResults[^1]);
    }
    
    private void OnRemoveResultClicked(object sender, RoutedEventArgs e)
    {
        if (sender is Button { Tag: LabResultEntry entry })
        {
            _labResults.Remove(entry);
        }
    }

    // ------------------------------------------------------------------------
    // Disorders
    // ------------------------------------------------------------------------

    private void OnAddDisorderClicked(object sender, RoutedEventArgs e)
    {
        if (_disorders.Count > 0 && string.IsNullOrWhiteSpace(_disorders[^1].Name))
        {
            return; // guard: last disorder must be filled before adding another
        }

        _disorders.Add(new DisorderEntry());
    }

    private void OnRemoveDisorderClicked(object sender, RoutedEventArgs e)
    {
        if (sender is Button { Tag: DisorderEntry entry })
        {
            _disorders.Remove(entry);
        }
    }

    private void UpdateAddDisorderButtonState()
    {
        AddDisorderButton.IsEnabled =
            _disorders.Count == 0 || !string.IsNullOrWhiteSpace(_disorders[^1].Name);
    }

    // ------------------------------------------------------------------------
    // Centralized change tracking
    // ------------------------------------------------------------------------

    // Any add, remove, or edit — on either collection — goes through here.
    // No handler above needs to call PersistAnswer or UpdateAddDisorderButtonState directly.
    private void HookCollectionEvents(INotifyCollectionChanged collection)
    {
        collection.CollectionChanged += (_, args) =>
        {
            if (args.NewItems is not null)
            {
                foreach (var item in args.NewItems)
                {
                    if (item is INotifyPropertyChanged notifying)
                    {
                        notifying.PropertyChanged += OnEntryChanged;
                    }
                }
            }

            OnAnyChange();
        };
    }

    private void OnEntryChanged(object? sender, PropertyChangedEventArgs e) => OnAnyChange();

    private void OnAnyChange()
    {
        UpdateAddResultButtonState();
        UpdateAddDisorderButtonState();
        PersistAnswer();
    }

    // ------------------------------------------------------------------------
    // Answer handling
    // ------------------------------------------------------------------------

    private async void PersistAnswer()
    {
        var question = Question;
        if (question is null) return;

        var answer = new RepeatableKeyValueAnswer(
            _labResults.Select(r => new KeyValue(r.Name.Trim(), r.Value.Trim())).ToList(),
            _disorders.Select(d => d.Name.Trim()).ToList());

        var page = ControlUtility.FindAncestor<QuestionPage>(this);
        if (page?.DataContext is QuestionViewModel viewModel)
        {
            await viewModel.Model.UpdateAnswer(answer);
        }
    }
}
