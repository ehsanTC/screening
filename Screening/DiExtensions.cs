using Screening.Domain.RiskCalculator;
using Screening.Domain.RiskCalculator.Abstraction;
using Screening.Domain.Services;
using Screening.Domain.Services.Abstraction;
using Screening.Services.FileStorage;
using Screening.Services.Profile;

namespace Screening;

public static class DiExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        return services
            .AddSingleton<IQuestionnaireParser, YamlQuestionnaireParser>()
            .AddSingleton<IQuestionnaireProviderFactory, QuestionnaireProviderFactory>()
            .AddSingleton<IQuestionnaireContentProvider, YamlQuestionnaireContentProvider>()
            .AddSingleton<ISessionStorageService, SessionStorageService>()
            .AddSingleton<IProfileService, ProfileService>()
            .AddSingleton<IRiskCalculatorFactory, RiskCalculatorFactory>();
    }
}
