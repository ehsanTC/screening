using Common.Abstraction;
using Screening.Domain.Extensions;
using Screening.Domain.Models;
using Screening.Domain.Questions;
using Screening.Domain.Utilities;
using Screening.Presentation.Services.Abstraction;

namespace Screening.Presentation.Services;

public class DisorderPromptBuilder(
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
        var questionnaireData = dataAggregator.GetQuestionnaireData(questionnaire.Questions);
        
        var userData = dataAggregator.MergeData(profileData, questionnaireData);

        var answers = userData
            .Where(x => !string.IsNullOrEmpty(x.Key))
            .Select(x => $" - {x.Key}:{x.Value.ToReadableString(AnswerFormatStrategy.AiFriendly)}");

        var template = FetchPrompt(questionnaire.Type);

        return string.Format(
            template,
            questionnaire.Title,
            string.Join(Environment.NewLine, answers));
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
