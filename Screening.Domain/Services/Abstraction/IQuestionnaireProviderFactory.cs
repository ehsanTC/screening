using Screening.Domain.Models;
using Screening.Domain.Questions;

namespace Screening.Domain.Services.Abstraction;

public interface IQuestionnaireProviderFactory
{
    QuestionnaireEntity? GetProvider(QuestionnaireType questionnaire);
}
