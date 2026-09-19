using Screening.Domain.Questions;

namespace Screening.Presentation.Services.Abstraction;

public interface IScreeningPromptBuilder
{
    Task<string> BuildPrompt(QuestionnaireEntity questionnaire);
}
