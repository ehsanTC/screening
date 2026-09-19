using YamlDotNet.Serialization;

namespace Screening.Domain.Parser.Models;

public class PreventionSteps : IEquatable<PreventionSteps>
{
    [YamlMember(Alias = "primordial")]
    public List<string> Primordial { get; set; } = [];

    [YamlMember(Alias = "primary")]
    public List<string> Primary { get; set; } = [];

    [YamlMember(Alias = "secondary")]
    public List<string> Secondary { get; set; } = [];

    public bool Equals(PreventionSteps? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;

        return Primordial.SequenceEqual(other.Primordial)
               && Primary.SequenceEqual(other.Primary)
               && Secondary.SequenceEqual(other.Secondary);
    }

    public override bool Equals(object? obj) => Equals(obj as PreventionSteps);

    public override int GetHashCode()
    {
        var hash = new HashCode();
        foreach (var s in Primordial) hash.Add(s);
        foreach (var s in Primary)    hash.Add(s);
        foreach (var s in Secondary)  hash.Add(s);
        return hash.ToHashCode();
    }

    public static bool operator ==(PreventionSteps? a, PreventionSteps? b)
        => Equals(a, b);

    public static bool operator !=(PreventionSteps? a, PreventionSteps? b)
        => !Equals(a, b);
}
