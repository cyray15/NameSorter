namespace NameSorter;

public static class FullNameParser
{
    public static FullName Parse(string value)
    {
        ArgumentNullException.ThrowIfNull(value);

        var parts = value.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length is < 2 or > 4)
        {
            throw new FormatException(NameSorterMessages.InvalidNameFormat);
        }

        return new FullName(parts[..^1], parts[^1]);
    }
}
