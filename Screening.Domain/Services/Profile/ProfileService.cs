using System.Collections.Concurrent;
using System.Text.Json;
using Screening.Domain.Parser.Models;
using Screening.Domain.Services.FileStorage;

namespace Screening.Domain.Services.Profile;

public class ProfileService(ISessionStorageService storage) : IProfileService
{
    private static bool _isLoaded;
    private static readonly SemaphoreSlim Lock = new(1, 1);
    private static ConcurrentDictionary<string, SavableAnswer> _profile = new();

    private async Task EnsureLoadedAsync()
    {
        if (_isLoaded) return;

        await Lock.WaitAsync();
        try
        {
            if (_isLoaded) return;

            var loaded = await storage.LoadAsync<ConcurrentDictionary<string, SavableAnswer>>(
                ApplicationFiles.UserProfile);

            _profile = loaded ?? new ConcurrentDictionary<string, SavableAnswer>();
            _isLoaded = true;
        }
        catch (JsonException ex)
        {
            Console.WriteLine(ex);
            _profile = new();
        }
        finally
        {
            Lock.Release();
        }
    }

    public async Task SaveAsync(string key, SavableAnswer answer)
    {
        await LoadProfileData();

        _profile.AddOrUpdate(key, answer, (k, v) => answer);
        await storage.SaveAsync(ApplicationFiles.UserProfile, _profile);
    }

    public async Task DeleteAsync(string key)
    {
        await LoadProfileData();
        _profile.TryRemove(key, out _);
        await storage.SaveAsync(ApplicationFiles.UserProfile, _profile);
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
            await EnsureLoadedAsync();
    }

    public async Task<IDictionary<string, SavableAnswer>> LoadAllAsync()
    {
        await LoadProfileData();
        
        var result = new Dictionary<string, SavableAnswer>();

        foreach (var key in _profile.Keys)
        {
            if (!string.IsNullOrEmpty(key) && _profile.TryGetValue(key, out var value))
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
