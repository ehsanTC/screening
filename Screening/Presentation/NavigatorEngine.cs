using Screening.Domain.Answers;
using Screening.Domain.Models;
using Screening.Domain.Questions;
using Screening.Domain.Services.Abstraction;
using Screening.Presentation.Dtos;

namespace Screening.Presentation;
    
public class NavigatorEngine
{
    private int _index;
    private readonly string _lastPageNextButtonText;
    private readonly string _middlePageNextButtonText;

    private readonly QuestionnaireContext _questionnaire;

    public NavigatorEngine(IQuestionnaireProviderFactory factory, QuestionnairePageRequest request)
    {
        _questionnaire = factory?.GetProvider(request.Name)?.GetQuestionnaire() 
                ?? throw new ArgumentNullException(nameof(factory));
        
        _lastPageNextButtonText = request.LastPageNextButtonText;
        _middlePageNextButtonText = request.MiddlePageNextButtonText;
    }

    public QuestionState State =>
        new (_questionnaire.Questions[_index],
            GetProgression(),
            CanGoNext,
            CanGoBack,
            IsLastQuestion,
            GetNextButtonText());

    private double GetProgression()
    {
        var currentQuestion = _questionnaire.Questions[_index];
        double position = currentQuestion.Question.Id;
        
        return (position + 1) / _questionnaire.Questions.Count * 100;
    }

    private string GetNextButtonText() => 
        !IsLastQuestion ? _middlePageNextButtonText : _lastPageNextButtonText;

    private bool CanGoNext
    {
        get =>
            _questionnaire.Questions[_index] is 
                { IsOptional: true }  or 
                { IsOptional: false, IsAnswered: true } &&
            (IsLastQuestion || _index < _questionnaire.Questions.Count - 1);
    }

    private bool CanGoBack => _index > 0;
    private bool IsLastQuestion => 
        _questionnaire.Questions[_index].Question.Id == _questionnaire.Questions.Count - 1;

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
