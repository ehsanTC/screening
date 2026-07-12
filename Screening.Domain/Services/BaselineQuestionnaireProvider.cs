using Screening.Domain.Abstractions;
using Screening.Domain.Questions;
using Screening.Domain.Services.Abstraction;

namespace Screening.Domain.Services;

public class BaselineQuestionnaireProvider(IQuestionnaireContentProvider provider): IQuestionnaireProvider
{
    private Questionnaire? _questionnaire;

    private static readonly string FileName = "Baseline.yaml";
    public Questionnaire GetQuestionnaire() => _questionnaire ??= provider.Read(FileName);
}
