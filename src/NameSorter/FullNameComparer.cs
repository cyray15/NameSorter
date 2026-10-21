namespace NameSorter;

public sealed class FullNameComparer : IComparer<FullName>
{
    private static readonly StringComparer PartComparer = StringComparer.OrdinalIgnoreCase;

    public int Compare(FullName? left, FullName? right)
    {
        if (ReferenceEquals(left, right))
        {
            return 0;
        }

        if (left is null)
        {
            return -1;
        }

        if (right is null)
        {
            return 1;
        }

        var comparison = ComparePart(left.LastName, right.LastName);
        if (comparison != 0)
        {
            return comparison;
        }

        var sharedGivenNameCount = Math.Min(left.GivenNames.Count, right.GivenNames.Count);
        for (var index = 0; index < sharedGivenNameCount; index++)
        {
            comparison = ComparePart(left.GivenNames[index], right.GivenNames[index]);
            if (comparison != 0)
            {
                return comparison;
            }
        }

        return left.GivenNames.Count.CompareTo(right.GivenNames.Count);
    }

    private static int ComparePart(string left, string right)
    {
        var comparison = PartComparer.Compare(left, right);
        return comparison != 0 ? comparison : StringComparer.Ordinal.Compare(left, right);
    }
}
