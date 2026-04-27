using Screening.Domain.Questions;
using Screening.Infrastructure.Abstractions;

namespace Screening.Infrastructure.Services;

public class DiabetesQuestionProvider : IQuestionProvider
{
    public List<IQuestion> GetQuestions() =>
    [
        new TextInputQuestion() {Text = "Name"},
        new TextInputQuestion() {Text = "Age", IsNumeric = true, MinValue = 1, MaxValue = 100},
        new TextInputQuestion() {Text = "Description"},
        new TextInputQuestion() {Text = "Date of birth"},
    ];
}
