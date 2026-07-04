using Screening.Domain.Abstractions;
using Screening.Domain.Questions;
using Screening.Domain.Services.Abstraction;

namespace Screening.Domain.Services;

public class BaselineQuestionnaireProvider(IQuestionnaireContentProvider provider): IQuestionnaireProvider
{
    private QuestionnaireContext? _questionnaire;

    private static readonly string FileName = "Baseline.yaml";
    public QuestionnaireContext GetQuestionnaire() => _questionnaire ??= provider.Read(FileName);
}
