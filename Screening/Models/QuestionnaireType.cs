namespace Screening.Models;

public record QuestionnaireType
{
    public string Name { get; init; }
    private QuestionnaireType(string name) => Name = name;

    public static QuestionnaireType Test() =>  new QuestionnaireType("test");
    public static QuestionnaireType Diabetes() =>  new QuestionnaireType("diabetes");
    public static QuestionnaireType Ascvd() =>  new QuestionnaireType("ascvd");
}
