using Screening.Domain.Models;
using Screening.Presentation.Dialogs;

namespace Screening.Presentation.Services.Abstraction;

public interface IPromptBuilderFactory
{
    IScreeningPromptBuilder  CreatePromptBuilder(QuestionnaireType type);
}
