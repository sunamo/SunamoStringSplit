namespace SunamoStringSplit.Tests;

/// <summary>
/// Tests for SHSplit.SplitParagraphToMaxChars.
/// </summary>
public class SplitParagraphToMaxCharsTests
{
    /// <summary>
    /// Tests that long lines break at the last fitting space and short or unbreakable lines stay whole.
    /// </summary>
    [Fact]
    public void BreaksAtLastFittingSpace()
    {
        var result = SHSplit.SplitParagraphToMaxChars("aaa bbb ccc\nshort\nunbreakableword", 8);

        Assert.Equal(string.Join(Environment.NewLine, "aaa bbb", "ccc", "short", "unbreakableword"), result);
    }
}