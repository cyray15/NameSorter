namespace NameSorter.Tests;

public sealed class FullNameParserTests
{
    [Theory]
    [InlineData("Alex Smith", "Alex", "Smith")]
    [InlineData("Alex Jordan Smith", "Alex Jordan", "Smith")]
    [InlineData("Alex Jordan Casey Smith", "Alex Jordan Casey", "Smith")]
    public void Parse_GivenValidFullName_WhenParsed_ThenReturnsNameParts(
        string value,
        string expectedGivenNames,
        string expectedLastName)
    {
        var name = FullNameParser.Parse(value);

        Assert.Equal(expectedGivenNames.Split(' '), name.GivenNames);
        Assert.Equal(expectedLastName, name.LastName);
    }

    [Fact]
    public void Parse_GivenIrregularWhitespace_WhenParsed_ThenNormalizesNameParts()
    {
        var name = FullNameParser.Parse("  Alex\tJordan   Smith  ");

        Assert.Equal(["Alex", "Jordan"], name.GivenNames);
        Assert.Equal("Smith", name.LastName);
    }

    [Theory]
    [InlineData("Single")]
    [InlineData("One Two Three Four Five")]
    public void Parse_GivenInvalidPartCount_WhenParsed_ThenThrowsFormatException(string value)
    {
        var exception = Assert.Throws<FormatException>(() => FullNameParser.Parse(value));

        Assert.Contains("one to three given names", exception.Message);
    }
}
