namespace SunamoStringSplit;

partial class SHSplit
{
    public static List<string> SplitCharList(string parametry, List<char> deli)
    {
        return Split(StringSplitOptions.RemoveEmptyEntries, parametry.RemoveInvisibleChars(), deli.ConvertAll(d => d.ToString()).ToArray());
    }

    public static List<string> SplitChar(string parametry, params char[] deli)
    {
        return Split(StringSplitOptions.RemoveEmptyEntries, parametry.RemoveInvisibleChars(),
            deli.ToList().ConvertAll(d => d.ToString()).ToArray());
    }

    public static List<string> SplitNoneChar(string text, params char[] deli)
    {
        return SplitChar(StringSplitOptions.None, text.RemoveInvisibleChars(), deli);
    }

    public static List<string> SplitNoneCharList(string text, List<char> deli)
    {
        var converted = deli.ConvertAll(d => d.ToString());
        return Split(StringSplitOptions.None, text.RemoveInvisibleChars(), converted.ToArray());
    }

    private static List<string> SplitChar(StringSplitOptions removeEmptyEntries, string parametry,
        params char[] deli)
    {
        var temp = deli.ToList();
        var sep = new string[temp.Count()];
        for (var i = 0; i < sep.Length; i++) sep[i] = temp[i].ToString();
        var result = parametry.RemoveInvisibleChars().Split(sep, removeEmptyEntries).ToList();
        return result;
    }
}