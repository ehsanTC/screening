using Screening.Domain.Answers;
using Screening.Domain.Models;
using Screening.Domain.Services.Abstraction;
using Screening.Presentation.Dtos;

namespace Screening.Presentation;

public partial record QuestionModel
{
    private readonly NavigatorEngine _engine;

    public IState<QuestionState> State { get; }

    public QuestionModel(IQuestionnaireProviderFactory factory, QuestionnairePageRequest pageRequest)
    {
        ArgumentNullException.ThrowIfNull(factory);

        var provider = factory.GetProvider(pageRequest.Name);
        _engine = new NavigatorEngine(factory, pageRequest);
        State = State<QuestionState>.Value(this, () => _engine.State);
    }

    public void Publish() => State.Update(_ => _engine.State, CancellationToken.None);

    private void Execute(Action command)
    {
        command();
        Publish();
    }

    public void UpdateAnswer(Answer answer) => 
        Execute(() => _engine.UpdateAnswer(answer));

    public void Next()
    {
        var currentState = _engine.State;
        if (currentState.IsLastQuestion)
        {
            Calculate();
        }
        else
        {
            Execute(() => _engine.TryGoNext());
        }
    }

    private void Calculate()
    {
        throw new NotImplementedException();
    }

    public void Back() => Execute(() => _engine.TryGoBack());
}
