using System.Text.RegularExpressions;
using Microsoft.UI.Xaml.Documents;

namespace Screening.Presentation.Utility;

public static class BoldMarkdownExtensions
{
    private static readonly Regex BoldPattern = new(@"\*\*(.+?)\*\*", RegexOptions.Compiled);

    public static readonly DependencyProperty BoldMarkdownTextProperty =
        DependencyProperty.RegisterAttached(
            "BoldMarkdownText",
            typeof(string),
            typeof(BoldMarkdownExtensions),
            new PropertyMetadata(null, OnBoldMarkdownTextChanged));

    public static void SetBoldMarkdownText(TextBlock element, string value) =>
        element.SetValue(BoldMarkdownTextProperty, value);

    public static string GetBoldMarkdownText(TextBlock element) =>
        (string)element.GetValue(BoldMarkdownTextProperty);

    private static void OnBoldMarkdownTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not TextBlock textBlock)
            return;

        var text = e.NewValue as string ?? string.Empty;
        textBlock.Inlines.Clear();

        int lastIndex = 0;
        foreach (Match match in BoldPattern.Matches(text))
        {
            if (match.Index > lastIndex)
                textBlock.Inlines.Add(new Run { Text = text.Substring(lastIndex, match.Index - lastIndex) });

            textBlock.Inlines.Add(new Bold { Inlines = { new Run { Text = match.Groups[1].Value } } });

            lastIndex = match.Index + match.Length;
        }

        if (lastIndex < text.Length)
            textBlock.Inlines.Add(new Run { Text = text.Substring(lastIndex) });
    }
}
