namespace SunamoStringSplit;

partial class SHSplit
{
    /// <summary>
    /// Split char list.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="delimiters">The delimiters.</param>
    /// <returns>The split char list result.</returns>
    public static List<string> SplitCharList(string text, List<char> delimiters)
    {
        return Split(StringSplitOptions.RemoveEmptyEntries, text.RemoveInvisibleChars(), delimiters.ConvertAll(delimiter => delimiter.ToString()).ToArray());
    }

    /// <summary>
    /// Split char.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="delimiters">The delimiters.</param>
    /// <returns>The split char result.</returns>
    public static List<string> SplitChar(string text, params char[] delimiters)
    {
        return Split(StringSplitOptions.RemoveEmptyEntries, text.RemoveInvisibleChars(),
            delimiters.ToList().ConvertAll(delimiter => delimiter.ToString()).ToArray());
    }

    /// <summary>
    /// Split none char.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="delimiters">The delimiters.</param>
    /// <returns>The split none char result.</returns>
    public static List<string> SplitNoneChar(string text, params char[] delimiters)
    {
        return SplitChar(StringSplitOptions.None, text.RemoveInvisibleChars(), delimiters);
    }

    /// <summary>
    /// Split none char list.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="delimiters">The delimiters.</param>
    /// <returns>The split none char list result.</returns>
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
