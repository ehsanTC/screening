using System.Net;
using System.Net.Http.Headers;
using Screening.Domain.RiskCalculator.Fracture.Utility;

namespace Screening;

public static class HttpClientRegistration
{
    public static IServiceCollection ConfigureHttpClientServices(this IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddFraxClientConfigs();
        
        return services;
    }

    private static void AddFraxClientConfigs(this IServiceCollection services)
    {
        var cookieContainer = new CookieContainer();
        services.AddSingleton(cookieContainer);

        services.AddHttpClient<FractureRiskApiClient>(client =>
            {
                client.BaseAddress = new Uri("https://www.fraxplus.org");
                client.DefaultRequestHeaders.UserAgent.ParseAdd(
                    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/152.0.0.0 Safari/537.36");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            })
            .ConfigurePrimaryHttpMessageHandler(sp => new HttpClientHandler
            {
                CookieContainer = sp.GetRequiredService<CookieContainer>(),
                UseCookies = true,
            });
    }
}
