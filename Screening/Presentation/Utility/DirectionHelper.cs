using System.Globalization;

namespace Screening.Presentation.Utility;

public static class DirectionHelper
{
    public static bool IsRightToLeft { get; private set; }

    public static FlowDirection Direction =>
        IsRightToLeft ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;

    public static TextAlignment Alignment =>
        IsRightToLeft ? TextAlignment.Right : TextAlignment.Left;

    public static void Initialize(string languageTag)
    {
        IsRightToLeft = new CultureInfo(languageTag).TextInfo.IsRightToLeft;
        ImplicitStyleHelper.Install(Alignment);
    }
}
