namespace SunamoStringSplit;

partial class SHSplit
{
    public static List<string> Split(string text, string delimiter)
    {
        return Split(StringSplitOptions.RemoveEmptyEntries, text.RemoveInvisibleChars(), delimiter);
    }

    public static List<string> SplitList(string text, List<string> delimiters)
    {
        return Split(StringSplitOptions.RemoveEmptyEntries, text.RemoveInvisibleChars(), delimiters.ToArray());
    }

    public static List<string> Split(string text, params string[] delimiters)
    {
        return Split(StringSplitOptions.RemoveEmptyEntries, text.RemoveInvisibleChars(), delimiters);
    }

    public static List<string> SplitNone(string text, params string[] delimiters)
    {
        return text.RemoveInvisibleChars().Split(delimiters, StringSplitOptions.None).ToList();
    }

    private static List<string> Split(StringSplitOptions stringSplitOptions, string text, List<char> delimiters)
    {
        var charList = delimiters.ToList();
        var separators = new string[charList.Count];
        for (var i = 0; i < separators.Length; i++) separators[i] = charList[i].ToString();
        var result = text.RemoveInvisibleChars().Split(separators, stringSplitOptions).ToList();
        return result;
    }
}
