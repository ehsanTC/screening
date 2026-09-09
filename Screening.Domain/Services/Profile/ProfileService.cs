using System.Collections.Concurrent;
using System.Text.Json;
using Screening.Domain.Parser.Models;
using Screening.Domain.Services.FileStorage;

namespace Screening.Domain.Services.Profile;

public class ProfileService(ISessionStorageService storage) : IProfileService
{
    private static ConcurrentDictionary<string, SavableAnswer>? _profile = new();
    private static readonly JsonSerializerOptions? Options = new()
    {
        PropertyNameCaseInsensitive = true,
        WriteIndented = true
    };
    
    private async Task LoadDictionary()
    {
        try
        {
            var fileContent = await storage.LoadAsync<string>(ApplicationFiles.UserProfile);
            _profile = JsonSerializer.Deserialize<ConcurrentDictionary<string, SavableAnswer>>(
                fileContent ?? string.Empty,
                Options);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task SaveAsync(string key, SavableAnswer value)
    {
        _profile.AddOrUpdate(key, value, (k, v) => value);
        
        var serializedData = JsonSerializer.Serialize(_profile, Options);
        await storage.SaveAsync(ApplicationFiles.UserProfile, serializedData);
    }

    public async Task DeleteAsync(string key)
    {
        _profile.TryRemove(key, out _);
        
        var serializedData = JsonSerializer.Serialize(_profile);
        await storage.SaveAsync(ApplicationFiles.UserProfile, serializedData);
    }

    public async Task<SavableAnswer?> LoadAsync(string key)
    {
        await LoadProfileData();
        
        _profile.TryGetValue(key, out var value);
        return value;
    }

    private async Task LoadProfileData()
    {
        if (_profile == null || _profile?.Count == 0)
            await LoadDictionary();
    }

    public async Task<IDictionary<string, SavableAnswer>> LoadAllAsync()
    {
        await LoadProfileData();
        
        var result = new Dictionary<string, SavableAnswer>();

        foreach (var key in _profile.Keys)
        {
            if (!string.IsNullOrEmpty(key) &&
                _profile.TryGetValue(key, out var value))
            {
                result.Add(key, value);
            }
        }
        
        return result;
    }

    public SavableAnswer? Load(string key)
    {
        throw new NotImplementedException();
    }
}
