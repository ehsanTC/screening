using Screening.Domain.Answers;
using Screening.Domain.Models;
using Screening.Domain.Services.Abstraction;
using Screening.Presentation.Dtos;
using Screening.Questionnaire;

namespace Screening.Presentation;

public partial record QuestionModel
{
    private readonly QuestionnaireSession _session;

    public IState<QuestionState> State { get; }

    public QuestionModel(IQuestionnaireProviderFactory factory, QuestionnairePageRequest pageRequest)
    {
        ArgumentNullException.ThrowIfNull(factory);

        var provider = factory.GetProvider(pageRequest.Name);
        _session = new QuestionnaireSession(factory, pageRequest);
        State = State<QuestionState>.Value(this, () => _session.State);
    }

    public void Publish() => State.Update(_ => _session.State, CancellationToken.None);

    private void Execute(Action command)
    {
        command();
        Publish();
    }

    public void UpdateAnswer(Answer answer) => 
        Execute(() => _session.UpdateAnswer(answer));

    public void Next()
    {
        var currentState = _session.State;
        if (currentState.IsLastQuestion)
        {
            Calculate();
        }
        else
        {
            Execute(() => _session.TryGoNext());
        }
    }

    private void Calculate()
    {
        throw new NotImplementedException();
    }

    public void Back() => Execute(() => _session.TryGoBack());
}
