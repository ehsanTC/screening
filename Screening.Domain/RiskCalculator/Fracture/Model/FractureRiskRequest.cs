namespace Screening.Domain.RiskCalculator.Fracture.Model;

using System.Text.Json.Serialization;

public class FractureRiskRequest
{
    [JsonPropertyName("continent")] 
    public int Continent { get; } = 4;  // Middle east and Africa

    [JsonPropertyName("country")]
    public int Country { get; } = 68;   // Iran

    [JsonPropertyName("identification")]
    public string Identification { get; set; } = string.Empty;

    [JsonPropertyName("age")]
    public int Age { get; set; }

    [JsonPropertyName("sex")]
    public string Sex { get; set; }

    [JsonPropertyName("bmi")]
    public Bmi Bmi { get; set; } = new();

    [JsonPropertyName("previousFracture")]
    public bool PreviousFracture { get; set; }

    [JsonPropertyName("parentFracturedHip")]
    public bool ParentFracturedHip { get; set; }

    [JsonPropertyName("smoking")]
    public bool Smoking { get; set; }

    [JsonPropertyName("glucocorticoids")]
    public bool Glucocorticoids { get; set; }

    [JsonPropertyName("rheumatoidArthritis")]
    public bool RheumatoidArthritis { get; set; }

    [JsonPropertyName("secondaryOsteoporosis")]
    public bool SecondaryOsteoporosis { get; set; }

    [JsonPropertyName("alcohol")]
    public bool Alcohol { get; set; }

    [JsonPropertyName("femoralNeckBMD")]
    public FemoralNeckBmd FemoralNeckBmd { get; set; } = new();
}
