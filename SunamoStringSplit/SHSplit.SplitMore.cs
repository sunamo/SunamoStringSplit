namespace SunamoStringSplit;

public partial class SHSplit
{
    public static List<string> SplitMore(string text, params char[] delimiters)
    {
        return text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    public static List<string> SplitMore(string text, params string[] delimiters)
    {
        return text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToList();
    }
}
