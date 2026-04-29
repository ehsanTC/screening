
using Screening.Domain.Questions;

namespace Screening.Presentation;

public record QuestionState(
    IQuestion? CurrentQuestion,
    bool CanGoNext,
    bool CanGoBack
);

