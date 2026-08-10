
using Windows.Globalization;

namespace Screening.Domain.Utilities;

public static class LanguageHelper
{
    public static string GetCurrentLanguage()
    {
        var overrideLang = ApplicationLanguages.PrimaryLanguageOverride;

        // Use the override if set, otherwise use the app/system default
        string fullTag = !string.IsNullOrEmpty(overrideLang)
            ? overrideLang
            : ApplicationLanguages.Languages[0];

        // fullTag can look like "en-US" or "fa-IR" or just "en"
        // We only want the first part: "en", "fa", etc.
        return fullTag.Split('-')[0];
    }
}
