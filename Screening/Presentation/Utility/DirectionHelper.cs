using System.Globalization;

namespace Screening.Presentation.Utility;

public static class DirectionHelper
{
    public static bool IsRightToLeft { get; private set; }

    public static FlowDirection Direction =>
        IsRightToLeft ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;

    public static TextAlignment Alignment =>
        IsRightToLeft ? TextAlignment.Right : TextAlignment.Left;

    // Call this once at startup, BEFORE any page or dialog is created.
    public static void Initialize(string languageTag)
    {
        IsRightToLeft = new CultureInfo(languageTag).TextInfo.IsRightToLeft;
        InstallTextBlockStyle();
    }

    private static void InstallTextBlockStyle()
    {
        var style = new Style(typeof(TextBlock));

        // Keep any implicit TextBlock style your theme may already have.
        if (Application.Current.Resources.TryGetValue(typeof(TextBlock), out var existing)
            && existing is Style existingStyle)
        {
            style.BasedOn = existingStyle;
        }

        style.Setters.Add(new Setter(TextBlock.TextAlignmentProperty, Alignment));
        style.Setters.Add(new Setter(TextBlock.HorizontalTextAlignmentProperty, Alignment));

        Application.Current.Resources[typeof(TextBlock)] = style;
    }
}
