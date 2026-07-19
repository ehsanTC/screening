namespace Screening.Domain.Models.Dtos;

public record ProgressionState(double Percent, int Current, int Total)
{
    public string Display => $"{Current} / {Total}";
}
