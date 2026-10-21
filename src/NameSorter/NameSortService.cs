namespace NameSorter;

public sealed class NameSortService
{
    private readonly FullNameComparer _comparer;
    private static readonly System.Text.CompositeFormat InvalidNameOnLineFormat =
        System.Text.CompositeFormat.Parse(NameSorterMessages.InvalidNameOnLine);

    public NameSortService(FullNameComparer comparer)
    {
        _comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
    }

    public IReadOnlyList<FullName> SortLines(IEnumerable<string> lines)
    {
        ArgumentNullException.ThrowIfNull(lines);

        var names = new List<FullName>();
        var lineNumber = 0;
        foreach (var line in lines)
        {
            lineNumber++;
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            try
            {
                names.Add(FullNameParser.Parse(line));
            }
            catch (FormatException exception)
            {
                throw new FormatException(
                    string.Format(
                        System.Globalization.CultureInfo.InvariantCulture,
                        InvalidNameOnLineFormat,
                        lineNumber,
                        exception.Message),
                    exception);
            }
        }

        return names.OrderBy(name => name, _comparer).ToArray();
    }
}
