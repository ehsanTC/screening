using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Screening.Presentation.Model;
using Screening.Presentation.Services.Abstraction;

namespace Screening.Presentation.Services;

public class ChatService : IChatService
{
    private readonly HttpClient _http;
    
    public ChatService(IHttpClientFactory httpClientFactory)
    {
        _http = httpClientFactory?.CreateClient() ?? throw new ArgumentNullException(nameof(httpClientFactory));
    }
    
    public async Task<string> GetChatResponseAsync(string message, ChatConfig config, CancellationToken ct)
    {   
        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", config.ApiKey);
        
        var requestBody = new
        {
            model = config.Model,
            messages = new[]
            {
                new { role = "user", content = message }
            }
        };

        var json = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _http.PostAsync(config.Url, content, ct);
        response.EnsureSuccessStatusCode();

        var responseJson = await response.Content.ReadAsStringAsync(ct);

        using var doc = JsonDocument.Parse(responseJson);

        if (!doc.RootElement.TryGetProperty("choices", out var choices) ||
            choices.GetArrayLength() == 0)
            return string.Empty;
        
        var reply = doc.RootElement
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString();

        return reply ?? string.Empty;
    }
}
