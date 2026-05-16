using Screening.Domain.Infrastructure.Abstractions;
using Screening.Domain.Questions;

namespace Screening.Presentation;

public partial record QuestionModel
{
    private readonly QuestionnaireViewModel _questionnaire;
    private int _index;

    public IState<QuestionState> State => _state;
    private readonly IState<QuestionState> _state;

    public QuestionModel(IQuestionProvider provider)
    {
        ArgumentNullException.ThrowIfNull(provider);

        _index = 0;
        _questionnaire = provider.GetQuestionnaire();

        _state = State<QuestionState>.Value(this, CreateState);
    }

    private QuestionState CreateState()
    {
        var current = _questionnaire.ShowQuestionAtIndex(_index)
            ? _questionnaire.Questions[_index]
            : _questionnaire.Questions[--_index];

        return new QuestionState(
            CurrentQuestion: current,
            CanGoNext: current.IsAnswered && _index < _questionnaire.Questions.Count - 1,
            CanGoBack: _index > 0
        );
    }

    public void RefreshState() => 
        _state?.Update(s => CreateState(), CancellationToken.None);

    public void AnswerUpdated() => RefreshState();

    public void UpdateCurrentTextAnswer(string value)
    {
        if (_questionnaire.Questions[_index] is not TextInputDisplayableQuestion question)
            return;

        question.Response = value;
        RefreshState();
    }

    public void Next()
    {
        var questions = _questionnaire.Questions;
        var q = questions[_index];
        q.Validate();

        if (_index < questions.Count - 1)
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
