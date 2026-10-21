namespace NameSorter.Tests;

public sealed class FullNameComparerTests
{
    private readonly FullNameComparer _comparer = new();

    [Fact]
    public void Compare_GivenDifferentLastNames_WhenCompared_ThenOrdersByLastName()
    {
        var comparison = _comparer.Compare(
            new FullName(["Alex"], "Taylor"),
            new FullName(["Zoe"], "Anderson"));

        Assert.True(comparison > 0);
    }

    [Fact]
    public void Compare_GivenMatchingLastNames_WhenCompared_ThenOrdersGivenNamesInSequence()
    {
        var earlier = new FullName(["Alex", "Ann"], "Taylor");
        var later = new FullName(["Alex", "Bob"], "Taylor");

        Assert.True(_comparer.Compare(earlier, later) < 0);
        Assert.True(_comparer.Compare(
            new FullName(["Alex"], "Taylor"),
            new FullName(["Alex", "Ann"], "Taylor")) < 0);
    }

    [Fact]
    public void Compare_GivenNamesDifferOnlyByCase_WhenCompared_ThenUsesOrdinalTieBreak()
    {
        var uppercase = new FullName(["Alex"], "Smith");
        var lowercase = new FullName(["alex"], "Smith");

        Assert.True(_comparer.Compare(uppercase, lowercase) < 0);
    }
}
