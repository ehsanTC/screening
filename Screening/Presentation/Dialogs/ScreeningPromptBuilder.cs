using Common.Abstraction;
using Screening.Domain.Extensions;
using Screening.Domain.Questionnaire;
using Screening.Domain.Questions;
using Screening.Domain.Questions.Abstraction;
using Screening.Domain.Services.Profile;
using Screening.Domain.Utilities;

namespace Screening.Presentation.Dialogs;

public class ScreeningPromptBuilder(
    IAssetFileProvider assetProvider,
    ILocalizedFileNameResolver localizedFileNameResolver, 
    IProfileService  profileService)
    : IScreeningPromptBuilder
{
    private readonly ILocalizedFileNameResolver _fileNameResolver = localizedFileNameResolver
        ?? throw new ArgumentNullException(nameof(localizedFileNameResolver));

    private readonly IAssetFileProvider _assetProvider = assetProvider
        ?? throw new ArgumentNullException(nameof(assetProvider));

    public async Task<string> BuildPrompt(QuestionnaireEntity questionnaire)
    {
        var reader = new QuestionnaireAnswerReader(questionnaire);
        var userData = await GetMergedUserData(questionnaire.Questions, reader);

        var answers = userData
            .Where(x => !string.IsNullOrEmpty(x.questionCode) &&
                    !string.IsNullOrEmpty(x.answer))
            .Select(x => $" - {x.questionCode}:{x.answer}");

        var template = FetchPrompt();

        return string.Format(
            template,
            questionnaire.Title,
            string.Join(Environment.NewLine, answers));
    }

    private async Task<IEnumerable<(string questionCode, string answer)>> GetMergedUserData(
        IReadOnlyList<IDisplayableQuestion> questionnaireQuestions,
        QuestionnaireAnswerReader reader)
    {
        var questionnaireAnswers = GetCurrentQuestionnaireData(questionnaireQuestions, reader);
        var profileData = await profileService.LoadAllAsync();

        var result = questionnaireAnswers
            .Select(x => (x.Key, x.Value));

        var profileAnswers = profileData
            .Where(x => !questionnaireAnswers.ContainsKey(x.Key))
            .Select(x => (x.Key, x.Value.Value.ToAiReadableString()));

        return result.Concat(profileAnswers);
    }

    private static Dictionary<string, string> GetCurrentQuestionnaireData(
        IReadOnlyList<IDisplayableQuestion> questions,
        QuestionnaireAnswerReader reader)
    {
        return questions
            .Where(x => TryFetchAnswer(x, reader, out _))
            .ToDictionary(
                x => x.Question.Code,
                x =>
                {
                    TryFetchAnswer(x, reader, out string answer);
                    return answer;
                });
    }

    private string FetchPrompt()
    {
        var languageCode = LanguageHelper.GetCurrentLanguage();
        var localizedFileName = _fileNameResolver.Resolve("AssessmentExplainPrompt.txt", languageCode);
        using var stream = _assetProvider.OpenAsset(localizedFileName);
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }

    private static bool TryFetchAnswer(IDisplayableQuestion question, QuestionnaireAnswerReader reader,
        out string answer)
    {
        answer = String.Empty;
        if (question.Answer is null)
            return false;

        var yamlQuestion = question.Question;
        answer = yamlQuestion.Type.ToLowerInvariant() switch
        {
            "single_choice" => reader.GetSingleItem(yamlQuestion.Code)?.Text ?? string.Empty,
            "string" or "number" => reader.GetString(yamlQuestion.Code),
            "multi_choice" => string.Join("+", reader.GetMultipleItem(yamlQuestion.Code).Select(item => item.Text)),
            _ => throw new NotSupportedException("The question type is not supported.")
        };

        return true;
    }
}
