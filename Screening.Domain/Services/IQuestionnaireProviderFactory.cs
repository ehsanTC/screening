using Screening.Domain.Abstractions;
using Screening.Domain.Models;

namespace Screening.Domain.Services;

public interface IQuestionnaireProviderFactory
{
    IQuestionnaireProvider GetProvider(QuestionnaireType.Type questionnaire);
}

public class QuestionnaireProviderFactory(IQuestionnaireParser parser) : IQuestionnaireProviderFactory
{
    private readonly IQuestionnaireParser _parser = parser
        ?? throw new ArgumentNullException(nameof(parser));

    public IQuestionnaireProvider GetProvider(QuestionnaireType.Type questionnaire) =>
        questionnaire switch
        {
            QuestionnaireType.Type.Diabetes => new DiabetesQuestionnaireProvider(_parser),
            QuestionnaireType.Type.Test => new TestQuestionnaireProvider(),
            QuestionnaireType.Type.Ascvd => throw new NotImplementedException(), // TODO

            _ => throw new NotImplementedException(),
        };
}
