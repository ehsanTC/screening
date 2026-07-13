using Screening.Presentation.Dtos;

namespace Screening.Questionnaire;

public class QuestionStateFactory
{
    public QuestionState Create(QuestionNavigator navigator, string middlePageNextButtonText, 
        string lastPageNextButtonText) =>
        new(navigator.CurrentQuestion,
            GetProgression(navigator),
            navigator.CanGoNext,
            navigator.CanGoBack,
            navigator.IsLastQuestion,
            GetNextButtonText(navigator, middlePageNextButtonText, lastPageNextButtonText));

    private string GetNextButtonText(QuestionNavigator navigator, 
        string middlePageNextButtonText,
        string lastPageNextButtonText) =>
        !navigator.IsLastQuestion ? middlePageNextButtonText : lastPageNextButtonText;

    private ProgressionState GetProgression(QuestionNavigator navigator)
    {
        int position = navigator.Index + 1;

        var percent = (double)position / navigator.TotalSteps * 100;
        return new ProgressionState(percent, position, navigator.TotalSteps);
    }
}
