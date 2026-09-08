namespace Screening.Presentation.Utility;

public interface IAiDescriptionService    
{
    Task<string?> GetDescriptionAsync(string prompt, CancellationToken ct);
}
