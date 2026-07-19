using Screening.Domain.Answers;
using Screening.Domain.Models;
using Screening.Domain.Models.Dtos;
using Screening.Domain.Questionnaire;
using Screening.Domain.RiskCalculator;
using Screening.Domain.Services.Abstraction;
using Screening.Domain.Services.Profile;

namespace Screening.Presentation;

public partial record QuestionModel
{
    private readonly QuestionnaireSession _session;

    public IState<QuestionState> State { get; }

    public QuestionModel(IProfileService profileService,
        IQuestionnaireProviderFactory factory,
        QuestionnairePageRequest pageRequest,
        IRiskEvaluatorFactory riskCalculatorFactory)
    {
        ArgumentNullException.ThrowIfNull(factory);

        _session = new QuestionnaireSession(factory, pageRequest, profileService, riskCalculatorFactory);
        State = State<QuestionState>.Value(this, () => _session.State);
    }

    public void Publish() => State.Update(_ => _session.State, CancellationToken.None);

    private void Execute(Action command)
    {
        command();
        Publish();
    }

    public void UpdateAnswer(Answer answer) => Execute(() => _session.UpdateAnswer(answer));

    public void Next() => Execute(() => _session.GoForward());

    public void Back() => Execute(() => _session.GoBackward());
}
