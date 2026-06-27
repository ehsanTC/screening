using Screening.Domain.Answers;
using Screening.Domain.Questions;

namespace Screening.Presentation;
    
public class NavigatorEngine(QuestionnaireContext questionnaireContext)
{
    private int _index;

    private readonly QuestionnaireContext _questionnaire =
        questionnaireContext ?? throw new ArgumentNullException(nameof(questionnaireContext));

    public QuestionState State =>
        new (_questionnaire.Questions[_index],
            CanGoNext,
            CanGoBack,
            IsFinished);

    private bool CanGoNext =>
        _questionnaire.Questions[_index].IsAnswered 
        && _index < _questionnaire.Questions.Count - 1;

    private bool CanGoBack => _index > 0;
    private bool IsFinished => _index == _questionnaire.Questions.Count - 1;

    public bool TryGoNext()
    {
        State?.CurrentQuestion?.Validate();

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

        var skipIndex = nextIndex + 1;

        if (skipIndex < _questionnaire.Questions.Count)
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

    public void SetAnswer(string text)
    {
        ((TextInputDisplayableQuestion) State?.CurrentQuestion).Answer = new TextAnswer(text);
    }
}
