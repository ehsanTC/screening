using Screening.Domain.Models;
using Screening.Domain.Questions;
using Screening.Domain.Questions.Abstraction;
using Screening.Domain.Services.Abstraction;

namespace Screening.Questionnaire;
    
public class QuestionNavigator
{
    private int _index;
    private readonly Domain.Questions.Questionnaire _questionnaire;

    public QuestionNavigator(IQuestionnaireProviderFactory factory, QuestionnairePageRequest request)
    {
        _questionnaire = factory?.GetProvider(request.Name)?.GetQuestionnaire() 
                ?? throw new ArgumentNullException(nameof(factory));
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
        _questionnaire.Questions[_index].Question.Id == _questionnaire.Questions.Count - 1;
    
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
}
