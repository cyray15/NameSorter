namespace NameSorter.Tests;

public sealed class CommandLineOptionsTests
{
    [Fact]
    public void TryGetInputPath_ReturnsPath_WhenOnePathIsSupplied()
    {
        var result = CommandLineOptions.TryGetInputPath(["./names.txt"], out var path);

        Assert.True(result);
        Assert.Equal("./names.txt", path);
    }

    [Theory]
    [InlineData()]
    [InlineData("")]
    [InlineData("--help")]
    [InlineData("one.txt", "two.txt")]
    public void TryGetInputPath_RejectsInvalidArguments(params string[] arguments)
    {
        var result = CommandLineOptions.TryGetInputPath(arguments, out var path);

        Assert.False(result);
        Assert.Null(path);
    }
}
