namespace SunamoStringSplit;

/// <summary>
/// Provides string-based string splitting methods.
/// </summary>
partial class SHSplit
{
    /// <summary>
    /// Splits a string by a single delimiter, removing empty entries.
    /// </summary>
    /// <param name="text">The text to split.</param>
    /// <param name="delimiter">The delimiter to split by.</param>
    /// <returns>A list of substrings.</returns>
    public static List<string> Split(string text, string delimiter)
    {
        return Split(StringSplitOptions.RemoveEmptyEntries, text.RemoveInvisibleChars(), delimiter);
    }

    /// <summary>
    /// Splits a string by a list of delimiters, removing empty entries.
    /// </summary>
    /// <param name="text">The text to split.</param>
    /// <param name="delimiters">The list of delimiters to split by.</param>
    /// <returns>A list of substrings.</returns>
    public static List<string> SplitList(string text, List<string> delimiters)
    {
        return Split(StringSplitOptions.RemoveEmptyEntries, text.RemoveInvisibleChars(), delimiters.ToArray());
    }

    /// <summary>
    /// Splits a string by the specified delimiters, removing empty entries.
    /// </summary>
    /// <param name="text">The text to split.</param>
    /// <param name="delimiters">The delimiters to split by.</param>
    /// <returns>A list of substrings.</returns>
    public static List<string> Split(string text, params string[] delimiters)
    {
        return Split(StringSplitOptions.RemoveEmptyEntries, text.RemoveInvisibleChars(), delimiters);
    }

    /// <summary>
    /// Splits a string by the specified delimiters without removing empty entries.
    /// </summary>
    /// <param name="text">The text to split.</param>
    /// <param name="delimiters">The delimiters to split by.</param>
    /// <returns>A list of substrings including empty entries.</returns>
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
