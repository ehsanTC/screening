using Screening.Domain.Answers;

namespace Screening.Domain.Parser.Models;

public record SavableAnswer(Answer Value, YamlSaveConfig SaveConfig);
