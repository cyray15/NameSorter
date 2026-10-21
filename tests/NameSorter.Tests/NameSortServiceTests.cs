namespace NameSorter.Tests;

public sealed class NameSortServiceTests
{
    private readonly NameSortService _sorter = new(new FullNameComparer());

    [Fact]
    public void SortLines_GivenAssessmentExample_WhenSorted_ThenReturnsExpectedOrder()
    {
        var sorted = _sorter.SortLines(
        [
            "Janet Parsons",
            "Vaughn Lewis",
            "Adonis Julius Archer",
            "Shelby Nathan Yoder",
            "Marin Alvarez",
            "London Lindsey",
            "Beau Tristan Bentley",
            "Leo Gardner",
            "Hunter Uriah Mathew Clarke",
            "Mikayla Lopez",
            "Frankie Conner Ritter"
        ]);

        Assert.Equal(
        [
            "Marin Alvarez",
            "Adonis Julius Archer",
            "Beau Tristan Bentley",
            "Hunter Uriah Mathew Clarke",
            "Leo Gardner",
            "Vaughn Lewis",
            "London Lindsey",
            "Mikayla Lopez",
            "Janet Parsons",
            "Frankie Conner Ritter",
            "Shelby Nathan Yoder"
        ],
        sorted.Select(name => name.ToString()));
    }

    [Fact]
    public void SortLines_GivenDuplicateNames_WhenSorted_ThenRetainsEveryName()
    {
        var sorted = _sorter.SortLines(["Alex Smith", "Alex Smith"]);

        Assert.Equal(["Alex Smith", "Alex Smith"], sorted.Select(name => name.ToString()));
    }

    [Fact]
    public void SortLines_GivenBlankLinesAndIrregularWhitespace_WhenSorted_ThenIgnoresBlanksAndNormalizesNames()
    {
        var sorted = _sorter.SortLines(["  ", "  Alex\tJordan   Smith  "]);

        Assert.Equal(["Alex Jordan Smith"], sorted.Select(name => name.ToString()));
    }

    [Fact]
    public void SortLines_GivenInvalidNameOnSecondLine_WhenSorted_ThenReportsLineNumber()
    {
        var exception = Assert.Throws<FormatException>(
            () => _sorter.SortLines(["Valid Name", "Single"]));

        Assert.Contains("line 2", exception.Message);
    }

    [Fact]
    public void SortLines_GivenOneThousandNames_WhenSorted_ThenReturnsAllNamesInOrder()
    {
        var input = Enumerable.Range(0, 1_000)
            .Select(index => $"Given{index:D4} Last{999 - index:D4}");

        var sorted = _sorter.SortLines(input);

        Assert.Equal(1_000, sorted.Count);
        Assert.Equal("Given0999 Last0000", sorted[0].ToString());
        Assert.Equal("Given0000 Last0999", sorted[^1].ToString());
    }
}
