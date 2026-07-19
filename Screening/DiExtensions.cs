using Screening.Domain.Questionnaire.Abstraction;
using Screening.Domain.Questionnaire.InputBuilders;
using Screening.Domain.RiskCalculator;
using Screening.Domain.RiskCalculator.Abstraction;
using Screening.Domain.RiskCalculator.Requests;
using Screening.Domain.Services;
using Screening.Domain.Services.Abstraction;
using Screening.Domain.Services.FileStorage;
using Screening.Domain.Services.Profile;

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
        .AddSingleton<IRiskEvaluatorFactory, RiskEvaluatorFactory>();

    private static IServiceCollection AddRiskInputBuilder(this IServiceCollection services) => services
        .AddSingleton<IRiskEvaluator, DiabetesRiskEvaluator>()
        .AddSingleton<IRiskEvaluator, AscvdRiskEvaluator>();

    private static IServiceCollection AddRiskEvaluators(this IServiceCollection services) => services
        .AddSingleton<IRiskInputBuilder<AscvdRiskInput>, AscvdRiskInputBuilder>()
        .AddSingleton<IRiskInputBuilder<DiabetesRiskInput>, DiabetesRiskInputBuilder>();
}
