using Screening.Domain.Questions.Abstraction;

namespace Screening.Presentation;

public record QuestionState(
    IDisplayableQuestion? CurrentQuestion,
    bool CanGoNext,
    bool CanGoBack,
    bool IsLastQuestion,
    string NextButtonText
);
