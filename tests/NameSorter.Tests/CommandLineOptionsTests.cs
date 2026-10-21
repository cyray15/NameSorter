namespace NameSorter.Tests;

public sealed class CommandLineOptionsTests
{
    [Fact]
    public void TryGetInputPath_GivenOnePath_WhenCalled_ThenReturnsThatPath()
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
    public void TryGetInputPath_GivenInvalidArguments_WhenCalled_ThenRejectsArguments(params string[] arguments)
    {
        var result = CommandLineOptions.TryGetInputPath(arguments, out var path);

        Assert.False(result);
        Assert.Null(path);
    }
}
