namespace SunamoStringSplit._sunamo.SunamoExtensions;

internal static class StringExtensions
{
    internal static string RemoveInvisibleChars(this string text)
    {
        int[] charsToRemove = [8205];
        return new string(text.ToCharArray()
            .Where(character => !charsToRemove.Contains((int)character))
            .ToArray());
    }
}
