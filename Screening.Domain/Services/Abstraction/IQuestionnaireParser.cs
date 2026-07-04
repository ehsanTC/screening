using Screening.Domain.Parser.Models;

namespace Screening.Domain.Services.Abstraction;

public interface IQuestionnaireParser
{
    YamlQuestionnaire Parse(string content);
}
