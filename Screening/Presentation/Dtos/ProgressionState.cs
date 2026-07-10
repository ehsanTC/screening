namespace Screening.Presentation.Dtos;

public record ProgressionState(double Percent, int Current, int Total)
{
    public string Display => $"{Current} / {Total}";
}
