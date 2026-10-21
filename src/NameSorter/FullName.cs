namespace NameSorter;

public sealed class FullName
{
    public FullName(IEnumerable<string> givenNames, string lastName)
    {
        ArgumentNullException.ThrowIfNull(givenNames);
        ArgumentException.ThrowIfNullOrWhiteSpace(lastName);

        var names = givenNames.ToArray();
        if (names.Length is < 1 or > 3)
        {
            throw new ArgumentException(NameSorterMessages.GivenNameCount, nameof(givenNames));
        }

        if (names.Any(string.IsNullOrWhiteSpace))
        {
            throw new ArgumentException(NameSorterMessages.GivenNamesRequired, nameof(givenNames));
        }

        GivenNames = Array.AsReadOnly(names);
        LastName = lastName;
    }

    public IReadOnlyList<string> GivenNames { get; }

    public string LastName { get; }

    public override string ToString() => string.Join(' ', GivenNames.Append(LastName));
}
