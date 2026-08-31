using Screening.Domain.Extensions;
using Screening.Domain.Models;
using Screening.Domain.Questionnaire;
using Screening.Domain.Questionnaire.Abstraction;
using Screening.Domain.Questions;
using Screening.Domain.RiskCalculator.Abstraction;
using Screening.Domain.RiskCalculator.Fracture.Model;
using Screening.Domain.RiskCalculator.Fracture.Utility;

namespace Screening.Domain.RiskCalculator.Fracture;

public class FractureRiskEvaluator : IRiskEvaluator
{
    private const string FraxApiAddress = "https://www.fraxplus.org/api/v1/frax";
    
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IRiskInputBuilder<FractureRiskInput> _inputBuilder;
    
    public QuestionnaireType QuestionnaireType => QuestionnaireType.Fracture;

    public FractureRiskEvaluator(
        IRiskInputBuilder<FractureRiskInput> inputBuilder,
        IHttpClientFactory httpClientFactory)
    {
        _inputBuilder = inputBuilder ?? throw new ArgumentNullException(nameof(inputBuilder));
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
    }
    
    public RiskResult? Evaluate(QuestionnaireEntity questionnaire)
    {
        var reader = new QuestionnaireAnswerReader(questionnaire);
        var input = _inputBuilder.Build(reader);
        
        var scores = CalculateFraxScore(input);
        return CreateResult(questionnaire, scores);
    }

    private RiskResult? CreateResult(QuestionnaireEntity questionnaire, FraxScore? scores)
    {
        if (scores == null)
            return null;

        var hipScore = scores.Hip.Raw;
        var mofScore = scores.MajorOsteoporosisFracture.Raw;
        
        var category = questionnaire.GetCategory(mofScore);
        
        if (category == null)
            return null;

        var riskResult = new RiskResult(
            mofScore.ToString(),
            category.GetSeverity(),
            Summary: string.Format(category.Summary, mofScore, hipScore),
            Title: category.Text,
            SubTitle: category.Subtitle,
            RiskDescription: category.Description,
            Icon: category.Icon,
            Color: category.Color,
            PreventionSteps: category.Prevention,
            NextSteps: category.NextSteps,
            FollowUp: category.FollowUp,
            PatientEducation: category.PatientEducation
        );
        
        return riskResult;
    }

    private FraxScore? CalculateFraxScore(FractureRiskInput input)
    {
        HttpClient client = _httpClientFactory.CreateClient();
        var apiClient = new FractureRiskApiClient(client);
        
        try
        {
            var result = apiClient.CalculateAsync(input, 
                new Uri(FraxApiAddress, UriKind.Absolute)).Result;
            
            return result.FraxScore;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}
