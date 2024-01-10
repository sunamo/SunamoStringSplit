namespace SunamoStringSplit._sunamo;
internal class SH
{
    internal static Func<string, string, List<int>> ReturnOccurencesOfString;
    internal static Func<string, char, (string, string)> GetPartsByLocationNoOut;
    internal static Func<string, int, (string, string)> GetPartsByLocationNoOutInt;
    internal static Func<string, string, string, string> ReplaceOnce;
    internal static Func<string, string, int> OccurencesOfStringIn;
    internal static Func<string, int, string> SubstringIfAvailableStart;

}
