using Screening.Domain.Questions;
using Screening.Infrastructure.Abstractions;

namespace Screening.Presentation;

public partial record QuestionModel
{
    private readonly List<IDisplayableQuestion> _questions;
    private int _index;

    public IState<QuestionState> State => _state;
    private readonly IState<QuestionState> _state;

    public QuestionModel(IQuestionProvider provider)
    {
        ArgumentNullException.ThrowIfNull(provider);

        _index = 0;
        _questions = provider.GetQuestionnaire()?.Questions?.ToList();

        _state = State<QuestionState>.Value(this, CreateState);
    }

    private QuestionState CreateState()
    {
        var current = _questions[_index];

        return new QuestionState(
            CurrentQuestion: current,
            CanGoNext: current.IsAnswered && _index < _questions.Count - 1,
            CanGoBack: _index > 0
        );
    }

    public void RefreshState()
    {
        _state.Update(s => CreateState(), CancellationToken.None);
    }

    public void AnswerUpdated() => RefreshState();

    public void UpdateCurrentTextAnswer(string value)
    {
        if (_questions[_index] is TextInputDisplayableQuestion question)
        {
            question.Response = value;
            RefreshState();
        }
    }

    public void Next()
    {
        var q = _questions[_index];
        q.Validate();

        if (_index < _questions.Count - 1)
        {
            _index++;
            RefreshState();
        }
    }
    
    // TODO: Fix the last question. When questions ended, the Next should become "calculate"

    public void Back()
    {
        if (_index > 0)
        {
            _index--;
            RefreshState();
        }
    }
}
