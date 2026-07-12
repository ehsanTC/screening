namespace Screening.Services.FileStorage;

public interface ISessionStorageService
{
    Task SaveAsync<T>(string fileName, T data);
    Task<T?> LoadAsync<T>(string fileName);
    Task DeleteAsync(string fileName);
}
