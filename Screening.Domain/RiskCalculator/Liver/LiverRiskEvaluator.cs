using Screening.Domain.Extensions;
using Screening.Domain.Models;
using Screening.Domain.Questionnaire;
using Screening.Domain.Questionnaire.Abstraction;
using Screening.Domain.Questions;
using Screening.Domain.RiskCalculator.Abstraction;
using Screening.Domain.RiskCalculator.Models;

namespace Screening.Domain.RiskCalculator.Liver;

public sealed class LiverRiskEvaluator : IRiskEvaluator
{
    private const int ObesityThreshold = 30;
    private const double NfsLowCutoff = -1.455;
    private const double NfsHighCutoff = 0.676;
    private const double Fib4LowCutoff = 1.30;
    private const double Fib4HighCutoff = 2.67;

    private readonly IRiskInputBuilder<LiverRiskInput> _riskInputBuilder;

    public QuestionnaireType QuestionnaireType => QuestionnaireType.Liver;

    public LiverRiskEvaluator(IRiskInputBuilder<LiverRiskInput> riskInputBuilder)
    {
        _riskInputBuilder = riskInputBuilder ?? throw new ArgumentNullException(nameof(riskInputBuilder));
    }

    public RiskResult Evaluate(QuestionnaireEntity questionnaire)
    {
        ArgumentNullException.ThrowIfNull(questionnaire);

        var reader = new QuestionnaireAnswerReader(questionnaire);
        var input = _riskInputBuilder.Build(reader);

        if (!QualifiesForRiskAssessment(input))
            return BuildResult(
                RiskSeverity.NotEligibleForAssessment,
                questionnaire);

        if (!HasSuspectedFattyLiver(input))
            return BuildResult(
                RiskSeverity.NoIndicationForFurtherAssessment,
                questionnaire);

        var fib4 = CalculateFib4(
            input.Age,
            input.Ast,
            input.Alt,
            input.PlateletCount);

        var nfs = CalculateNfs(
            input.Age,
            input.Bmi,
            input.HaveDiabetes,
            input.Ast,
            input.Alt,
            input.PlateletCount,
            input.Albumin);

        if (fib4 is null || nfs is null)
            return BuildResult(
                RiskSeverity.InsufficientData,
                questionnaire);

        var severity = Classify(nfs.Value, fib4.Value);
        return BuildResult(severity, questionnaire);
    }
    
    private static bool QualifiesForRiskAssessment(LiverRiskInput input) => input.Age > 50 &&
        (input.HaveDiabetes || input.Bmi > ObesityThreshold || input.HaveMetabolicSyndrome);

    private static bool HasSuspectedFattyLiver(LiverRiskInput input) => input.UltrasonicFattyLiver is true ||
        HasElevatedLiverEnzymes(input.Gender, input.Alt, input.Ast);

    private static RiskResult BuildInsufficientDataResult() =>
        RiskResult.Empty with
        {
            Score = "N/A",
            Summary =
            "Fatty liver disease is suspected, but there is not enough laboratory information to assess fibrosis risk.",
            RiskDescription =
            "FIB-4 requires age, AST, ALT and platelet count. NFS additionally requires BMI, diabetes status and albumin.",
            NextSteps =
            "Discuss AST, ALT and platelet testing with your healthcare provider; albumin is additionally required to calculate NFS."
        };

    private static RiskSeverity Classify(double nfs, double fib4)
    {
        if (nfs < NfsLowCutoff && fib4 < Fib4LowCutoff)
            return RiskSeverity.Low;

        if (nfs > NfsHighCutoff && fib4 > Fib4HighCutoff)
            return RiskSeverity.High;

        return RiskSeverity.Medium;
    }

    private static RiskResult BuildResult(RiskSeverity severity, QuestionnaireEntity questionnaire)
    {
        var category = questionnaire.GetCategory(severity);
        return new RiskResult(
            Score: severity.ToString(),
            Severity: severity,
            Summary: string.Format(category.Summary, severity),
            Title: category.Text,
            SubTitle: category.Subtitle,
            RiskDescription: category.Description,
            Icon: category.Icon,
            Color: category.Color,
            PreventionSteps: category.Prevention,
            NextSteps: category.NextSteps,
            FollowUp: category.FollowUp,
            PatientEducation: category.PatientEducation);
    }

    private static bool HasElevatedLiverEnzymes(Sex gender, double? alt, double? ast)
    {
        var altCutoff = gender == Sex.Male ? 33.0 : 25;
        return alt > altCutoff || ast is > 35.0;
    }

    private static double? CalculateFib4(int age, double? ast, double? alt, double? platelet)
    {
        if (ast is null || alt is null || platelet is null || ast <= 0 || alt <= 0 || platelet <= 0)
            return null;

        return age * ast.Value / (platelet.Value * Math.Sqrt(alt.Value));
    }

    private static double? CalculateNfs(int age, double? bmi, bool diabetes, double? ast, double? alt,
        double? platelet,
        double? albumin)
    {
        if (ast is null || alt is null || platelet is null || albumin is null || bmi is null ||
            ast <= 0 || alt <= 0 || platelet <= 0)
            return null;

        var astAltRatio = ast.Value / alt.Value;

        return -1.675
               + 0.037 * age
               + 0.094 * bmi.Value
               + 1.13 * (diabetes ? 1 : 0)
               + 0.99 * astAltRatio
               - 0.013 * platelet.Value
               - 0.66 * albumin.Value;
    }
}
