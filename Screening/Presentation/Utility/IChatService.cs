using Screening.Presentation.Model;

namespace Screening.Presentation.Utility;

public interface IChatService
{
    Task<string> GetChatResponseAsync(string message, ChatConfig config, CancellationToken ct);
}
