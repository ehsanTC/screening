using Screening.Domain.Questions;

namespace Screening.Domain.Abstractions;

public interface IQuestionnaireProvider
{
    Questionnaire GetQuestionnaire();
}
