namespace Screening.Domain.RiskCalculator.Abstraction;

public enum RiskSeverity
{
    Low,
    BorderLine,
    Medium,
    High,
    
    NotEligibleForAssessment,
    NoIndicationForFurtherAssessment,
    InsufficientData,
    
    Unknown
}
