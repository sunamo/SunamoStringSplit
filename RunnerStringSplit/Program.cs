namespace RunnerStringSplit;

using SunamoStringSplit.Tests;

internal class Program
{
    private static void Main()
    {
        SHSplitTests t = new SHSplitTests();
        //t.SplitTest();
        t.SplitByWhiteSpacesTest();
    }
}