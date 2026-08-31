using System.Text.Json.Serialization;

namespace Screening.Domain.RiskCalculator.Fracture.Model;

public class FemoralNeckBmd
{
    [JsonPropertyName("scanningEquipment")]
    public string ScanningEquipment { get; set; } = string.Empty;

    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;

    [JsonPropertyName("unit")]
    public string Unit { get; set; } = string.Empty;
}
