using System.Text.Json;
using System.Text.Json.Serialization;

namespace Screening.Domain.RiskCalculator.Fracture.Model;


public class FractureRiskErrorResponse
{
    [JsonPropertyName("errors")]
    public List<FractureRiskError> Errors { get; set; } = new();
}

public class FractureRiskError
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("value")]
    public FemoralNeckBmd Value { get; set; }

    [JsonPropertyName("msg")]
    public FractureRiskErrorMessage Msg { get; set; } = new();

    [JsonPropertyName("path")]
    public string Path { get; set; } = string.Empty;

    [JsonPropertyName("location")]
    public string Location { get; set; } = string.Empty;
}

public class FractureRiskErrorMessage
{
    [JsonPropertyName("code")]
    public string Code { get; set; } = string.Empty;

    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;

    [JsonPropertyName("field")]
    public string Field { get; set; } = string.Empty;
}
