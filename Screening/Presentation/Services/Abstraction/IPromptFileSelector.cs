using Screening.Domain.Models;

namespace Screening.Presentation.Services.Abstraction;

public interface IPromptFileSelector
{
    string GetFileName(QuestionnaireType type);
}

public class PromptFileSelector : IPromptFileSelector
{
    public string GetFileName(QuestionnaireType type) =>
        type switch
        {
            QuestionnaireType.GeneralEvaluation => "GeneralHealthAssessmentPrompt.txt",
            
            _ => "AssessmentExplainPrompt.txt"
        };
}
