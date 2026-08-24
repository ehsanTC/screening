using YamlDotNet.Serialization;

namespace Screening.Domain.Parser.Models;

public class PreventionSteps
{
    [YamlMember(Alias = "primordial")] public List<string> Primordial { get; set; } = [];

    [YamlMember(Alias = "primary")] public List<string> Primary { get; set; } = [];

    [YamlMember(Alias = "secondary")] public List<string> Secondary { get; set; } = [];
}
