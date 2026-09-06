namespace Common.Abstraction;

public interface ILocalizedFileNameResolver
{
    string Resolve(string fileName, string languageCode);
}
