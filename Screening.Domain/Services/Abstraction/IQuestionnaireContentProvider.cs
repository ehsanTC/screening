using Screening.Domain.Questions;

namespace Screening.Domain.Services.Abstraction;

public interface IQuestionnaireContentProvider
{
    QuestionnaireContext Read(string questionnaireName);
}
