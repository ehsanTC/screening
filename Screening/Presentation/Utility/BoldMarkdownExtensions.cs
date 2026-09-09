using System.Text.RegularExpressions;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;

namespace Screening.Presentation.Utility;

public static class BoldMarkdownExtensions
{
    private static readonly Regex BoldPattern =
        new(@"\*\*(.+?)\*\*", RegexOptions.Compiled);

    public static readonly DependencyProperty BoldMarkdownTextProperty =
        DependencyProperty.RegisterAttached(
            "BoldMarkdownText",
            typeof(string),
            typeof(BoldMarkdownExtensions),
            new PropertyMetadata(string.Empty, OnBoldMarkdownTextChanged));

    public static void SetBoldMarkdownText(
        DependencyObject element,
        string value)
    {
        element.SetValue(BoldMarkdownTextProperty, value);
    }

    public static string GetBoldMarkdownText(
        DependencyObject element)
    {
        return (string)element.GetValue(BoldMarkdownTextProperty);
    }

    private static void OnBoldMarkdownTextChanged(
        DependencyObject d,
        DependencyPropertyChangedEventArgs e)
    {
        if (d is not TextBlock textBlock)
            return;

        var markdown = e.NewValue as string ?? string.Empty;

        textBlock.Inlines.Clear();

        var lastIndex = 0;

        foreach (Match match in BoldPattern.Matches(markdown))
        {
            // Normal text before the bold section
            if (match.Index > lastIndex)
            {
                textBlock.Inlines.Add(
                    new Run
                    {
                        Text = markdown.Substring(
                            lastIndex,
                            match.Index - lastIndex)
                    });
            }

            // Bold section
            var bold = new Bold();

            bold.Inlines.Add(
                new Run
                {
                    Text = match.Groups[1].Value
                });

            textBlock.Inlines.Add(bold);

            lastIndex = match.Index + match.Length;
        }

        // Remaining normal text
        if (lastIndex < markdown.Length)
        {
            textBlock.Inlines.Add(
                new Run
                {
                    Text = markdown.Substring(lastIndex)
                });
        }
    }
}
