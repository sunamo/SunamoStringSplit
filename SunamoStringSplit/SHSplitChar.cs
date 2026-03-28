namespace SunamoStringSplit;

/// <summary>
/// Provides character-based string splitting methods.
/// </summary>
partial class SHSplit
{
    /// <summary>
    /// Splits a string by a list of character delimiters, removing empty entries.
    /// </summary>
    /// <param name="text">The text to split.</param>
    /// <param name="delimiters">The list of character delimiters.</param>
    /// <returns>A list of substrings.</returns>
    public static List<string> SplitCharList(string text, List<char> delimiters)
    {
        return Split(StringSplitOptions.RemoveEmptyEntries, text.RemoveInvisibleChars(), delimiters.ConvertAll(delimiter => delimiter.ToString()).ToArray());
    }

    /// <summary>
    /// Splits a string by character delimiters, removing empty entries.
    /// </summary>
    /// <param name="text">The text to split.</param>
    /// <param name="delimiters">The character delimiters to split by.</param>
    /// <returns>A list of substrings.</returns>
    public static List<string> SplitChar(string text, params char[] delimiters)
    {
        return Split(StringSplitOptions.RemoveEmptyEntries, text.RemoveInvisibleChars(),
            delimiters.ToList().ConvertAll(delimiter => delimiter.ToString()).ToArray());
    }

    /// <summary>
    /// Splits a string by character delimiters without removing empty entries.
    /// </summary>
    /// <param name="text">The text to split.</param>
    /// <param name="delimiters">The character delimiters to split by.</param>
    /// <returns>A list of substrings including empty entries.</returns>
    public static List<string> SplitNoneChar(string text, params char[] delimiters)
    {
        return SplitChar(StringSplitOptions.None, text.RemoveInvisibleChars(), delimiters);
    }

    /// <summary>
    /// Splits a string by a list of character delimiters without removing empty entries.
    /// </summary>
    /// <param name="text">The text to split.</param>
    /// <param name="delimiters">The list of character delimiters.</param>
    /// <returns>A list of substrings including empty entries.</returns>
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
