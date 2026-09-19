using Screening.Presentation.Model;

namespace Screening.Presentation.Services.Abstraction;

public interface IChatService
{
    Task<string> GetChatResponseAsync(string message, ChatConfig config, CancellationToken ct);
}
