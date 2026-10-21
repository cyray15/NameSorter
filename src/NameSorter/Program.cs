using NameSorter;

const string usage = "Usage: name-sorter <input-file-path>";

if (args.Length == 1 && args[0] is "--help" or "-h")
{
    Console.WriteLine(usage);
    return 0;
}

if (!CommandLineOptions.TryGetInputPath(args, out var inputPath))
{
    Console.Error.WriteLine(usage);
    return 1;
}

if (!File.Exists(inputPath))
{
    Console.Error.WriteLine($"Input file not found: {inputPath}");
    return 1;
}

try
{
    var sorter = new NameSortService(new FullNameComparer());
    var sortedNames = sorter.SortLines(File.ReadLines(inputPath));
    var outputLines = sortedNames.Select(name => name.ToString()).ToArray();

    File.WriteAllLines(Path.Combine(Environment.CurrentDirectory, "sorted-names-list.txt"), outputLines);
    foreach (var name in outputLines)
    {
        Console.WriteLine(name);
    }

    return 0;
}
catch (FormatException exception)
{
    Console.Error.WriteLine(exception.Message);
    return 1;
}
catch (IOException exception)
{
    Console.Error.WriteLine($"Unable to read or write a name list: {exception.Message}");
    return 1;
}
catch (UnauthorizedAccessException exception)
{
    Console.Error.WriteLine($"Unable to access a name list: {exception.Message}");
    return 1;
}
