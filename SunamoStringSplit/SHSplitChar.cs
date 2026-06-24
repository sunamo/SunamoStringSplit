namespace SunamoStringSplit;

partial class SHSplit
{
    public static List<string> SplitCharList(string text, List<char> delimiters)
    {
        return Split(StringSplitOptions.RemoveEmptyEntries, text.RemoveInvisibleChars(), delimiters.ConvertAll(delimiter => delimiter.ToString()).ToArray());
    }

    public static List<string> SplitChar(string text, params char[] delimiters)
    {
        return Split(StringSplitOptions.RemoveEmptyEntries, text.RemoveInvisibleChars(),
            delimiters.ToList().ConvertAll(delimiter => delimiter.ToString()).ToArray());
    }

    public static List<string> SplitNoneChar(string text, params char[] delimiters)
    {
        return SplitChar(StringSplitOptions.None, text.RemoveInvisibleChars(), delimiters);
    }

    public static List<string> SplitNoneCharList(string text, List<char> delimiters)
    {
        var converted = delimiters.ConvertAll(delimiter => delimiter.ToString());
        return Split(StringSplitOptions.None, text.RemoveInvisibleChars(), converted.ToArray());
    }

    private static List<string> SplitChar(StringSplitOptions stringSplitOptions, string text,
        params char[] delimiters)
    {
        var charList = delimiters.ToList();
        var separators = new string[charList.Count];
        for (var i = 0; i < separators.Length; i++) separators[i] = charList[i].ToString();
        var result = text.RemoveInvisibleChars().Split(separators, stringSplitOptions).ToList();
        return result;
    }
}
