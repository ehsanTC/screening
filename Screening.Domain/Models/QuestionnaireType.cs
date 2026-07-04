namespace Screening.Domain.Models;

public record QuestionnaireType(QuestionnaireType.Type Name)
{   
    public enum Type
    {
        Test,
        Diabetes,
        Ascvd
    }
}
