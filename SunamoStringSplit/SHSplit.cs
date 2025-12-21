namespace SunamoStringSplit;

// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
public partial class SHSplit
{
    public static char[] spaceAndPuntactionChars =
    {
        ' ',
        '-',
        '.',
        ',',
        ';',
        ':',
        '!',
        '?',
        '\u2013',
        '\u2014',
        '\u2010',
        '\u2026',
        '\u201E',
        '\u201C',
        '\u201A',
        '\u2018',
        '\u00BB',
        '\u00AB',
        '\u2019',
        '\\',
        '(',
        ')',
        ']',
        '[',
        '{',
        '}',
        '\u3008',
        '\u3009',
        '<',
        '>',
        '/',
        '\\',
        '|',
        '\u201D',
        '"',
        '~',
        '\u00B0',
        '+',
        '@',
        '#',
        '$',
        '%',
        '^',
        '&',
        '*',
        '=',
        '_',
        '\u02C7',
        '\u00A8',
        '\u00A4',
        '\u00F7',
        '\u00D7',
        '\u02DD'
    };
    private static bool Result;
    public static void RemoveWhichHaveWhitespaceAtBothSides(string text, List<int> bold)
    {
        for (var i = bold.Count - 1; i >= 0; i--)
            if (char.IsWhiteSpace(text[bold[i] - 1]) && char.IsWhiteSpace(text[bold[i] + 1]))
                bold.RemoveAt(i);
    }

    ///// <summary>
    ///// Zde to dává smysl, potřebuji i value SE
    ///// </summary>
    ///// <param name="n"></param>
    ///// <param name="v"></param>
    ///// <returns></returns>
    //public static string NullToStringOrDefault(object n, string value)
    //{
    //    return se.SH.NullToStringOrDefault(n, value);
    //}
    ///// <summary>
    /////     Nemůže to být string[], protože jak předávám třeba AllChars.whiteSpaceChars tak mi to z List
    /////     <char>
    /////         udělá Object[]<List
    /////         <char>
    /////             >
    /////             Usage: Exceptions.TypeAndMethodName
    ///// </summary>
    ///// <param name="parametry"></param>
    ///// <param name="deli"></param>
    ///// <returns></returns>
    //public static List<string> Split(string parametry, params string[] deli)
    //{
    //    return Split(StringSplitOptions.RemoveEmptyEntries, parametry, deli);
    //}
    //public static List<string> Split(string parametry, List<string> deli)
    //{
    //    return Split(StringSplitOptions.RemoveEmptyEntries, parametry, deli);
    //}
    //public static List<string> Split(string parametry, List<char> deli)
    //{
    //    return Split(StringSplitOptions.RemoveEmptyEntries, parametry, deli);
    //}
    /// <summary>
    ///     With these
    /// </summary>
    /// <param name = "stringSplitOptions"></param>
    /// <param name = "text"></param>
    /// <param name = "deli"></param>
    public static List<string> Split(StringSplitOptions stringSplitOptions, string text, params string[] deli)
    {
        if (deli == null || deli.Count() == 0)
            throw new Exception("NoDelimiterDetermined");
        //var ie = CA.OneElementCollectionToMulti(deli);
        //var deli3 = new List<string>IEnumerable2(ie);
        var result = text.RemoveInvisibleChars().Split(deli, stringSplitOptions).ToList();
        CA.Trim(result);
        if (stringSplitOptions == StringSplitOptions.RemoveEmptyEntries)
            result = result.Where(data => data.Trim() != string.Empty).ToList();
        return result;
    }

    public static List<string> SplitAndKeepDelimiters(string originalString, List<string> ienu)
    {
        //var ienu = (IList)deli;
        var vr = Regex.Split(originalString.RemoveInvisibleChars(), @"(?<=[" + string.Join("", ienu) + "])");
        return vr.ToList();
    }

    public static List<string> SplitAndReturnRegexMatches(string input, Regex r, params char[] del)
    {
        var vr = new List<string>();
        var ds = SplitChar(input, del);
        foreach (var item in ds)
            if (r.IsMatch(item))
                vr.Add(item);
        return vr;
    }

    /// <summary>
    ///     Před voláním této metody se musíš ujistit že A2 není úplně na konci
    /// </summary>
    /// <param name = "p"></param>
    /// <param name = "firstNormal"></param>
    /// <param name = "title"></param>
    /// <param name = "remix"></param>
    public static void SplitByIndex(string parameter, int firstNormal, out string title, out string remix)
    {
        title = parameter.Substring(0, firstNormal);
        remix = parameter.Substring(firstNormal + 1);
    }

    public static List<string> SplitByIndexes(string input, List<int> bm)
    {
        var data = new List<string>(bm.Count + 1);
        bm.Sort();
        string before, after;
        before = input;
        for (var i = bm.Count - 1; i >= 0; i--)
        {
            (before, after) = SH.GetPartsByLocationNoOutInt(before, bm[i]);
            data.Insert(0, after);
        }

        (before, after) = SH.GetPartsByLocationNoOutInt(before, bm[0]);
        data.Insert(0, before);
        data.Reverse();
        return data;
    }

    /// <summary>
    ///     In case that delimiter cannot be found, to A2,3 set null
    ///     Before calling this method I must assure that A1 havent A4 on end
    /// </summary>
    /// <param name = "input"></param>
    /// <param name = "filePath2"></param>
    /// <param name = "fileName"></param>
    /// <param name = "backslash"></param>
    public static void SplitByLastCharToTwoParts(string input, out string filePath, out string fileName, char delimiter)
    {
        var dex = input.LastIndexOf(delimiter);
        if (dex != -1)
        {
            SplitByIndex(input.RemoveInvisibleChars(), dex, out filePath, out fileName);
        }
        else
        {
            filePath = null;
            fileName = null;
        }
    }

    public static List<string> SplitByLetterCount(string text, int c)
    {
        text = text.RemoveInvisibleChars();
        var sl = text.Length;
        var element = sl / c;
        var remain = sl % c;
        if (remain != 0)
            throw new Exception("NumbersOfLetters" + " " + text + " is not dividable with " + c);
        var sourceList = new List<string>(c);
        var from = 0;
        while (text.Length > from + c - 2)
        {
            sourceList.Add(text.Substring(from, c));
            from += c;
            if (from == sl)
                break;
        }

        return sourceList;
    }

    public static List<string> SplitByNewLines(string pull)
    {
        return Split(pull, "\n", "\r");
    }

    public static List<string> SplitBySpaceAndPunctuationChars(string text)
    {
        return SplitChar(text.RemoveInvisibleChars(), spaceAndPuntactionChars);
    }

    public static List<string> SplitBySpaceAndPunctuationCharsAndWhiteSpaces(string text)
    {
        throw new NotImplementedException();
    //return text.Split(s_spaceAndPuntactionCharsAndWhiteSpaces).ToList();
    }

    /// <summary>
    ///     Do výsledku zahranu i mezery a punktační znaménka
    /// </summary>
    /// <param name = "veta"></param>
    public static List<string> SplitBySpaceAndPunctuationCharsLeave(string veta)
    {
        var vr = new List<string>();
        vr.Add("");
        foreach (var item in veta.RemoveInvisibleChars())
        {
            var jeMezeraOrPunkce = false;
            foreach (var item2 in spaceAndPuntactionChars)
                if (item == item2)
                {
                    jeMezeraOrPunkce = true;
                    break;
                }

            if (jeMezeraOrPunkce)
            {
                if (vr[vr.Count - 1] == "")
                    vr[vr.Count - 1] += item.ToString();
                else
                    vr.Add(item.ToString());
                vr.Add("");
            }
            else
            {
                vr[vr.Count - 1] += item.ToString();
            }
        }

        return vr;
    }

    public static List<string> SplitByWhiteSpaces(string text, bool removeEmpty = false)
    {
        WhitespaceCharService whitespaceChar = new();
        whitespaceChar.ConvertWhiteSpaceCodesToChars();
        if (whitespaceChar == null)
        {
            ThrowEx.Custom($"whitespaceChar.whiteSpaceChars is not initialized");
            ;
        }

        text = text.RemoveInvisibleChars();
        List<string> r = null;
        if (removeEmpty)
        {
            //r = text.Split(AllChars.whiteSpaceChars.ToArray()).ToList();
            r = SplitChar(text, whitespaceChar.whiteSpaceChars.ToArray()).ToList();
        }
        else
            //r = text.Split(AllChars.whiteSpaceChars.ToArray(), StringSplitOptions.None).ToList();
            r = SplitNone(text, whitespaceChar.whiteSpaceChars.ConvertAll(data => data.ToString()).ToArray()).ToList();
        return r;
    }
}