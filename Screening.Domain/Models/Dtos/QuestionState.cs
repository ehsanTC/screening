using Screening.Domain.Questions.Abstraction;

namespace Screening.Domain.Models.Dtos;

public record QuestionState(
    IDisplayableQuestion? CurrentQuestion,
    ProgressionState Progression,
    bool CanGoNext,
    bool CanGoBack,
    bool IsLastQuestion,
    string NextButtonText
);
