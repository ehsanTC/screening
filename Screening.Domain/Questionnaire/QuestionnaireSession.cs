using Screening.Domain.Answers;
using Screening.Domain.Models;
using Screening.Domain.Models.Dtos;
using Screening.Domain.Parser.Models;
using Screening.Domain.RiskCalculator;
using Screening.Domain.Services.Abstraction;
using Screening.Domain.Services.Profile;

namespace Screening.Domain.Questionnaire;

public class QuestionnaireSession
{
    private readonly QuestionNavigator _navigator;
    private readonly IProfileService _profileService;
    private readonly QuestionnairePageRequest _pageRequest;
    private readonly IRiskEvaluatorFactory _riskEvaluatorFactory;

    public QuestionnaireSession(IQuestionnaireProviderFactory factory,
        QuestionnairePageRequest pageRequest,
        IProfileService profileService,
        IRiskEvaluatorFactory riskEvaluatorFactory)
    {
        _pageRequest = pageRequest ?? throw new ArgumentNullException(nameof(pageRequest));
        _profileService = profileService ?? throw new ArgumentNullException(nameof(profileService));
        _riskEvaluatorFactory = riskEvaluatorFactory ?? throw new ArgumentNullException(nameof(riskEvaluatorFactory));

        var questionnaire = factory?.GetProvider(pageRequest.QuestionnaireType)?.GetQuestionnaire()
                            ?? throw new ArgumentNullException(nameof(factory));
        
        _navigator = new QuestionNavigator(questionnaire);
        _navigator.QuestionSkipped += OnQuestionSkipped;

        OnCurrentQuestionChanged();
    }

    private void OnQuestionSkipped(object? sender, SkipEventArgs e)
    {
        // Why update the answer of skipped question?
        // Set default value to it, in order to be used in the calculation
        var skippedQuestion = e.Question;
        skippedQuestion.OnSkipped();
    }

    private void OnCurrentQuestionChanged()
    {
        Task.Run(async () =>
        {
            if (await TryLoadCurrentQuestionAnswerAsync() is { } answer)
                UpdateCurrentAnswer(answer);
        });
    }

    public QuestionState State => new QuestionStateFactory().Create(
        _navigator,
        _pageRequest.MiddlePageNextButtonText,
        _pageRequest.LastPageNextButtonText);

    public async Task UpdateAnswer(Answer answer)
    {
        UpdateCurrentAnswer(answer);
        await TrySaveAnswer(answer);
    }

    private async Task<bool> TrySaveAnswer(Answer answer)
    {
        var currentQuestion = _navigator.CurrentQuestion;
        if (!currentQuestion.HasSaveConfigs)
            return false;

        var saveConfig = currentQuestion.Question.SaveConfig;

        if (saveConfig != null)
            await _profileService.SaveAsync(saveConfig.Path, new SavableAnswer(answer, saveConfig));

        return true;
    }

    private void UpdateCurrentAnswer(Answer answer)
    {
        var currentQuestion = _navigator.CurrentQuestion;
        currentQuestion.Answer = answer;
    }

    public void GoForward()
    {
        if (_navigator.Finished)
        {
            var result = Calculate();

            return;
        }

        _navigator.TryGoNext();
        OnCurrentQuestionChanged();
    }

    private object Calculate()
    {
        try
        {
            var riskEvaluator = _riskEvaluatorFactory.Create(_pageRequest.QuestionnaireType);

            return riskEvaluator?.Evaluate(_navigator.Questionnaire);
        }
        catch
        {
            return null;
        }
    }

    private async Task<Answer?> TryLoadCurrentQuestionAnswerAsync()
    {
        var question = _navigator.CurrentQuestion;
        if (!question.HasSaveConfigs)
            return null;

        var key = question.Question.SaveConfig.Path;
        var savedAnswer = await _profileService.LoadAsync(key);

        return savedAnswer?.Value;
    }

    public void GoBackward() => _navigator.TryGoBack();
}
