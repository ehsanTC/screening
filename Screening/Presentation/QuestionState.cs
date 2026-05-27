using Screening.Domain.Questions;

namespace Screening.Presentation;

public record QuestionState(
    IDisplayableQuestion? CurrentQuestion,
    bool CanGoNext,
    bool CanGoBack,
    bool Finished
);

