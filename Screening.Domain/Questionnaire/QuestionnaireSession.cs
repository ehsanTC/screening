using Screening.Domain.Answers;
using Screening.Domain.Models;
using Screening.Domain.Models.Dtos;
using Screening.Domain.Parser.Models;
using Screening.Domain.RiskCalculator.Abstraction;
using Screening.Domain.Services.Abstraction;
using Screening.Domain.Services.Profile;

namespace Screening.Domain.Questionnaire;

public class QuestionnaireSession
{
    private readonly QuestionNavigator _navigator;
    private readonly IProfileService _profileService;
    private readonly QuestionnairePageRequest _pageRequest;
    private readonly IRiskEvaluatorFactory _riskEvaluatorFactory;

    public QuestionnaireSession(
        IQuestionnaireProviderFactory factory,
        QuestionnairePageRequest pageRequest,
        IProfileService profileService,
        IRiskEvaluatorFactory riskEvaluatorFactory)
    {
        _pageRequest = pageRequest ?? throw new ArgumentNullException(nameof(pageRequest));
        _profileService = profileService ?? throw new ArgumentNullException(nameof(profileService));
        _riskEvaluatorFactory = riskEvaluatorFactory ?? throw new ArgumentNullException(nameof(riskEvaluatorFactory));

        var questionnaire = factory?.GetProvider(pageRequest.QuestionnaireType)
                            ?? throw new ArgumentNullException(nameof(factory));

        _navigator = new QuestionNavigator(questionnaire);
        _navigator.QuestionSkipped += OnQuestionSkipped;

        OnCurrentQuestionChanged();
    }

    public QuestionState State =>
        new QuestionStateFactory().Create(
            _navigator,
            _pageRequest.MiddlePageNextButtonText,
            _pageRequest.LastPageNextButtonText);

    public bool TryMoveNext()
    {
        if (!_navigator.TryGoNext())
            return false;

        OnCurrentQuestionChanged();
        return true;
    }

    public bool TryMoveBack()
    {
        if (!_navigator.TryGoBack())
            return false;

        OnCurrentQuestionChanged();
        return true;
    }

    public async Task UpdateAnswer(Answer answer)
    {
        UpdateCurrentAnswer(answer);
        await TrySaveAnswer(answer);
    }

    public RiskResult Evaluate()
    {
        var evaluator = _riskEvaluatorFactory.Create(_pageRequest.QuestionnaireType);

        if (evaluator == null)
            throw new InvalidOperationException(
                $"No evaluator registered for '{_pageRequest.QuestionnaireType}'.");

        return evaluator.Evaluate(_navigator.Questionnaire);
    }

    private async Task<bool> TrySaveAnswer(Answer answer)
    {
        var question = _navigator.CurrentQuestion;

        if (!question.HasSaveConfigs)
            return false;

        var saveConfig = question.Question.SaveConfig!;

        await _profileService.SaveAsync(
            saveConfig.Path,
            new SavableAnswer(answer, saveConfig));

        return true;
    }

    private void UpdateCurrentAnswer(Answer answer) =>
        _navigator.CurrentQuestion.Answer = answer;

    private async Task<Answer?> TryLoadCurrentQuestionAnswerAsync()
    {
        var question = _navigator.CurrentQuestion;

        if (!question.HasSaveConfigs)
            return null;

        var key = question.Question.SaveConfig!.Path;
        var savedAnswer = await _profileService.LoadAsync(key);

        return savedAnswer?.Value;
    }

    private void OnCurrentQuestionChanged()
    {
        Task.Run(async () =>
        {
            var answer = await TryLoadCurrentQuestionAnswerAsync();

            if (answer != null)
                UpdateCurrentAnswer(answer);
        });
    }

    private void OnQuestionSkipped(object? sender, SkipEventArgs e) =>
        e.Question.OnSkipped();
}
