using Screening.Domain.Questions;

namespace Screening.Domain.Abstractions;

public interface IQuestionProvider
{
    QuestionnaireContext GetQuestionnaire();
}
