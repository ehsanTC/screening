using Screening.Domain.RiskCalculator.Abstraction;
using Screening.Presentation.Model;
using Screening.Presentation.Utility;

namespace Screening.Presentation.Dialogs;


    public partial record ResultDialogModel
    {
        private readonly QuestionnaireOutcome _outcome;
        
        private readonly IConnectivityService _connectivity;
        private readonly IAiDescriptionService _aiService;
        private readonly IScreeningPromptBuilder _promptBuilder;

        public RiskResult Risk => _outcome.RiskResult;

        // Cached AI text. Stays set as long as this dialog instance is open.
        public IState<string?> AiDescription => State<string?>.Value(this, () => null);

        // Which view is showing right now.
        public IState<bool> IsAiDescriptionVisible => State<bool>.Value(this, () => false);

        // Button is enabled if: going back to the result (no network needed),
        // OR we're online and can fetch.
        public IFeed<bool> IsAiButtonEnabled => Feed<bool>.Async(async ct =>
        {
            var isVisible = await IsAiDescriptionVisible;
            return isVisible || _connectivity.IsConnected;
        });

        public ResultDialogModel(QuestionnaireOutcome outcome,
            IAiDescriptionService aiService, 
            IConnectivityService connectivity,
            IScreeningPromptBuilder  promptBuilder)
        {
            _outcome = outcome ?? throw new ArgumentNullException(nameof(outcome));
            _aiService = aiService ?? throw new ArgumentNullException(nameof(aiService));
            _connectivity = connectivity ?? throw new ArgumentNullException(nameof(connectivity));
            _promptBuilder = promptBuilder ?? throw new ArgumentNullException(nameof(promptBuilder));
        }

    public async ValueTask ToggleAiDescription(CancellationToken ct)
    {
        var isVisible = await IsAiDescriptionVisible;

        if (!isVisible)
        {
            var cached = await AiDescription;
            if (string.IsNullOrWhiteSpace(cached))
            {
                var prompt = _promptBuilder.BuildPrompt(_outcome.Questionnaire);
                var text = await _aiService.GetDescriptionAsync(prompt, ct);
                await AiDescription.UpdateAsync(_ => text, ct);
            }
        }

        await IsAiDescriptionVisible.UpdateAsync(v => !v, ct);
    }
}
