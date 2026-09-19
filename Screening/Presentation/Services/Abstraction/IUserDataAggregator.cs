using Screening.Domain.Answers;
using Screening.Domain.Questions.Abstraction;

namespace Screening.Presentation.Services.Abstraction;

public interface IUserDataAggregator
{
    Task<IReadOnlyDictionary<string, Answer>> GetProfileDataAsync();
    IReadOnlyDictionary<string, Answer> GetQuestionnaireData(IReadOnlyList<IDisplayableQuestion> questions);
    IReadOnlyDictionary<string, Answer> MergeData(params IReadOnlyDictionary<string, Answer>[] questions);
}
