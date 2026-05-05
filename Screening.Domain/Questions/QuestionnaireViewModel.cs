namespace Screening.Domain.Questions;

public class QuestionnaireViewModel
{
    public string? Title { get; set; }
    public IReadOnlyCollection<IQuestionViewModel>? Questions { get; set; }
}
