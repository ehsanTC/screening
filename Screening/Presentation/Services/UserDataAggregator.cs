using Screening.Domain.Answers;
using Screening.Domain.Questions.Abstraction;
using Screening.Domain.Services.Profile;
using Screening.Presentation.Services.Abstraction;

namespace Screening.Presentation.Services;

public class UserDataAggregator(IProfileService profileService) : IUserDataAggregator
{
    public async Task<IReadOnlyDictionary<string, Answer>> GetProfileDataAsync()
    {
        var profileData = await profileService.LoadAllAsync();

        return profileData.ToDictionary(
            kv => kv.Key,
            kv => kv.Value.Value);
    }

    public IReadOnlyDictionary<string, Answer> GetQuestionnaireData(IReadOnlyList<IDisplayableQuestion> questions)
    {
        ArgumentNullException.ThrowIfNull(questions);
        return questions.ToDictionary(x => x.Question.Code, x => x.Answer!);
    }

    public IReadOnlyDictionary<string, Answer>? MergeData(params IReadOnlyDictionary<string, Answer>[] questions)
    {
        ArgumentNullException.ThrowIfNull(questions);

        try
        {
            return questions
                .SelectMany(questionnaire => questionnaire)
                .GroupBy(kvp => kvp.Key)
                .ToDictionary(g => g.Key, g => g.Last().Value);
        }
        
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
