namespace SunamoStringSplit._sunamo.SunamoExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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