using Screening.Domain.Questions;

namespace Screening.Domain.Services.Abstraction;

public interface IQuestionnaireContentProvider
{
    Questionnaire Read(string questionnaireName);
}
