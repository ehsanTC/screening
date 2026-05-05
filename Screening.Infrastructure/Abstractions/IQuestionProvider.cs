using Screening.Domain.Questions;

namespace Screening.Infrastructure.Abstractions;

public interface IQuestionProvider
{
    QuestionnaireViewModel GetQuestionnaire();
}
