using Screening.Domain.Questions;

namespace Screening.Infrastructure.Abstractions;

public interface IQuestionProvider
{
    List<IQuestion> GetQuestions();
}
