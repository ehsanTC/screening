using Screening.Domain.Models;
using Screening.Domain.Questions;
using Screening.Domain.RiskCalculator.Models;
using Screening.Domain.Services;

namespace Screening.Presentation;

public partial record QuestionModel
{
    private readonly NavigatorEngine _engine;
    private readonly QuestionnaireType _type;

    public IState<QuestionState> State { get; }

    public QuestionModel(IQuestionnaireProviderFactory factory, QuestionnaireType type)
    {
        ArgumentNullException.ThrowIfNull(factory);

        _type = type;
        var provider = factory.GetProvider(_type.Name);
        _engine = new NavigatorEngine(provider.GetQuestionnaire());
        State = State<QuestionState>.Value(this, () => _engine.State);
    }

    public void Publish() => State.Update(_ => _engine.State, CancellationToken.None);

    private void Execute(Action command)
    {
        command();
        Publish();
    }

    public void UpdateCurrentTextAnswer(string value) => Execute(() => _engine.SetAnswer(value));

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

    // TODO: Fix the last question. When questions ended, the Next should become "calculate"

    public void Back() => Execute(() => _engine.TryGoBack());
}
