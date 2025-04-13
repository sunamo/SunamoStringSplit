namespace SunamoStringSplit;
partial class SHSplit
{
    public static List<string> Split(string parametry, string deli)
    {
        return Split(StringSplitOptions.RemoveEmptyEntries, parametry.RemoveInvisibleChars(), deli);
    }

    public static List<string> SplitList(string parametry, List<string> deli)
    {
        return Split(StringSplitOptions.RemoveEmptyEntries, parametry.RemoveInvisibleChars(), deli.ToArray());
    }

    public static List<string> Split(string parametry, params string[] deli)
    {
        return Split(StringSplitOptions.RemoveEmptyEntries, parametry.RemoveInvisibleChars(), deli);
    }

    public static List<string> SplitNone(string text, params string[] deli)
    {
        return text.RemoveInvisibleChars().Split(deli, StringSplitOptions.None).ToList();
    }

    private static List<string> Split(StringSplitOptions removeEmptyEntries, string parametry, List<char> deli)
    {
        var t = deli.ToList();
        var sep = new string[t.Count()];
        for (var i = 0; i < sep.Length; i++) sep[i] = t[i].ToString();
        var result = parametry.RemoveInvisibleChars().Split(sep, removeEmptyEntries).ToList();
        return result;
    }
}
