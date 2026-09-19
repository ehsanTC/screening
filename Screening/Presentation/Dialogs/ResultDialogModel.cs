using System.Text;
using CommunityToolkit.WinUI.UI.Controls.TextToolbarSymbols;
using Screening.Domain.Answers;
using Screening.Domain.Extensions;
using Screening.Domain.Parser.Models;
using Screening.Domain.RiskCalculator.Abstraction;
using Screening.Presentation.Model;
using Screening.Presentation.Services.Abstraction;
using Screening.Presentation.Utility;

namespace Screening.Presentation.Dialogs;

public partial record ResultDialogModel
{
    private readonly QuestionnaireOutcome _outcome;

    private readonly IConnectivityService _connectivity;
    private readonly IAiDescriptionService _aiService;
    private readonly IPromptBuilderFactory _promptBuilderFactory;
    private readonly IStringLocalizer _localizer;

    public RiskResult Risk => _outcome.RiskResult;

    public bool IsResultEmpty => Risk.IsEmpty;


    public IState<bool> IsAiDescriptionVisible =>
        State<bool>.Value(this, () => IsResultEmpty);

    public IState<string> AiDescription => State<string>.Value(this, () => string.Empty);

    public IFeed<string?> AiPanelText => AiDescription.Select(description =>
        !string.IsNullOrWhiteSpace(description)
            ? description
            : IsResultEmpty ? BuildUserInsertedDataText() : null);
    
    public IFeed<bool> ShowStructuredResult =>
        IsAiDescriptionVisible.Select(isVisible => !IsResultEmpty && !isVisible);

    public IFeed<bool> ShowAiPanel =>
        IsAiDescriptionVisible.Select(isVisible => IsResultEmpty || isVisible);

    public IFeed<bool> CanGoBackToResult =>
        IsAiDescriptionVisible.Select(isVisible => !IsResultEmpty && isVisible);

    public IFeed<bool> IsAiButtonEnabled => Feed<bool>.Async(async ct =>
    {
        var isVisible = await IsAiDescriptionVisible;
        var hasDescription = !string.IsNullOrWhiteSpace(await AiDescription);

        // Empty-result case, already fetched: nothing left to do.
        if (IsResultEmpty && hasDescription) return false;

        // Going back to a real result never needs network.
        if (!IsResultEmpty && isVisible) return true;

        // Otherwise we are about to call the AI service.
        return _connectivity.IsConnected;
    });

    public ResultDialogModel(
        QuestionnaireOutcome outcome,
        IAiDescriptionService aiService,
        IConnectivityService connectivity,
        IPromptBuilderFactory promptBuilderFactory,
        IStringLocalizer localizer)
    {
        _outcome = outcome ?? throw new ArgumentNullException(nameof(outcome));
        _aiService = aiService ?? throw new ArgumentNullException(nameof(aiService));
        _connectivity = connectivity ?? throw new ArgumentNullException(nameof(connectivity));
        _promptBuilderFactory = promptBuilderFactory ?? throw new ArgumentNullException(nameof(promptBuilderFactory));
        _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
    }

    public async ValueTask ToggleAiDescription(CancellationToken ct)
    {
        var isVisible = await IsAiDescriptionVisible;

        // Empty-result case: the panel is always visible.
        // The button's only job here is to fetch the AI text once.
        if (IsResultEmpty)
        {
            await FetchAiDescriptionIfNeededAsync(ct);
            return;
        }

        if (!isVisible)
        {
            await FetchAiDescriptionIfNeededAsync(ct);
            await IsAiDescriptionVisible.UpdateAsync(_ => true, ct);
            return;
        }

        // Going back to the structured result — no fetch needed.
        await IsAiDescriptionVisible.UpdateAsync(_ => false, ct);
    }

    private async ValueTask FetchAiDescriptionIfNeededAsync(CancellationToken ct)
    {
        var cached = await AiDescription;
        if (!string.IsNullOrWhiteSpace(cached)) return;

        var promptBuilderFactory = _promptBuilderFactory.CreatePromptBuilder(_outcome.Questionnaire.Type);
        var prompt = await promptBuilderFactory.BuildPrompt(_outcome.Questionnaire);
        var text = await _aiService.GetDescriptionAsync(prompt, ct);
        await AiDescription.UpdateAsync(_ => text, ct);
    }

    private string BuildUserInsertedDataText()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{_localizer["ResultDialog_NoResultMessage"]}");
        sb.AppendLine();

        foreach (var question in _outcome.Questionnaire.Questions
                     .Where(q => q.Answer is not null))
        {
            sb.AppendLine($"**{question.Question.Text}** ");
            sb.AppendLine(question.Answer?.ToReadableString(AnswerFormatStrategy.AiFriendly));
            sb.AppendLine();
        }

        return sb.ToString();
    }
}
