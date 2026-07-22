using Screening.Domain.Questions;
using Screening.Domain.Questions.Abstraction;

namespace Screening.Domain.Questionnaire;
    
public sealed class QuestionNavigator
{
    private int _index;
    private readonly QuestionnaireEntity _questionnaire;

    public event EventHandler<SkipEventArgs> QuestionSkipped;

    private void OnQuestionSkipped(SkipEventArgs e)
    {
        QuestionSkipped?.Invoke(this, e);
    }

    public QuestionNavigator(QuestionnaireEntity questionnaireEntity)
    {
        _questionnaire = questionnaireEntity ?? throw new ArgumentNullException(nameof(questionnaireEntity));
    }

    public IDisplayableQuestion CurrentQuestion => _questionnaire.Questions[_index];
    public int Index => _index;
    public int TotalSteps => _questionnaire.Questions.Count;
    
    internal bool CanGoNext
    {
        get =>
            _questionnaire.Questions[_index] is 
                { IsOptional: true }  or 
                { IsOptional: false, IsAnswered: true } &&
            (IsLastQuestion || _index < _questionnaire.Questions.Count - 1);
    }

    internal bool CanGoBack => _index > 0;

    internal bool IsLastQuestion =>
        // TODO: Bad idea of using question's Id
        _questionnaire.Questions[_index].Question.Id >= _questionnaire.Questions.Count;

    public QuestionnaireEntity Questionnaire => _questionnaire;

    public bool TryGoNext()
    {
        CurrentQuestion?.Validate();

        if (!CanGoNext)
            return false;
        
        var nextIndex = _index + 1;

        if (nextIndex >= _questionnaire.Questions.Count)
            return false;

        if (_questionnaire.ShowQuestionAtIndex(nextIndex))
        {
            _index = nextIndex;
            return true;
        }

        // the next question can't be shown, so go to the next one
        var skipIndex = nextIndex + 1;
        
        if (skipIndex >= _questionnaire.Questions.Count)
            return true;

        OnQuestionSkipped(new SkipEventArgs(nextIndex, _questionnaire.Questions[nextIndex]));
        _index = skipIndex;

        return true;
    }

    public bool TryGoBack()
    {
        if (!CanGoBack)
            return false;

        var prevIndex = _index - 1;

        if (prevIndex < 0)
            return false;

        if (_questionnaire.ShowQuestionAtIndex(prevIndex))
        {
            _index = prevIndex;
        }
        else
        {
            var skipIndex = prevIndex - 1;

            if (skipIndex >= 0)
                _index = skipIndex;
        }
        
        return true;
    }
}
