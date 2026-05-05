using System.Reflection;
using Screening.Domain.Questions;
using Screening.Infrastructure.Abstractions;

namespace Screening.Infrastructure.Services;

public class DiabetesQuestionProvider : IQuestionProvider
{
    private QuestionnaireViewModel? _questionnaire;
    private readonly IQuestionnaireParser _parser;
    private const string DiabetesQuestionnaireFile = "DiabatesType2-questionnaire.yaml";

    private QuestionnaireViewModel PrepareQuestionnaire()
    {
        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream(DiabetesQuestionnaireFile);
        
        if (stream == null)
            throw new ApplicationException("Could not find Diabetes questionnaire.yaml");
        
        using var reader = new StreamReader(stream);
        var text = reader.ReadToEnd();

        var yamlQuestionnaire = _parser.Parse(text);
        return new QuestionnaireViewModel()
        {
            Title = yamlQuestionnaire.Title,
            Questions = [..yamlQuestionnaire.Questions.Select(QuestionFactory.CreateQuestion)]
        };
    }

    public DiabetesQuestionProvider(IQuestionnaireParser parser)
    {
        _parser = parser ?? throw new ArgumentNullException(nameof(parser));
    }

    public QuestionnaireViewModel GetQuestionnaire()
        => _questionnaire ??= PrepareQuestionnaire();
}
