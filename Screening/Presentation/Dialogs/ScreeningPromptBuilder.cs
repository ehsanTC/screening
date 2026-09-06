using Common.Abstraction;
using Screening.Domain.Questionnaire;
using Screening.Domain.Questions;
using Screening.Domain.Questions.Abstraction;
using Screening.Domain.Utilities;

namespace Screening.Presentation.Dialogs;

public class ScreeningPromptBuilder(
    IAssetFileProvider assetProvider,
    ILocalizedFileNameResolver localizedFileNameResolver)
    : IScreeningPromptBuilder
{
    private readonly ILocalizedFileNameResolver _fileNameResolver = localizedFileNameResolver
        ?? throw new ArgumentNullException(nameof(localizedFileNameResolver));

    private readonly IAssetFileProvider _assetProvider = assetProvider
        ?? throw new ArgumentNullException(nameof(assetProvider));

    public string BuildPrompt(QuestionnaireEntity questionnaire)
    {
        var reader = new QuestionnaireAnswerReader(questionnaire);

        var questionAndAnswers = new List<string>(questionnaire.Questions.Count);
        foreach (var question in questionnaire.Questions)
        {
            if (TryFetchAnswer(question, reader, out string answer))
            {
                questionAndAnswers.Add($" - {question.Question.Code}:{answer}{Environment.NewLine}");
            }
        }

        var template = FetchPrompt();
        return string.Format(template, questionnaire.Title, string.Join(",", questionAndAnswers));
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
