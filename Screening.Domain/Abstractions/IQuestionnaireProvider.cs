using Screening.Domain.Questions;

namespace Screening.Domain.Abstractions;

public interface IQuestionnaireProvider
{
    QuestionnaireContext GetQuestionnaire();
}
