using Screening.Domain.Parser.Models;

namespace Screening.Domain.Services.Profile;

public interface IProfileService
{
    Task SaveAsync(string key, SavableAnswer value);
    Task DeleteAsync(string key);
    Task <SavableAnswer?> LoadAsync(string key);
}
