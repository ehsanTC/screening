using Screening.Domain.Questions;
using Screening.Domain.Questions.Abstraction;

namespace Screening.Domain.Questionnaire;
    
public sealed class QuestionNavigator
{
    private int _index;
    private readonly QuestionnaireEntity _questionnaire;

    public event EventHandler<SkipEventArgs> QuestionSkipped;

    private void OnQuestionSkipped(SkipEventArgs e) => QuestionSkipped?.Invoke(this, e);

    public QuestionNavigator(QuestionnaireEntity questionnaireEntity)
    {
        _questionnaire = questionnaireEntity ?? throw new ArgumentNullException(nameof(questionnaireEntity));
    }

    public IDisplayableQuestion CurrentQuestion => _questionnaire.Questions[_index];
    public int Index => _index;
    public int TotalSteps => _questionnaire.Questions.Count;

    internal bool IsLastQuestion
    {
        get
        {
            for (var i = _index + 1; i < _questionnaire.Questions.Count; i++)
            {
                if (_questionnaire.ShowQuestionAtIndex(i))
                    return false;
            }

            return true;
        }
    }

    internal bool CanGoNext =>
        _questionnaire.Questions[_index] is
            { IsOptional: true } or
            { IsOptional: false, IsAnswered: true };

    internal bool CanGoBack => _index > 0;

    public QuestionnaireEntity Questionnaire => _questionnaire;

    public bool TryGoNext()
    {
        CurrentQuestion?.Validate();

        if (!CanGoNext)
            return false;

        var candidate = _index + 1;

        while (candidate < _questionnaire.Questions.Count &&
               !_questionnaire.ShowQuestionAtIndex(candidate))
        {
            OnQuestionSkipped(new SkipEventArgs(candidate, _questionnaire.Questions[candidate]));
            candidate++;
        }

        if (candidate >= _questionnaire.Questions.Count)
            return false; // no visible question left to move to

        _index = candidate;
        return true;
    }

    public bool TryGoBack()
    {
        if (!CanGoBack)
            return false;

        var candidate = _index - 1;

        while (candidate >= 0 &&
               !_questionnaire.ShowQuestionAtIndex(candidate))
        {
            candidate--;
        }

        if (candidate < 0)
            return false;

        _index = candidate;
        return true;
    }
}
