using Screening.Domain.Models;
using Screening.Domain.Questions;
using Screening.Domain.Services.Abstraction;

namespace Screening.Domain.Services;

public class QuestionnaireProviderFactory : IQuestionnaireProviderFactory
{
    private readonly ILocalizedFileNameResolver _resolver;
    private readonly IQuestionnaireContentProvider _provider;

    public QuestionnaireProviderFactory(IQuestionnaireContentProvider provider,
        ILocalizedFileNameResolver resolver)
    {
        _provider = provider;
        _resolver = resolver;
    }

    public QuestionnaireEntity? GetProvider(QuestionnaireType questionnaire)
    {
        var questionnaireFile = questionnaire switch
        {
            QuestionnaireType.Baseline => "Baseline.yaml",
            QuestionnaireType.Diabetes => "Diabetes-Type2-questionnaire.yaml",
            QuestionnaireType.Ascvd => "Ascvd-questionnaire.yaml",

            _ => null,
        };
     
        if (questionnaireFile == null)
            return null;
        
        var resolvedFileName = _resolver.Resolve(questionnaireFile);
        return _provider.Read(resolvedFileName);
    }
}
