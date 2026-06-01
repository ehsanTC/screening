using Screening.Domain.Parser.Models;

namespace Screening.Domain.Extensions;

public static class YamlQuestionExtensions
{
    public static bool HasSaveConfig(this YamlQuestion question) =>
        question.SaveConfig != null;
}
