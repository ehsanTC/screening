using Screening.Domain.Parser.Models;

namespace Screening.Domain.Services;

public interface IQuestionnaireParser
{
    YamlQuestionnaire Parse(string content);
}
