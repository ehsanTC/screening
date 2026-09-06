using Screening.Domain.Questions;

namespace Screening.Presentation.Dialogs;

public interface IScreeningPromptBuilder
{
    string BuildPrompt(QuestionnaireEntity questionnaire);
}
