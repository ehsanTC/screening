using YamlDotNet.Serialization;

namespace Screening.Domain.Parser.Models;

public class YamlListItem : IEquatable<YamlListItem>
{
    [YamlMember(Alias = "text")]  public required string Text { get; init; }
    [YamlMember(Alias = "value")] public required string Value { get; init; }

    public bool Equals(YamlListItem? other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (obj.GetType() != GetType())
            return false;

        return Equals((YamlListItem)obj);
    }

    public override int GetHashCode() => 
        StringComparer.OrdinalIgnoreCase.GetHashCode(Value ?? string.Empty);
}
