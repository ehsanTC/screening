using Screening.Domain.Questions;

namespace Screening.Domain.Services.Abstraction;

public interface IQuestionnaireContentProvider
{
    QuestionnaireEntity Read(string questionnaireName);
}
