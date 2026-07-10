using Screening.Domain.Questions.Abstraction;

namespace Screening.Presentation.Dtos;

public record QuestionState(
    IDisplayableQuestion? CurrentQuestion,
    double Progression,
    bool CanGoNext,
    bool CanGoBack,
    bool IsLastQuestion,
    string NextButtonText
);
