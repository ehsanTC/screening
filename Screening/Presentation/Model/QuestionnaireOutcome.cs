using Screening.Domain.Questions;
using Screening.Domain.RiskCalculator.Abstraction;

namespace Screening.Presentation.Model;

public record QuestionnaireOutcome(QuestionnaireEntity Questionnaire, RiskResult  RiskResult);
