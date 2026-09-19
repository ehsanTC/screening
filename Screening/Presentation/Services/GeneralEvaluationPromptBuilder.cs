using System.Text;
using Common.Abstraction;
using Screening.Domain.Answers;
using Screening.Domain.Extensions;
using Screening.Domain.Models;
using Screening.Domain.Questions;
using Screening.Domain.Utilities;
using Screening.Presentation.Services.Abstraction;

namespace Screening.Presentation.Services;

public class GeneralEvaluationPromptBuilder (
    IAssetFileProvider assetProvider,
    ILocalizedFileNameResolver localizedFileNameResolver, 
    IPromptFileSelector promptFileSelector,
    IUserDataAggregator dataAggregator)
    : IScreeningPromptBuilder
{
    private readonly ILocalizedFileNameResolver _fileNameResolver = localizedFileNameResolver
        ?? throw new ArgumentNullException(nameof(localizedFileNameResolver));

    private readonly IAssetFileProvider _assetProvider = assetProvider
        ?? throw new ArgumentNullException(nameof(assetProvider));

    public async Task<string> BuildPrompt(QuestionnaireEntity questionnaire)
    {
        var profileData = await dataAggregator.GetProfileDataAsync();
        var currentQuestionnaire = dataAggregator.GetQuestionnaireData(questionnaire.Questions);
        
        var interestedDisease = GetInterestedDisease(currentQuestionnaire);
        var template = FetchPrompt(questionnaire.Type);
        
        return string.Format(
            template,
            Format(profileData),
            interestedDisease);
    }

    private static string GetInterestedDisease(IReadOnlyDictionary<string, Answer> answers)
    {
        StringBuilder  builder = new();
        foreach (var answer in answers)
        {
            if (answer.Value is MultiOptionAnswer moAnswer)
            {
                moAnswer.SelectedOptions.ForEach(option => builder.AppendLine($" • {option.Text}"));
            }
        }    
        
        return builder.ToString();
    }

    private static string Format(IReadOnlyDictionary<string, Answer> keyValues)
    {
        var answers = keyValues
            .Where(x => !string.IsNullOrEmpty(x.Key) && x.Value != null)
            .Select(x =>
            {
                if (x.Value is not RepeatableKeyValueAnswer rkvAnswer)
                {
                    return $" • {x.Key}:{x.Value.ToReadableString(AnswerFormatStrategy.AiFriendly)}";
                }

                var stringBuilder = new StringBuilder();
                rkvAnswer.Disorders.ForEach(d => stringBuilder.AppendLine($" • Have {d}"));
                rkvAnswer.LabResults.ForEach(lr => 
                    stringBuilder.AppendLine($" • {lr.Key} : {lr.Value}"));
                    
                return stringBuilder.ToString();

            })
            .ToList();
        
        return string.Join(Environment.NewLine, answers);
    }

    private string FetchPrompt(QuestionnaireType type)
    {
        var languageCode = LanguageHelper.GetCurrentLanguage();
        var promptFile = promptFileSelector.GetFileName(type);
        var localizedFileName = _fileNameResolver.Resolve(promptFile, languageCode);

        using var stream = _assetProvider.OpenAsset(localizedFileName);
        using var reader = new StreamReader(stream);
        
        return reader.ReadToEnd();
    }
}

