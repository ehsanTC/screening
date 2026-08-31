using Screening.Domain.RiskCalculator.Fracture.Model;
using Screening.Domain.RiskCalculator.Models;

namespace Screening.Domain.RiskCalculator.Fracture.Utility;

using System.Net.Http.Json;

public class FractureRiskApiClient
{
    private readonly HttpClient _httpClient;

    public FractureRiskApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<FractureRiskResponse> CalculateAsync(FractureRiskInput input, Uri address)
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
            request.FemoralNeckBmd = new FemoralNeckBmd
            {
                ScanningEquipment = input.BmdMeasure.Value.Value.ToString(),
                Unit = input.BmdMeasure.Value.Unit,
                Value = input.BmdValue?.ToString() ?? string.Empty,
            };

        var response = await _httpClient.PostAsJsonAsync(address, request);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content
                .ReadFromJsonAsync<FractureRiskResponse>();

            return result ?? throw new InvalidOperationException("API returned an empty response.");
        }

        var error = await response.Content
            .ReadFromJsonAsync<FractureRiskErrorResponse>();

        var message = error?.Errors.FirstOrDefault()?.Msg?.Message
                      ?? $"API request failed with status {(int)response.StatusCode}.";

        throw new FractureRiskApiException(
            response.StatusCode,
            message,
            error);
    }
}
