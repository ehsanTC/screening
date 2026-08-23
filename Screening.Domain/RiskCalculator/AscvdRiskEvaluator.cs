using Screening.Domain.Extensions;
using Screening.Domain.Models;
using Screening.Domain.Parser.Models;
using Screening.Domain.Questionnaire;
using Screening.Domain.Questionnaire.Abstraction;
using Screening.Domain.Questions;
using Screening.Domain.RiskCalculator.Abstraction;
using Screening.Domain.RiskCalculator.Models;
using Screening.Domain.RiskCalculator.Requests;

namespace Screening.Domain.RiskCalculator;

public class AscvdRiskEvaluator : IRiskEvaluator
{
    private readonly IRiskInputBuilder<AscvdRiskInput> _inputBuilder;
    private IReadOnlyDictionary<(PreventModel, Sex, Outcome), PreventCoefficients> _coeffs;

    public AscvdRiskEvaluator(IRiskInputBuilder<AscvdRiskInput> inputBuilder)
    {
        _coeffs = PreventCoefficientTable.Data;
        _inputBuilder = inputBuilder ?? throw new ArgumentNullException(nameof(inputBuilder));
    }

    public QuestionnaireType QuestionnaireType => QuestionnaireType.Ascvd;

    public RiskResult? Evaluate(QuestionnaireEntity questionnaire)
    {
        var riskInput = BuildInput(questionnaire);
        var score = Calculate(riskInput);
        
        if (score == null)
            return null;
        
        var ascvd10YearScore = (float) score.Ascvd10Yr;
        var category = questionnaire.GetCategory(ascvd10YearScore);
        
        if (category == null)
            return null;
        
        var severity = category.GetSeverity();
        return new RiskResult(ascvd10YearScore.ToString("F1"), 
            severity, 
            Summary: category.Summary,
            Title: category.Value,
            SubTitle: category.Subtitle,
            Icon: category.Icon,
            Color: category.Color,
            PreventionSteps: category.Prevention,
            NextSteps: category.NextSteps,
            FollowUp: category.FollowUp,
            PatientEducation:  category.PatientEducation);
    }

    private AscvdRiskInput BuildInput(QuestionnaireEntity questionnaire)
    {
        var reader = new QuestionnaireAnswerReader(questionnaire);
        var input = _inputBuilder.Build(reader);
        return input;
    }

    private HeartRiskResult? Calculate(AscvdRiskInput input)
    {
        // 2. Validate core inputs
        if (input.Age is < 30 or > 79) return null;
        if (input.TotalCholesterol is < 130 or > 320) return null;
        if (input.HdlCholesterol is < 20 or > 100) return null;
        if (input.SystolicBp is < 9 or > 20) return null;
        if (input.Egfr <= 0) return null;

        input.SystolicBp *= 10;
        bool validForHf = input.Bmi is >= 18.5 and < 40;

        // 3. Determine model (exact AHA logic)
        bool hasUacr = input.Uacr.HasValue;
        bool hasHbA1c = input.HbA1c.HasValue;
        bool hasSdi = input.SdiCategory.HasValue;
        int optionalCount = (hasUacr ? 1 : 0) + (hasHbA1c ? 1 : 0) + (hasSdi ? 1 : 0);

        PreventModel model;

        if (optionalCount >= 2)
        {
            model = PreventModel.Full;
        }
        else if (hasUacr)
        {
            model = PreventModel.Uacr;
        }
        else if (hasHbA1c)
        {
            model = PreventModel.HbA1c;
        }
        else if (hasSdi)
        {
            model = PreventModel.Sdi;
        }
        else
        {
            model = PreventModel.Base;
        }

        // 4. Transform inputs
        double ageC = (input.Age - 55) / 10.0;
        double nonHdlC = ToMmol(input.TotalCholesterol - input.HdlCholesterol) - 3.5;
        double hdlC = (ToMmol(input.HdlCholesterol) - 1.3) / 0.3;
        double sbpMinC = (Math.Min(input.SystolicBp, 110) - 110) / 20.0;
        double sbpMaxC = (Math.Max(input.SystolicBp, 110) - 130) / 20.0;
        double egfrMinC = (Math.Min(input.Egfr, 60) - 60) / -15.0;
        double egfrMaxC = (Math.Max(input.Egfr, 60) - 90) / -15.0;

        double bmiForCalc = input.Bmi ?? 0;
        double bmiMinC = input.Bmi.HasValue ? (Math.Min(bmiForCalc, 30) - 25) / 5.0 : 0;
        double bmiMaxC = input.Bmi.HasValue ? (Math.Max(bmiForCalc, 30) - 30) / 5.0 : 0;

        double? sdiCat = input.SdiCategory.HasValue ? (double)input.SdiCategory.Value : (double?)null;

        double dm = input.Diabetic ? 1 : 0;
        double smk = input.Smoker ? 1 : 0;
        double bpTreat = input.HypertensionTreated ? 1 : 0;
        double statin = input.LipidLowering ? 1 : 0;

        // 5. Calculate outcomes
        var outcomes = new[]
        {
            Outcome.TenYearCvd, Outcome.TenYearAscvd, Outcome.TenYearHf,
            Outcome.ThirtyYearCvd, Outcome.ThirtyYearAscvd, Outcome.ThirtyYearHf
        };

        var result = new Dictionary<Outcome, double>();

        foreach (var outcome in outcomes)
        {
            var c = _coeffs[(model, input.Sex, outcome)];

            // --- Base linear predictor ---
            double sum =
                c.Intercept +
                c.Age * ageC +
                c.AgeSq * Math.Pow(ageC, 2) + // only nonzero in 30yr models
                c.NonHdl * nonHdlC +
                c.Hdl * hdlC +
                c.SbpMin * sbpMinC +
                c.SbpMax * sbpMaxC +
                c.Dm * dm +
                c.Smk * smk +
                c.EgfrMin * egfrMinC +
                c.EgfrMax * egfrMaxC +
                c.BpTreat * bpTreat +
                c.Statin * statin +
                c.BpTreatSbp * bpTreat * sbpMaxC +
                c.StatinNonHdl * statin * nonHdlC +
                c.AgeNonHdl * ageC * nonHdlC +
                c.AgeHdl * ageC * hdlC +
                c.AgeSbp * ageC * sbpMaxC +
                c.AgeDm * ageC * dm +
                c.AgeSmk * ageC * smk +
                c.AgeEgfr * ageC * egfrMinC;

            // --- BMI terms (HF only; coefficients are 0 for CVD/ASCVD) ---
            sum +=
                c.BmiMin * bmiMinC +
                c.BmiMax * bmiMaxC +
                c.AgeBmi * ageC * bmiMaxC;

            // --- Optional terms: SDI ---
            if (sdiCat.HasValue)
            {
                double cat = sdiCat.Value;
                sum += c.SdiC1 * (2 - cat) * cat + c.SdiC2 * (cat - 1) * (0.5 * cat);
            }
            else
            {
                sum += c.SdiMiss;
            }

            // --- Optional terms: UACR ---
            if (input.Uacr.HasValue)
            {
                sum += c.UacrCoef * Math.Log(AdjustUacr(input.Uacr.Value));
            }
            else
            {
                sum += c.UacrMiss;
            }

            // --- Optional terms: HbA1c ---
            if (input.HbA1c.HasValue)
            {
                double val = input.HbA1c.Value - 5.3;
                sum += c.HbA1cC1 * val * dm + c.HbA1cC2 * val * (1 - dm);
            }
            else
            {
                sum += c.HbA1cMiss;
            }

            double expSum = Math.Exp(sum);
            result[outcome] = 100 * expSum / (1 + expSum);
        }

        bool over59 = input.Age > 59;

        return new HeartRiskResult
        {
            Cvd10Yr   = result[Outcome.TenYearCvd],
            Ascvd10Yr = result[Outcome.TenYearAscvd],
            Hf10Yr    = !validForHf ? null : result[Outcome.TenYearHf],
            Cvd30Yr   = over59 ? null : result[Outcome.ThirtyYearCvd],
            Ascvd30Yr = over59 ? null : result[Outcome.ThirtyYearAscvd],
            Hf30Yr    = (over59 || !validForHf) ? null : result[Outcome.ThirtyYearHf]
        };
    }

    private string GetMessage(YamlResultCategory category, double score) =>
        $"{string.Format(category.Summary, score)}{Environment.NewLine}{category.NextSteps}";

    private static double ToMmol(double mgPerDl) => 0.02586 * mgPerDl;

    private static double AdjustUacr(double uacr) =>
        uacr >= 0.1 ? uacr : uacr >= 0 ? 0.1 : uacr;
}
