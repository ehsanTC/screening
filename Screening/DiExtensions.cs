using Common.Abstraction;
using Common.Services;
using Screening.Domain.Questionnaire.Abstraction;
using Screening.Domain.Questionnaire.InputBuilders;
using Screening.Domain.RiskCalculator.Abstraction;
using Screening.Domain.RiskCalculator.Ascvd;
using Screening.Domain.RiskCalculator.Diabetes;
using Screening.Domain.RiskCalculator.Fracture;
using Screening.Domain.RiskCalculator.General;
using Screening.Domain.RiskCalculator.Obesity;
using Screening.Domain.Services;
using Screening.Domain.Services.Abstraction;
using Screening.Domain.Services.FileStorage;
using Screening.Domain.Services.Profile;
using Screening.Presentation.Dialogs;
using Screening.Presentation.Services;
using Screening.Presentation.Services.Abstraction;
using Screening.Presentation.Utility;

namespace Screening;

public static class DiExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services) => services
        .AddSingleton<IQuestionnaireParser, YamlQuestionnaireParser>()
        .AddSingleton<IQuestionnaireProviderFactory, QuestionnaireProviderFactory>()
        .AddSingleton<IQuestionnaireContentProvider, YamlQuestionnaireContentProvider>()
        .AddSingleton<ISessionStorageService, SessionStorageService>()
        .AddSingleton<IProfileService, ProfileService>()
        .AddRiskInputBuilder()
        .AddRiskEvaluators()
        .AddSingleton<IRiskEvaluatorFactory, RiskEvaluatorFactory>()
        .AddSingleton<ILocalizedFileNameResolver, LocalizedFileNameResolver>()
        .AddSingleton<IConnectivityService, ConnectivityService>()
        .AddSingleton<IAssetFileProvider, AssetFileProvider>()
        .AddSingleton<IAiDescriptionService, AiDescriptionService>()
        .AddSingleton<IChatService, ChatService>()
        .AddSingleton<IPromptBuilderFactory, PromptBuilderFactory>()
        .AddSingleton<IPromptFileSelector, PromptFileSelector>()
        .AddSingleton<IUserDataAggregator, UserDataAggregator>();

    private static IServiceCollection AddRiskInputBuilder(this IServiceCollection services) => services
        .AddSingleton<IRiskEvaluator, DiabetesRiskEvaluator>()
        .AddSingleton<IRiskEvaluator, AscvdRiskEvaluator>()
        .AddSingleton<IRiskEvaluator, HeuristicRiskEvaluator>()
        .AddSingleton<IRiskEvaluator, ObesityRiskEvaluator>()
        .AddSingleton<IRiskEvaluator, GeneralRiskEvaluator>();

    private static IServiceCollection AddRiskEvaluators(this IServiceCollection services) => services
        .AddSingleton<IRiskInputBuilder<AscvdRiskInput>, AscvdRiskInputBuilder>()
        .AddSingleton<IRiskInputBuilder<DiabetesRiskInput>, DiabetesRiskInputBuilder>()
        .AddSingleton<IRiskInputBuilder<FractureRiskInput>, FractureRiskInputBuilder>()
        .AddSingleton<IRiskInputBuilder<ObesityRiskInput>, ObesityRiskInputBuilder>()
        .AddSingleton<IRiskInputBuilder<GeneralRiskInput>, GeneralRiskInputBuilder>();
}
