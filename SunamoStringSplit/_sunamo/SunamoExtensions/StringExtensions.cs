namespace SunamoStringSplit._sunamo.SunamoExtensions;

/// <summary>
/// Extension methods for string operations.
/// </summary>
internal static class StringExtensions
{
    /// <summary>
    /// Removes invisible Unicode characters (such as zero-width joiner) from a string.
    /// </summary>
    /// <param name="text">The input string to clean.</param>
    /// <returns>The string with invisible characters removed.</returns>
    internal static string RemoveInvisibleChars(this string text)
    {
        int[] charsToRemove = [8205];
        return new string(text.ToCharArray()
            .Where(character => !charsToRemove.Contains((int)character))
            .ToArray());
    }
}
