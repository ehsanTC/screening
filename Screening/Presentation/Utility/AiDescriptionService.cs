using System.Text.Json;
using Common.Abstraction;
using Screening.Presentation.Model;

namespace Screening.Presentation.Utility;

public class AiDescriptionService : IAiDescriptionService
{
    private readonly IChatService _chatService;
    private readonly IAssetFileProvider _assetFileProvider;

    public AiDescriptionService(IChatService chatService, IAssetFileProvider assetFileProvider)
    {
        _chatService = chatService ?? throw new ArgumentNullException(nameof(chatService));
        _assetFileProvider = assetFileProvider ?? throw new ArgumentNullException(nameof(assetFileProvider));
    }
    public async Task<string?> GetDescriptionAsync(string prompt, CancellationToken ct)
    {
        var keyValues = GetAiCredentials("AiCredentials.json");

        var config = new ChatConfig(keyValues["url"].GetString() ?? string.Empty,  
            keyValues["api_key"].GetString() ?? string.Empty, 
            keyValues["model"].GetString() ?? string.Empty);
        
        return await _chatService.GetChatResponseAsync(prompt, config, ct);
    }

    private Dictionary<string, JsonElement> GetAiCredentials(string file)
    {
        using var stream = _assetFileProvider.OpenAsset(file);
        using var reader = new StreamReader(stream);
        var json = reader.ReadToEnd();
        return JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json)!;
    }
}
