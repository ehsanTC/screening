using Windows.Globalization;
using Screening.Domain.Models;
using Screening.Presentation.Dialogs;
using Screening.Presentation.Model;
using Screening.Presentation.Utility;

namespace Screening;

public partial class App : Application
{
    private const string LanguageTag = "fa";

    public App()
    {
        ApplyLanguage(LanguageTag);
        this.InitializeComponent();
    }

    public static IServiceProvider Services { get; private set; } = default!;
    protected Window? MainWindow { get; private set; }
    protected IHost? Host { get; private set; }

    protected async override void OnLaunched(LaunchActivatedEventArgs args)
    {
        var builder = this.CreateBuilder(args)
            .UseToolkitNavigation()
            .Configure(host => host
#if DEBUG
                .UseEnvironment(Environments.Development)
#endif
                .UseLogging(configure: (context, logBuilder) =>
                {
                    logBuilder
                        .SetMinimumLevel(
                            context.HostingEnvironment.IsDevelopment() ? LogLevel.Information : LogLevel.Warning)
                        .CoreLogLevel(LogLevel.Warning);
                }, enableUnoLogging: true)
                .UseConfiguration(configure: configBuilder =>
                    configBuilder
                        .EmbeddedSource<App>()
                )
                // Enable localization (see appsettings.json for supported languages)
                .UseLocalization()
                .UseHttp((context, services) =>
                {
#if DEBUG
                    services.AddTransient<DelegatingHandler, DebugHttpHandler>();
#endif
                })
                .ConfigureServices((context, services) =>
                {
                    services.ConfigureHttpClientServices();
                    services.ConfigureServices();
                })
                .UseNavigation(ReactiveViewModelMappings.ViewModelMappings, RegisterRoutes)
                .UseStorage()
            );
        MainWindow = builder.Window;

#if DEBUG
        MainWindow.UseStudio();
#endif
        MainWindow.SetWindowIcon();

        Host = await builder.NavigateAsync<Shell>();
        
        if (builder.Window.Content is FrameworkElement rootElement)
        {
            rootElement.FlowDirection = DirectionHelper.Direction;
        }

        Services = Host.Services;
    }

    private static void RegisterRoutes(IViewRegistry views, IRouteRegistry routes)
    {
        views.Register(
            new ViewMap(ViewModel: typeof(ShellModel)),
            new ViewMap<MainPage, MainModel>(),
            new DataViewMap<QuestionPage, QuestionModel, QuestionnairePageRequest>(),
            new DataViewMap<ResultDialog, ResultDialogModel, QuestionnaireOutcome>()
        );

        routes.Register(
            new RouteMap("", View: views.FindByViewModel<ShellModel>(),
                Nested:
                [
                    new RouteMap("Main",
                        View: views.FindByViewModel<MainModel>(),
                        IsDefault: true),
                    new RouteMap("Questions",
                        View: views.FindByViewModel<QuestionModel>()),
                ]
            )
        );
    }

    private static void ApplyLanguage(string languageTag, FrameworkElement? rootElement = null)
    {
        ApplicationLanguages.PrimaryLanguageOverride = languageTag;
        DirectionHelper.Initialize(languageTag); 
    }
}
