// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoStringSplit._sunamo.SunamoExtensions;

internal static class StringExtensions
{
    internal static string RemoveInvisibleChars(this string input)
    {
        int[] charsToRemove = [8205];
        return new string(input.ToCharArray()
            .Where(c => !charsToRemove.Contains((int)c))
            .ToArray());
    }
}