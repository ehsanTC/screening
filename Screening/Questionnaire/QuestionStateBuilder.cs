using Screening.Presentation.Dtos;

namespace Screening.Questionnaire;

public class QuestionStateBuilder
{
    private QuestionNavigator _navigator;
    private string _lastPageNextButtonText;
    private string _middlePageNextButtonText;

    public QuestionStateBuilder SetLastStepActionText(string lastPageNextButtonText)
    {
        _lastPageNextButtonText = lastPageNextButtonText;
        return this;
    }

    public QuestionStateBuilder SetMiddleStepActionText(string middlePageNextButtonText)
    {
        _middlePageNextButtonText = middlePageNextButtonText;
        return this;
    }

    public QuestionState Build() =>
        new(_navigator.CurrentQuestion,
            GetProgression(_navigator),
            _navigator.CanGoNext,
            _navigator.CanGoBack,
            _navigator.IsLastQuestion,
            GetNextButtonText());

    private string GetNextButtonText() =>
        !_navigator.IsLastQuestion ? _middlePageNextButtonText : _lastPageNextButtonText;

    private ProgressionState GetProgression(QuestionNavigator navigator)
    {
        int position = navigator.Index + 1;

        var percent = (double)position / navigator.TotalSteps * 100;
        return new ProgressionState(percent, position, navigator.TotalSteps);
    }

    public QuestionStateBuilder SetNavigator(QuestionNavigator questionNavigator)
    {
        _navigator = questionNavigator ?? throw new ArgumentNullException(nameof(questionNavigator));
        return this;
    }
}
