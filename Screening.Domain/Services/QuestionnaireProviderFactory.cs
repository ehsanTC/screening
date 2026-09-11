using Common.Abstraction;
using Screening.Domain.Models;
using Screening.Domain.Questions;
using Screening.Domain.Services.Abstraction;
using Screening.Domain.Utilities;

namespace Screening.Domain.Services;

public class QuestionnaireProviderFactory(
    IQuestionnaireContentProvider provider,
    ILocalizedFileNameResolver localizer)
    : IQuestionnaireProviderFactory
{
    public QuestionnaireEntity? GetProvider(QuestionnaireType questionnaire)
    {
        var questionnaireFile = questionnaire switch
        {
            QuestionnaireType.Baseline => "Baseline.yaml",
            QuestionnaireType.Diabetes => "Diabetes-Type2-questionnaire.yaml",
            QuestionnaireType.Ascvd =>    "Ascvd-questionnaire.yaml",
            QuestionnaireType.Fracture => "Fracture-questionnaire.yaml",
            QuestionnaireType.Obesity =>  "Obesity-questionnaire.yaml",
            QuestionnaireType.GeneralEvaluation =>  "disorder-evaluate-questionnaire.yaml",            
            _ => null,
        };
     
        if (questionnaireFile == null)
            return null;

        var languageCode = LanguageHelper.GetCurrentLanguage();
        var resolvedFileName = localizer.Resolve(questionnaireFile, languageCode);
        return provider.Read(resolvedFileName);
    }
}
