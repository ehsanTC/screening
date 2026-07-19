namespace Screening.Domain.Models;

public record QuestionnairePageRequest(
    QuestionnaireType QuestionnaireType,
    string MiddlePageNextButtonText,
    string LastPageNextButtonText);
