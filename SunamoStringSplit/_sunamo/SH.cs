namespace SunamoStringSplit._sunamo;

/// <summary>
/// String helper utility class.
/// </summary>
internal class SH
{
    /// <summary>
    /// Counts occurrences of a substring in a source string.
    /// </summary>
    /// <param name="source">The source string to search in.</param>
    /// <param name="searchText">The substring to count occurrences of.</param>
    /// <returns>The number of occurrences found.</returns>
    internal static int OccurencesOfStringIn(string source, string searchText)
    {
        return source.Split(new[] { searchText }, StringSplitOptions.None).Length - 1;
    }

    /// <summary>
    /// Splits a string into two parts at the specified position.
    /// </summary>
    /// <param name="text">The text to split.</param>
    /// <param name="position">The index position at which to split.</param>
    /// <returns>A tuple containing the part before and after the position.</returns>
    internal static (string, string) GetPartsByLocationNoOutInt(string text, int position)
    {
        GetPartsByLocation(out string before, out string after, text, position);
        return (before, after);
    }

    /// <summary>
    /// Splits a string into two parts at the specified position using out parameters.
    /// </summary>
    /// <param name="before">The part of the string before the position.</param>
    /// <param name="after">The part of the string after the position.</param>
    /// <param name="text">The text to split.</param>
    /// <param name="position">The index position at which to split.</param>
    internal static void GetPartsByLocation(out string before, out string after, string text, int position)
    {
        if (position == -1)
        {
            before = text;
            after = "";
        }
        else
        {
            before = text.Substring(0, position);
            if (text.Length > position + 1)
                after = text.Substring(position + 1);
            else
                after = string.Empty;
        }
    }

    /// <summary>
    /// Returns all index positions where a substring occurs in a string.
    /// </summary>
    /// <param name="text">The text to search in.</param>
    /// <param name="searchText">The substring to find occurrences of.</param>
    /// <returns>A list of index positions where the substring occurs.</returns>
    internal static List<int> ReturnOccurencesOfString(string text, string searchText)
    {
        var results = new List<int>();
        for (var index = 0; index < text.Length - searchText.Length + 1; index++)
        {
            var substring = text.Substring(index, searchText.Length);

            if (substring == searchText)
                results.Add(index);
        }

        return results;
    }
}
