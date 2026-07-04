using Screening.Domain.Abstractions;
using Screening.Domain.Models;

namespace Screening.Domain.Services.Abstraction;

public interface IQuestionnaireProviderFactory
{
    IQuestionnaireProvider GetProvider(QuestionnaireType questionnaire);
}
