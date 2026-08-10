namespace Screening.Domain.Services.Abstraction;

public interface ILocalizedFileNameResolver
{
    string Resolve(string fileName);
}
