namespace RunnerStringSplit;

using SunamoStringSplit.Tests;

/// <summary>
/// Console runner for SunamoStringSplit tests.
/// </summary>
internal class Program
{
    private static void Main()
    {
        SHSplitTests t = new SHSplitTests();
        t.SplitByWhiteSpacesTest();
    }
}
