namespace Screening.Presentation.Utility;

public static class ControlUtility
{
    public static T? FindAncestor<T>(DependencyObject child)
        where T : class, DependencyObject
    {
        var parent = VisualTreeHelper.GetParent(child);
        while (parent != null)
        {
            if (parent is T ancestor)
            {
                return ancestor;
            }

            parent = VisualTreeHelper.GetParent(parent);
        }

        return null;
    }
}
