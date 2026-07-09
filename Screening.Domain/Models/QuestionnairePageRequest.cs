namespace Screening.Domain.Models;

public record QuestionnairePageRequest(QuestionnaireType Name,
    string MiddlePageNextButtonText,
    string LastPageNextButtonText)
{
}
