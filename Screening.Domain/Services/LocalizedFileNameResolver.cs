using Screening.Domain.Services.Abstraction;
using Screening.Domain.Utilities;

namespace Screening.Domain.Services;

public class LocalizedFileNameResolver : ILocalizedFileNameResolver
{
    private static readonly string DefaultLanguageCode = "en";

    public string Resolve(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            throw new ArgumentException("File name cannot be null or empty.", nameof(fileName));

        var languageCode = LanguageHelper.GetCurrentLanguage();

        // If current language is the default, return the original file name
        return string.Equals(languageCode, DefaultLanguageCode, StringComparison.OrdinalIgnoreCase) 
            ? $"{DefaultLanguageCode}\\{fileName}" 
            : $"{languageCode.ToLowerInvariant()}\\{fileName}";
    }
}
