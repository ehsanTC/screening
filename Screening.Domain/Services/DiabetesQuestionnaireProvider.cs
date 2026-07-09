using Screening.Domain.Abstractions;
using Screening.Domain.Questions;
using Screening.Domain.Services.Abstraction;

namespace Screening.Domain.Services;

public class DiabetesQuestionnaireProvider(IQuestionnaireContentProvider provider) : IQuestionnaireProvider
{
    private QuestionnaireContext? _questionnaire;

    private static readonly string DiabetesQuestionnaireFile =
        "DiabetesType2-questionnaire.yaml";

    public QuestionnaireContext GetQuestionnaire()
        => _questionnaire ??= provider.Read(DiabetesQuestionnaireFile);
}
