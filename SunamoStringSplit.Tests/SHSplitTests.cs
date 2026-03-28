namespace SunamoStringSplit.Tests;

/// <summary>
/// Unit tests for SHSplit string splitting methods.
/// </summary>
public class SHSplitTests
{
    /// <summary>
    /// Tests splitting a string containing multiple URLs separated by whitespace characters (tabs).
    /// </summary>
    [Fact]
    public void SplitByWhiteSpacesTest()
    {
        var text = "SrealityUri\t\thttps://www.sreality.cz//detail/pronajem/dum/rodinny/bohdanec-replice-/960152140\thttps://www.sreality.cz//detail/pronajem/dum/rodinny/beloky-beloky-/556712524\thttps://www.sreality.cz//detail/pronajem/dum/rodinny/kacice-kacice-masarykova/3465400908\thttps://www.sreality.cz//detail/pronajem/dum/rodinny/cesky-brod-cesky-brod-jatecka/3015361100\thttps://www.sreality.cz//detail/pronajem/dum/rodinny/melnicke-vtelno-vysoka-liben-/316199500\thttps://www.sreality.cz//detail/pronajem/dum/rodinny/otvovice-otvovice-/2051330636\thttps://www.sreality.cz//detail/pronajem/dum/rodinny/bojanovice-bojanovice-/20763212\thttps://www.sreality.cz//detail/pronajem/dum/chata/postupice-postupice-/3613577804\thttps://www.sreality.cz//detail/pronajem/dum/chata/hvozdany-vacikov-/2525753932\thttps://www.sreality.cz//detail/pronajem/dum/rodinny/bystrice-zahorany-/1500566860\thttps://www.sreality.cz//detail/pronajem/dum/rodinny/skvorec-trebohostice-/405869644\t\t\t\t\t\t\t\t\t\t\t\t\t";

        var result = SHSplit.SplitByWhiteSpaces(text);

        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Contains(result, element => element.Contains("sreality.cz"));
    }

    /// <summary>
    /// Tests splitting a string by a special Unicode character delimiter.
    /// </summary>
    [Fact]
    public void SplitTest()
    {
        var result = SHSplit.Split(" 63\u00ADm\u00AD ", "\u00AD");

        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Contains("63", result);
        Assert.Contains("m", result);
    }

    /// <summary>
    /// Tests splitting a string by character delimiters.
    /// </summary>
    [Fact]
    public void SplitCharTest()
    {
        var result = SHSplit.SplitChar("hello,world,test", ',');

        Assert.Equal(3, result.Count);
        Assert.Equal("hello", result[0]);
        Assert.Equal("world", result[1]);
        Assert.Equal("test", result[2]);
    }

    /// <summary>
    /// Tests splitting a string by newline characters.
    /// </summary>
    [Fact]
    public void SplitByNewLinesTest()
    {
        var result = SHSplit.SplitByNewLines("line1\nline2\nline3");

        Assert.Equal(3, result.Count);
        Assert.Equal("line1", result[0]);
        Assert.Equal("line2", result[1]);
        Assert.Equal("line3", result[2]);
    }

    /// <summary>
    /// Tests splitting a string into two parts at a specific index position.
    /// </summary>
    [Fact]
    public void SplitByIndexTest()
    {
        SHSplit.SplitByIndex("hello world", 5, out var before, out var after);

        Assert.Equal("hello", before);
        Assert.Equal("world", after);
    }

    /// <summary>
    /// Tests splitting a string by the last occurrence of a delimiter character.
    /// </summary>
    [Fact]
    public void SplitByLastCharToTwoPartsTest()
    {
        SHSplit.SplitByLastCharToTwoParts("path/to/file.txt", out var before, out var after, '/');

        Assert.Equal("path/to", before);
        Assert.Equal("file.txt", after);
    }

    /// <summary>
    /// Tests splitting a string into segments of equal character length.
    /// </summary>
    [Fact]
    public void SplitByLetterCountTest()
    {
        var result = SHSplit.SplitByLetterCount("abcdef", 3);

        Assert.Equal(2, result.Count);
        Assert.Equal("abc", result[0]);
        Assert.Equal("def", result[1]);
    }

    /// <summary>
    /// Tests splitting a string and parsing each part as an integer.
    /// </summary>
    [Fact]
    public void SplitToIntListTest()
    {
        var result = SHSplit.SplitToIntList("1,2,3", ",");

        Assert.Equal(3, result.Count);
        Assert.Equal(1, result[0]);
        Assert.Equal(2, result[1]);
        Assert.Equal(3, result[2]);
    }

    /// <summary>
    /// Tests splitting a string without removing empty entries.
    /// </summary>
    [Fact]
    public void SplitNoneTest()
    {
        var result = SHSplit.SplitNone("a,,b", ",");

        Assert.Equal(3, result.Count);
        Assert.Equal("a", result[0]);
        Assert.Equal("", result[1]);
        Assert.Equal("b", result[2]);
    }

    /// <summary>
    /// Tests splitting a string into a specific number of parts.
    /// </summary>
    [Fact]
    public void SplitToPartsTest()
    {
        var result = SHSplit.SplitToParts("a-b-c-d", 2, "-");

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("a", result[0]);
        Assert.Equal("b-c-d", result[1]);
    }

    /// <summary>
    /// Tests that SplitByLastCharToTwoParts returns null for both parts when delimiter is not found.
    /// </summary>
    [Fact]
    public void SplitByLastCharToTwoPartsNotFoundTest()
    {
        SHSplit.SplitByLastCharToTwoParts("nodelimiter", out var before, out var after, '/');

        Assert.Null(before);
        Assert.Null(after);
    }
}
