using System.Text.Json.Serialization;

namespace Screening.Domain.RiskCalculator.Fracture.Model;

public class Bmi
{
    [JsonPropertyName("weight")]
    public float WeightKg { get; set; }

    [JsonPropertyName("height")]
    public float HeightCm { get; set; }

    [JsonPropertyName("units")] 
    public string Units { get; } = "metric";
}
