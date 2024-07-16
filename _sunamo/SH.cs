namespace SunamoStringSplit._sunamo;

internal class SH
{

    internal static string JoinNL(List<string> l)
    {
        StringBuilder sb = new();
        foreach (var item in l) sb.AppendLine(item);
        var r = string.Empty;
        r = sb.ToString();
        return r;
    }


    internal static string FirstCharLower(string nazevPP)
    {
        if (nazevPP.Length < 2) return nazevPP;
        var sb = nazevPP.Substring(1);
        return nazevPP[0].ToString().ToLower() + sb;
    }
    /// <summary>
    ///     Convert \r\n to NewLine etc.
    /// </summary>
    /// <param name="delimiter"></param>
    internal static string ConvertTypedWhitespaceToString(string delimiter)
    {
        const string nl = @"
";
        switch (delimiter)
        {
            // must use \r\n, not Environment.NewLine (is not constant)
            case "\\r\\n":
            case "\\n":
            case "\\r":
                return nl;
            case "\\t":
                return "\t";
        }
        return delimiter;
    }
    /// <summary>
    ///     Musí tu být. split z .net vrací []
    ///     krom toho je instanční. musel bych měnit hodně kódu kvůli toho
    /// </summary>
    /// <param name="s"></param>
    /// <param name="dot"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    internal static List<string> SplitCharMore(string s, params char[] dot)
    {
        return s.Split(dot, StringSplitOptions.RemoveEmptyEntries).ToList();
    }
    internal static List<string> SplitMore(string s, params string[] dot)
    {
        return s.Split(dot, StringSplitOptions.RemoveEmptyEntries).ToList();
    }
    internal static List<string> SplitNone(string text, params string[] deli)
    {
        return text.Split(deli, StringSplitOptions.None).ToList();
    }
    /// <summary>
    ///     Usage: BadFormatOfElementInList
    ///     If null, return Consts.nulled
    ///     nemůžu odstranit z sunamo, i tam se používá.
    /// </summary>
    /// <param name="n"></param>
    /// <param name="v"></param>
    /// <returns></returns>
    internal static string NullToStringOrDefault(object n, string v)
    {
        throw new Exception(
        "Tahle metoda vypadala jinak ale jak idiot jsem ji změnil. Tím jak jsem poté přesouval metody tam zpět už je těžké se k tomu dostat.");
        return null;
        //return n == null ? " " + Consts.nulled : AllStrings.space + v.ToString();
    }
    /// <summary>
    ///     Usage: BadFormatOfElementInList
    ///     If null, return Consts.nulled
    ///     jsem
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    internal static string NullToStringOrDefault(object n)
    {
        //return NullToStringOrDefault(n, null);
        return n == null ? " " + Consts.nulled : AllStrings.space + n;
    }
    /// <summary>
    ///     Usage: Exceptions.MoreCandidates
    ///     není v .net (pouze char), přes split to taky nedává smysl (dá se to udělat i s .net ale bude to pomalejší)
    /// </summary>
    /// <param name="name"></param>
    /// <param name="ext"></param>
    /// <returns></returns>
    internal static string TrimEnd(string name, string ext)
    {
        while (name.EndsWith(ext)) return name.Substring(0, name.Length - ext.Length);
        return name;
    }


    internal static int OccurencesOfStringIn(string source, string p_2)
    {
        return source.Split(new string[] { p_2 }, StringSplitOptions.None).Length - 1;
    }

    internal static (string, string) GetPartsByLocationNoOutInt(string text, int pozice)
    {
        string pred, za;
        GetPartsByLocation(out pred, out za, text, pozice);
        return (pred, za);
    }

    internal static void GetPartsByLocation(out string pred, out string za, string text, int pozice)
    {
        if (pozice == -1)
        {
            pred = text;
            za = "";
        }
        else
        {
            pred = text.Substring(0, pozice);
            if (text.Length > pozice + 1)
            {
                za = text.Substring(pozice + 1);
            }
            else
            {
                za = string.Empty;
            }
        }
    }

    internal static List<int> ReturnOccurencesOfString(string vcem, string co)
    {

        List<int> Results = new List<int>();
        for (int Index = 0; Index < (vcem.Length - co.Length) + 1; Index++)
        {
            var subs = vcem.Substring(Index, co.Length);
            ////////DebugLogger.Instance.WriteLine(subs);
            // non-breaking space. &nbsp; code 160
            // 32 space
            char ch = subs[0];
            char ch2 = co[0];
            if (subs == AllStrings.space)
            {
            }
            if (subs == co)
                Results.Add(Index);
        }
        return Results;
    }
}
