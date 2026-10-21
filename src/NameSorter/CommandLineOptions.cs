namespace NameSorter;

/// <summary>Parses the required input path from command-line arguments.</summary>
public static class CommandLineOptions
{
    public static bool TryGetInputPath(string[] args, out string? path)
    {
        path = null;

        if (args.Length != 1 || string.IsNullOrWhiteSpace(args[0]))
        {
            return false;
        }

        if (args[0].StartsWith("--", StringComparison.Ordinal))
        {
            return false;
        }

        path = args[0];
        return true;
    }
}
