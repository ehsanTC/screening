using Screening.Domain.Answers;
using Screening.Domain.Models;
using Screening.Domain.Parser.Models;
using Screening.Domain.Services.Abstraction;
using Screening.Presentation.Dtos;
using Screening.Services.Profile;

namespace Screening.Questionnaire;

public class QuestionnaireSession
{
    private readonly IProfileService _profileService;
    private readonly QuestionnairePageRequest _pageRequest;
    private readonly QuestionNavigator _navigator;

    public QuestionnaireSession(IQuestionnaireProviderFactory factory,
        QuestionnairePageRequest pageRequest,
        IProfileService profileService)
    {
        _pageRequest = pageRequest ?? throw new ArgumentNullException(nameof(pageRequest));
        _profileService = profileService ?? throw new ArgumentNullException(nameof(profileService));
        _navigator = new QuestionNavigator(factory, pageRequest);

        OnCurrentQuestionChanged();
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

    public bool TryGoNext()
    {
        if (!_navigator.TryGoNext())
            return false;

        OnCurrentQuestionChanged();
        return true;
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

    public bool TryGoBack() => _navigator.TryGoBack();
}
