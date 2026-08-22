using System.Reflection;
using Screening.Domain.Questions;
using Screening.Domain.Services.Abstraction;

namespace Screening.Domain.Services;

public class YamlQuestionnaireContentProvider(IQuestionnaireParser parser) : IQuestionnaireContentProvider
{
    private readonly IQuestionnaireParser _parser = parser 
        ?? throw new ArgumentNullException(nameof(parser));
   
    public QuestionnaireEntity Read(string questionnaireName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream(questionnaireName);
        
        if (stream == null)
            throw new ApplicationException("Could not find Diabetes questionnaire.yaml");
        
        using var reader = new StreamReader(stream);
        var text = reader.ReadToEnd();

        var yamlQuestionnaire = _parser.Parse(text);
        
        return new QuestionnaireEntity()
        {
            Title = yamlQuestionnaire.Title,
            Version = yamlQuestionnaire.Version,
            ResultCategories = yamlQuestionnaire.Results,
            Questions = [..yamlQuestionnaire.Questions.Select(QuestionFactory.CreateQuestion)]
        };
    }
}
