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

// TODO(feature/name-parser): parse, sort, print and overwrite sorted-names-list.txt.
// Deliberately left for a separately reviewed implementation PR.
Console.Error.WriteLine($"Not implemented yet for input: {inputPath}");
return 2;
