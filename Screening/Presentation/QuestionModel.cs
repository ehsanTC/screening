using Screening.Domain.Answers;
using Screening.Domain.Models;
using Screening.Domain.Models.Dtos;
using Screening.Domain.Questionnaire;
using Screening.Domain.RiskCalculator.Abstraction;
using Screening.Domain.Services.Abstraction;
using Screening.Domain.Services.Profile;
using Screening.Presentation.Model;

namespace Screening.Presentation;

public partial record QuestionModel
{
    private readonly INavigator _navigator;
    private readonly QuestionnaireSession _session;

    public IState<QuestionState> State { get; }

    public QuestionModel(IProfileService profileService,
        IQuestionnaireProviderFactory factory,
        QuestionnairePageRequest pageRequest,
        IRiskEvaluatorFactory riskCalculatorFactory,
        INavigator navigator)
    {
        ArgumentNullException.ThrowIfNull(factory);

        _session = new QuestionnaireSession(factory, pageRequest, profileService, riskCalculatorFactory);
        _navigator = navigator ?? throw new ArgumentNullException(nameof(navigator));

        State = State<QuestionState>.Value(this, () => _session.State);
    }

    public void Publish() => State.Update(_ => _session.State, CancellationToken.None);

    private async Task ExecuteAsync(Func<Task> command)
    {
        await command();
        Publish();
    }

    public async Task UpdateAnswer(Answer answer) =>
        await ExecuteAsync(() => _session.UpdateAnswer(answer));

    public async Task Next() => await ExecuteAsync(async () =>
    {
        var state = _session.State;

        if (state.IsLastQuestion)
        {
            var result = _session.Evaluate();
            await _navigator.NavigateBackWithResultAsync(this, 
                data: new QuestionnaireOutcome(_session.Questionnaire, result));
            return;
        }

        _session.TryMoveNext();
    });

    public async Task Back() => await ExecuteAsync(() =>
    {
        _session.TryMoveBack();
        return Task.CompletedTask;
    });
}
