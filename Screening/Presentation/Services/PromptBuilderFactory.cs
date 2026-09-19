using Common.Abstraction;
using Screening.Domain.Models;
using Screening.Presentation.Services.Abstraction;

namespace Screening.Presentation.Services;

public class PromptBuilderFactory(
    IAssetFileProvider assetProvider,
    ILocalizedFileNameResolver localizedFileNameResolver, 
    IPromptFileSelector promptFileSelector,
    IUserDataAggregator dataAggregator) : IPromptBuilderFactory
{
    public IScreeningPromptBuilder CreatePromptBuilder(QuestionnaireType type) =>
        type switch
        {
            QuestionnaireType.GeneralEvaluation =>
                new GeneralEvaluationPromptBuilder(assetProvider,
                    localizedFileNameResolver,
                    promptFileSelector,
                    dataAggregator),

            _ => new DisorderPromptBuilder(assetProvider,
                localizedFileNameResolver,
                promptFileSelector,
                dataAggregator)
        };
}
