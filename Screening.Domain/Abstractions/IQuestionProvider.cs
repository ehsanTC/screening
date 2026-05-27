using Screening.Domain.Questions;

namespace Screening.Domain.Abstractions;

public interface IQuestionProvider
{
    QuestionnaireViewModel GetQuestionnaire();
}
