
using Screening.Domain.Questions;

namespace Screening.Presentation;

public record QuestionState(
    IQuestionViewModel? CurrentQuestion,
    bool CanGoNext,
    bool CanGoBack
);

