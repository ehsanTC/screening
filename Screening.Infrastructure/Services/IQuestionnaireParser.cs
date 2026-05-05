using Screening.Domain.Parser.Models;

namespace Screening.Infrastructure.Services;

public interface IQuestionnaireParser
{
    YamlQuestionnaire Parse(string content);
}
