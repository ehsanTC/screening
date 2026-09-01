using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Screening.Domain.RiskCalculator.Fracture.Model;
using Screening.Domain.RiskCalculator.Models;

namespace Screening.Domain.RiskCalculator.Fracture.Utility;

public class FractureRiskApiClient(HttpClient httpClient)
{
    private readonly HttpClient _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

    public async Task<FractureRiskResponse> CalculateAsync(FractureRiskInput input)
    {
        // Step 1: visit the calculation-tool page so the session cookie gets set
        var pageResponse = await _httpClient.GetAsync("/en/calculation-tool");
        pageResponse.EnsureSuccessStatusCode();

        // Step 2: get the CSRF token from current-user
        var csrfToken = await GetCsrfTokenAsync();

        // Step 3: build the request body
        var request = BuildRequest(input);

        // Step 4: send the POST with the token attached
        using var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/api/v1/frax");
        requestMessage.Content = JsonContent.Create(request);
        requestMessage.Headers.Add("X-CSRF-Token", csrfToken);

        var response = await _httpClient.SendAsync(requestMessage);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<FractureRiskResponse>();
            return result ?? throw new InvalidOperationException("API returned an empty response.");
        }

        var error = await response.Content.ReadFromJsonAsync<FractureRiskErrorResponse>();

        var message = error?.Errors.FirstOrDefault()?.Msg?.Message
                      ?? $"API request failed with status {(int)response.StatusCode}.";

        throw new FractureRiskApiException(response.StatusCode, message, error);
    }

    private FractureRiskRequest BuildRequest(FractureRiskInput input)
    {
        var request = new FractureRiskRequest
        {
            Age = input.Age,
            Sex = input.Sex == Sex.Male ? "M" : "F",

            Bmi = new Bmi
            {
                WeightKg = input.WeightKg,
                HeightCm = input.HeightCm,
            },

            Smoking = input.Smoking ?? false,
            PreviousFracture = input.PreviousFracture ?? false,
            Glucocorticoids = input.TakingCorticosteroids ?? false,
            ParentFracturedHip = input.ParentFracturedHip ?? false,
            RheumatoidArthritis = input.HaveRheumatoidArthritis ?? false,
            SecondaryOsteoporosis = input.HaveSecondaryOsteoporosis ?? false,
            Alcohol = input.DrinkExcessAlcohol ?? false,
        };

        if (input.BmdMeasure != null)
        {
            request.FemoralNeckBmd = new FemoralNeckBmd
            {
                ScanningEquipment = input.BmdMeasure.Value.EquipmentNumber.ToString(),
                Unit = input.BmdMeasure.Value.Unit,
                Value = input.BmdValue?.ToString() ?? string.Empty,
            };
        }
        
        return request;
    }

    private async Task<string> GetCsrfTokenAsync()
    {
        var response = await _httpClient.GetAsync("/api/v1/current-user");
        response.EnsureSuccessStatusCode();

        var currentUser = await response.Content.ReadFromJsonAsync<CurrentUserResponse>();

        if (currentUser is null || string.IsNullOrWhiteSpace(currentUser.SessionToken))
            throw new InvalidOperationException("Could not obtain CSRF/session token from current-user endpoint.");

        return currentUser.SessionToken;
    }
}

public class CurrentUserResponse
{
    [JsonPropertyName("user_id")]
    public int UserId { get; set; }

    [JsonPropertyName("access")]
    public bool Access { get; set; }

    [JsonPropertyName("api_key")]
    public string ApiKey { get; set; } = string.Empty;

    [JsonPropertyName("session_token")]
    public string SessionToken { get; set; } = string.Empty;

    [JsonPropertyName("roles")]
    public List<string> Roles { get; set; } = new();

    [JsonPropertyName("permissions")]
    public List<string> Permissions { get; set; } = new();
}
