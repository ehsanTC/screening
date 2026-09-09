using Screening.Domain.Questions;

namespace Screening.Presentation.Dialogs;

public interface IScreeningPromptBuilder
{
    Task<string> BuildPrompt(QuestionnaireEntity questionnaire);
}
