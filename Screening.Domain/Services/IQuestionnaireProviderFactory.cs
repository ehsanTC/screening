using Screening.Domain.Abstractions;

namespace Screening.Domain.Services;

public interface IQuestionnaireProviderFactory
{
    IQuestionnaireProvider GetProvider(string questionnaire);
}

public class QuestionnaireProviderFactory(IQuestionnaireParser parser) : IQuestionnaireProviderFactory
{
    private readonly IQuestionnaireParser _parser = parser
        ?? throw new ArgumentNullException(nameof(parser));

    public IQuestionnaireProvider GetProvider(string questionnaire) =>
        questionnaire.Trim().ToLowerInvariant() switch
        {
            "diabetes" => new DiabetesQuestionnaireProvider(_parser),
            "test" => new TestQuestionnaireProvider(),
            "ascvd" => throw new NotImplementedException(),

            _ => throw new NotImplementedException(),
        };
}
