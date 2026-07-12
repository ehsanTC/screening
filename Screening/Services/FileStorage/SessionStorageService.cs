using System.Text.Json;

namespace Screening.Services.FileStorage;

public class SessionStorageService : ISessionStorageService
{
    public async Task SaveAsync<T>(string fileName, T data)
    {
        StorageFolder folder = ApplicationData.Current.LocalFolder;
        StorageFile file = await folder.CreateFileAsync(
            $"{fileName}.json",
            CreationCollisionOption.ReplaceExisting);

        string json = JsonSerializer.Serialize(data);
        await FileIO.WriteTextAsync(file, json);
    }

    public async Task<T?> LoadAsync<T>(string fileName)
    {
        StorageFolder folder = ApplicationData.Current.LocalFolder;

        try
        {
            StorageFile file = await folder.GetFileAsync($"{fileName}.json");
            string json = await FileIO.ReadTextAsync(file);
            return JsonSerializer.Deserialize<T>(json);
        }
        catch (FileNotFoundException)
        {
            return default;
        }
    }

    public async Task DeleteAsync(string fileName)
    {
        StorageFolder folder = ApplicationData.Current.LocalFolder;

        try
        {
            StorageFile file = await folder.GetFileAsync($"{fileName}.json");
            await file.DeleteAsync();
        }
        catch (FileNotFoundException)
        {
            // already gone, nothing to do
        }
    }
}
