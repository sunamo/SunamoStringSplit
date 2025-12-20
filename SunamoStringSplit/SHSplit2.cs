// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoStringSplit;
public partial class SHSplit
{
    public static void SplitToParts2(string df, string deli, ref string before, ref string after)
    {
        var parameter = Split(df.RemoveInvisibleChars(), deli);
        before = parameter[0];
        after = parameter[1];
    }

    /// <summary>
    ///     FUNGUJE ale může být pomalá, snaž se využívat co nejméně
    ///     Pokud někde bude více delimiterů těsně za sebou, ve výsledku toto nebude, bude tam jen poslední delimiter value té řadě
    ///     příklad z 1,.Par při delimiteru , a . bude 1.Par
    /// </summary>
    /// <param name = "what"></param>
    /// <param name = "parts"></param>
    /// <param name = "deli"></param>
    public static List<string> SplitToPartsFromEnd(string what, int parts, params char[] deli)
    {
        List<char> chs = null;
        List<bool> bw = null;
        List<int> delimitersIndexes = null;
        SplitCustom(what, out chs, out bw, out delimitersIndexes, deli);
        var vr = new List<string>(parts);
        var stringBuilder = new StringBuilder();
        for (var i = chs.Count - 1; i >= 0; i--)
            if (!bw[i])
            {
                while (i != 0 && !bw[i - 1])
                    i--;
                var data = stringBuilder.ToString();
                stringBuilder.Clear();
                if (data != "")
                    vr.Add(data);
            }
            else
            {
                stringBuilder.Insert(0, chs[i]);
            //stringBuilder.Append(chs[i]);
            }

        var d2 = stringBuilder.ToString();
        stringBuilder.Clear();
        if (d2 != "")
            vr.Add(d2);
        var value = new List<string>(parts);
        for (var i = 0; i < vr.Count; i++)
            if (value.Count != parts)
            {
                value.Insert(0, vr[i]);
            }
            else
            {
                var ds = what[delimitersIndexes[i - 1]].ToString();
                value[0] = vr[i] + ds + value[0];
            }

        return value;
    }

    /// <summary>
    ///     TODO: Zatím NEfunguje 100%ně, až někdy budeš mít chuť tak se můžeš pokusit tuto metodu opravit. Zatím ji
    ///     nepoužívej, místo ní používej pomalejší ale funkční SplitToPartsFromEnd
    ///     Vrátí null value případě že řetězec bude prázdný
    ///     Pokud bude mít A1 méně částí než A2, vratí nenalezené části jako SE
    /// </summary>
    /// <param name = "what"></param>
    /// <param name = "parts"></param>
    /// <param name = "deli"></param>
    public static List<string> SplitToPartsFromEnd2(string what, int parts, params char[] deli)
    {
        var indexyDelimiteru = new List<int>();
        foreach (var item in deli)
            indexyDelimiteru.AddRange(SH.ReturnOccurencesOfString(what, item.ToString()));
        //indexyDelimiteru.OrderBy(data => data);
        indexyDelimiteru.Sort();
        var text = SplitChar(what, deli);
        if (text.Count < parts)
        {
            //throw new Exception("");
            if (text.Count > 0)
            {
                var vr2 = new List<string>();
                for (var i = 0; i < parts; i++)
                    if (i < text.Count)
                        vr2.Add(text[i]);
                    else
                        vr2.Add("");
                return vr2;
            //return new List<string> { text[0] };
            }

            return null;
        }

        if (text.Count == parts)
            return text;
        var parts2 = text.Count - parts - 1;
        //parts += povysit;
        if (parts < text.Count - 1)
            parts++;
        var vr = new List<string>(parts);
        // Tady musí být 4 menší než 1, protože po 1. iteraci to bude 3,pak 2, pak 1
        for (; parts > parts2; parts--)
            vr.Insert(0, text[parts]);
        parts++;
        for (var i = 1; i < parts; i++)
            vr[0] = text[i] + what[indexyDelimiteru[i]] + vr[0];
        //}
        vr[0] = text[0] + what[indexyDelimiteru[0]] + vr[0];
        return vr;
    }

    private static bool IsEndOfSentence(int dxDot, string s1, out string delimitingChars)
    {
        delimitingChars = null;
        var text = s1.Substring(dxDot);
        var c0 = text[0];
        char c1, c2;
        c1 = '@';
        c2 = '@';
        if (text.Length > 1)
        {
            c1 = text[1];
        }
        else
        {
            delimitingChars = text.Substring(0);
            Result = true;
        }

        if (text.Length > 2)
        {
            c2 = text[2];
        }
        else
        {
            delimitingChars = text.Substring(1);
            Result = true;
        }

        if (c1 == ' ' && char.IsUpper(c2))
        {
            delimitingChars = string.Join(string.Empty, c0, c1, c2);
            Result = true;
        }

        if (char.IsUpper(c1))
        {
            delimitingChars = string.Join(string.Empty, c0, c1);
            Result = true;
        }

        return Result;
    }
}