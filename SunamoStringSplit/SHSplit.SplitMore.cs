namespace SunamoStringSplit;

public partial class SHSplit
{
    /// <summary>
    /// Split more.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="delimiters">The delimiters.</param>
    /// <returns>The split more result.</returns>
    public static List<string> SplitMore(string text, params char[] delimiters)
    {
        return text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    /// <summary>
    /// Split more.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="delimiters">The delimiters.</param>
    /// <returns>The split more result.</returns>
    public static List<string> SplitMore(string text, params string[] delimiters)
    {
        return text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToList();
    }
}
