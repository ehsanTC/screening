using Screening.Domain.Answers;
using Screening.Domain.Models;
using Screening.Domain.Services.Abstraction;
using Screening.Presentation.Dtos;

namespace Screening.Questionnaire;

public class QuestionnaireSession
{
    private readonly QuestionnairePageRequest _pageRequest;
    private readonly QuestionNavigator _questionNavigator;

    public QuestionnaireSession(IQuestionnaireProviderFactory factory, QuestionnairePageRequest pageRequest)
    {
        _pageRequest = pageRequest;
        _questionNavigator = new QuestionNavigator(factory, pageRequest);
    }

    public QuestionState State => 
        new QuestionStateBuilder()
            .SetLastStepActionText(_pageRequest.LastPageNextButtonText)
            .SetMiddleStepActionText(_pageRequest.MiddlePageNextButtonText)
            .SetNavigator(_questionNavigator)
            .Build();

    public void UpdateAnswer(Answer answer)
    {
        var currentQuestion = _questionNavigator.CurrentQuestion;
        currentQuestion.Answer = answer;

        if (currentQuestion.HasSaveConfigs)
        {
            // Save the question response in his profile
        }
    }

    public bool TryGoNext() => _questionNavigator.TryGoNext();

    public bool TryGoBack() => _questionNavigator.TryGoBack();
}
