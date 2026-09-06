using Screening.Domain.Questions;

namespace Screening.Presentation.Dialogs;

internal interface IScreeningPromptBuilder
{
    string BuildPrompt(QuestionnaireEntity questionnaire);
}
