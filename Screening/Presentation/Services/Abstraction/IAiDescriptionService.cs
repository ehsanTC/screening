namespace Screening.Presentation.Services.Abstraction;

public interface IAiDescriptionService    
{
    Task<string?> GetDescriptionAsync(string prompt, CancellationToken ct);
}
