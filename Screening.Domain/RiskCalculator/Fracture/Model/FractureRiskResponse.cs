using System.Text.Json.Serialization;

namespace Screening.Domain.RiskCalculator.Fracture.Model;

public class FractureRiskResponse
{
    [JsonPropertyName("createdAt")]
    public long CreatedAt { get; set; }

    [JsonPropertyName("fraxScore")]
    public FraxScore FraxScore { get; set; } = new();
}

public class FraxScore
{
    [JsonPropertyName("mof")]
    public FraxScoreValue MajorOsteoporosisFracture { get; set; } = new();

    [JsonPropertyName("hip")]
    public FraxScoreValue Hip { get; set; } = new();
}

public class FraxScoreValue
{
    [JsonPropertyName("display")]
    public string Display { get; set; } = string.Empty;

    [JsonPropertyName("raw")]
    public double Raw { get; set; }
}
