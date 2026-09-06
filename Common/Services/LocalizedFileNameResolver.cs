using Common.Abstraction;

namespace Common.Services;

public class LocalizedFileNameResolver : ILocalizedFileNameResolver
{
    private const string DefaultLanguageCode = "en";

    public string Resolve(string fileName, string languageCode)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            throw new ArgumentException("File name cannot be null or empty.", nameof(fileName));

        // If current language is the default (en), return the original file name
        return string.Equals(languageCode, DefaultLanguageCode, StringComparison.OrdinalIgnoreCase) 
            ? $"{DefaultLanguageCode}\\{fileName}" 
            : $"{languageCode.ToLowerInvariant()}\\{fileName}";
    }
}
