using Screening.Domain.Abstractions;
using Screening.Domain.Models;
using Screening.Domain.Services.Abstraction;

namespace Screening.Domain.Services;

public class QuestionnaireProviderFactory(IQuestionnaireContentProvider provider) : IQuestionnaireProviderFactory
{
    private readonly IQuestionnaireContentProvider _provider = provider 
            ?? throw new ArgumentNullException(nameof(provider));

    public IQuestionnaireProvider GetProvider(QuestionnaireType questionnaire) =>
        questionnaire switch
        {
            QuestionnaireType.Diabetes => new DiabetesQuestionnaireProvider(_provider),
            QuestionnaireType.Test => new TestQuestionnaireProvider(),
            QuestionnaireType.Baseline => new BaselineQuestionnaireProvider(_provider),
            QuestionnaireType.Ascvd => throw new NotImplementedException(), // TODO

            _ => throw new NotImplementedException(),
        };
}
