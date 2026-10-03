namespace Screening.Presentation.Utility;

public static class ImplicitStyleHelper
{
    private const string AppFont = "ms-appx:///Assets/Fonts/YEKANBAKHFANUM-REGULAR.TTF#YEKANBAKHFANUM";

    // Call once at startup, BEFORE any page or dialog is created.
    public static void Install(TextAlignment alignment)
    {
        var font = new FontFamily(AppFont);

        Apply(typeof(TextBlock), style =>
        {
            style.Setters.Add(new Setter(TextBlock.FontFamilyProperty, font));
            style.Setters.Add(new Setter(TextBlock.TextAlignmentProperty, alignment));
            style.Setters.Add(new Setter(TextBlock.HorizontalTextAlignmentProperty, alignment));
        });
    }

    private static void Apply(Type targetType, Action<Style> configure)
    {
        var style = new Style(targetType);

        // Keep the style the theme already has, so the control template is not lost.
        if (Application.Current.Resources.TryGetValue(targetType, out var existing)
            && existing is Style existingStyle)
        {
            style.BasedOn = existingStyle;
        }

        configure(style);
        Application.Current.Resources[targetType] = style;
    }
}
