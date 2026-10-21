namespace NameSorter.Tests;

public sealed class FullNameTests
{
    [Fact]
    public void Constructor_GivenOneToThreeGivenNames_WhenCreated_ThenStoresNameParts()
    {
        var name = new FullName(["Alex", "Jordan"], "Smith");

        Assert.Equal(["Alex", "Jordan"], name.GivenNames);
        Assert.Equal("Smith", name.LastName);
    }

    [Fact]
    public void Constructor_GivenNoGivenNames_WhenCreated_ThenThrowsArgumentException()
    {
        var exception = Assert.Throws<ArgumentException>(() => new FullName([], "Smith"));

        Assert.Contains("one to three given names", exception.Message);
    }

    [Fact]
    public void Constructor_GivenWhitespaceGivenName_WhenCreated_ThenThrowsArgumentException()
    {
        var exception = Assert.Throws<ArgumentException>(() => new FullName(["Alex", " "], "Smith"));

        Assert.Contains("cannot be empty or whitespace", exception.Message);
    }

    [Fact]
    public void ToString_GivenFullName_WhenFormatted_ThenJoinsNamePartsWithSpaces()
    {
        var name = new FullName(["Alex", "Jordan"], "Smith");

        Assert.Equal("Alex Jordan Smith", name.ToString());
    }
}
