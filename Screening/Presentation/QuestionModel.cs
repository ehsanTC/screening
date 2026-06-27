using Screening.Domain.Abstractions;
using Screening.Domain.Questions;

namespace Screening.Presentation;

public partial record QuestionModel
{
    private readonly QuestionnaireContext _questionnaire;
    private int _index;

    public IState<QuestionState> State => _state;
    private readonly IState<QuestionState> _state;

    public QuestionModel(IQuestionProvider provider)
    {
        ArgumentNullException.ThrowIfNull(provider);

        _index = 0;
        _questionnaire = provider.GetQuestionnaire();

        _state = State<QuestionState>.Value(this, () => CreateState());
    }

    private QuestionState CreateState(MoveDirection direction = MoveDirection.None)
    {
        if (direction != MoveDirection.None && !_questionnaire.ShowQuestionAtIndex(_index))
        {
            _index += direction == MoveDirection.Forward ? 1 : -1;
        }

        var current = _questionnaire.Questions[_index];

        return new QuestionState(
            CurrentQuestion: current,
            CanGoNext: current.IsAnswered && _index < _questionnaire.Questions.Count - 1,
            CanGoBack: _index > 0,
            Finished: _index == _questionnaire.Questions.Count
        );
    }

    public void RefreshState() => 
        _state?.Update(s => CreateState(), CancellationToken.None);
    
    private void RefreshState(MoveDirection direction) =>
        _state?.Update(_ => CreateState(direction), CancellationToken.None);
    
    public void AnswerUpdated() => RefreshState();

    public void Publish() => AnswerUpdated();

    public void Next()
    {
        var questions = _questionnaire.Questions;
        var q = questions[_index];
        q.Validate();

        if (_index < questions.Count - 1)
        {
            _index++;
            RefreshState(MoveDirection.Forward);
        }
    }

    // TODO: Fix the last question. When questions ended, the Next should become "calculate"

    public void Back()
    {
        if (_index > 0)
        {
            _index--;
            RefreshState(MoveDirection.Backward);
        }
    }

    private enum MoveDirection
    {
        None,
        Forward,
        Backward
    }
}
