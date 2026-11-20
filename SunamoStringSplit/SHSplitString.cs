namespace SunamoStringSplit;

partial class SHSplit
{
    /// <summary>
    /// Split.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="delimiter">The delimiter.</param>
    /// <returns>The split result.</returns>
    public static List<string> Split(string text, string delimiter)
    {
        return Split(StringSplitOptions.RemoveEmptyEntries, text.RemoveInvisibleChars(), delimiter);
    }

    /// <summary>
    /// Split list.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="delimiters">The delimiters.</param>
    /// <returns>The split list result.</returns>
    public static List<string> SplitList(string text, List<string> delimiters)
    {
        return Split(StringSplitOptions.RemoveEmptyEntries, text.RemoveInvisibleChars(), delimiters.ToArray());
    }

    /// <summary>
    /// Split.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="delimiters">The delimiters.</param>
    /// <returns>The split result.</returns>
    public static List<string> Split(string text, params string[] delimiters)
    {
        return Split(StringSplitOptions.RemoveEmptyEntries, text.RemoveInvisibleChars(), delimiters);
    }

    /// <summary>
    /// Split none.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="delimiters">The delimiters.</param>
    /// <returns>The split none result.</returns>
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
