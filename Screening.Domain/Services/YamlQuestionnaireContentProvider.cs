using Common.Abstraction;
using Screening.Domain.Questions;
using Screening.Domain.Services.Abstraction;

namespace Screening.Domain.Services;

public class YamlQuestionnaireContentProvider : IQuestionnaireContentProvider
{
    private readonly IQuestionnaireParser _parser;
    private readonly IAssetFileProvider _assetFileProvider;

    public YamlQuestionnaireContentProvider(IQuestionnaireParser parser, IAssetFileProvider assetFileProvider)
    {
        _parser = parser ?? throw new ArgumentNullException(nameof(parser));
        _assetFileProvider = assetFileProvider ?? throw new ArgumentNullException(nameof(assetFileProvider));
    }

    public QuestionnaireEntity Read(string questionnaireName)
    {
        using var stream = _assetFileProvider.OpenAsset(questionnaireName);
        using var reader = new StreamReader(stream);
        var text = reader.ReadToEnd();

        var yamlQuestionnaire = _parser.Parse(text);

        return new QuestionnaireEntity()
        {
            Title = yamlQuestionnaire.Title,
            Type = yamlQuestionnaire.Type,
            Version = yamlQuestionnaire.Version,
            ResultCategories = yamlQuestionnaire.Results,
            Questions = [..yamlQuestionnaire.Questions.Select(QuestionFactory.CreateQuestion)]
        };
    }
}
