// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoStringSplit;
public partial class SHSplit
{
    /// <summary>
    ///     value A2 vrátí jednotlivé znaky z A1, value A3 bude false, pokud znak value A2 bude delimiter, jinak True
    /// </summary>
    /// <param name = "what"></param>
    /// <param name = "chs"></param>
    /// <param name = "bs"></param>
    /// <param name = "reverse"></param>
    /// <param name = "deli"></param>
    public static void SplitCustom(string what, out List<char> chs, out List<bool> bs, out List<int> delimitersIndexes, params char[] deli)
    {
        chs = new List<char>(what.Length);
        bs = new List<bool>(what.Length);
        delimitersIndexes = new List<int>(what.Length / 6);
        for (var i = 0; i < what.Length; i++)
        {
            var isNotDeli = true;
            var ch = what[i];
            foreach (var item in deli)
                if (item == ch)
                {
                    delimitersIndexes.Add(i);
                    isNotDeli = false;
                    break;
                }

            chs.Add(ch);
            bs.Add(isNotDeli);
        }

        delimitersIndexes.Reverse();
    }

    public static Tuple<string, string> SplitFromReplaceManyFormat(string input)
    {
        var to = new StringBuilder();
        var from = new StringBuilder();
        if (input.Contains("->"))
        {
            var lines = SHGetLines.GetLines(input);
            lines = lines.ConvertAll(data => data.Trim());
            foreach (var item in lines)
            {
                var parameter = Split(item, "->");
                from.AppendLine(parameter[0]);
                to.AppendLine(parameter[1]);
            }
        }
        else
        {
            from.AppendLine(input);
        }

        return new Tuple<string, string>(from.ToString(), to.ToString());
    }

    public static Tuple<List<string>, List<string>> SplitFromReplaceManyFormatList(string input)
    {
        var temp = SplitFromReplaceManyFormat(input);
        return new Tuple<List<string>, List<string>>(SHGetLines.GetLines(temp.Item1), SHGetLines.GetLines(temp.Item2));
    }

    public static string SplitParagraphToMaxChars(string text, int maxChars)
    {
        var parts = Split(text, Environment.NewLine);
        var data = new List<List<string>>();
        foreach (var item in parts)
            ThrowEx.NotImplementedMethod();
        //data.Add(new List<string>(item));
        var index = -1;
        foreach (var item in data)
        {
            var d1 = false;
            index++;
            var copy = item.ToList();
            var s1 = item[0];
            var f = s1.Length;
            if (f > maxChars)
            {
                var dxDots = SH.ReturnOccurencesOfString(s1, ".");
                var i = 0;
                var dx = 0;
                var alreadyProcessed = 0;
                var alreadyTrimmed = 0;
                foreach (var dxDot2 in dxDots)
                {
                    i++;
                    var dxDot = dxDot2 - alreadyTrimmed;
                    var s1C = s1.Length;
                    if (s1C > maxChars)
                    {
                        if (i > 1)
                            if (dxDot > maxChars)
                            {
                                string delimitingChars = null;
                                if (IsEndOfSentence(dxDot, s1, out delimitingChars))
                                {
                                    string before, after;
                                    // Zde jsem dal -1 místo -2, vracelo mi to na začátku rok
                                    // Může mi to občas přetáhnout limit 250 znaků ale furt je to lepší než mít na začátku rok
                                    var ddx = dxDots[i - 1] + 1;
                                    ddx -= alreadyTrimmed;
                                    (before, after) = SH.GetPartsByLocationNoOutInt(s1, ddx);
                                    after = after.Trim();
                                    if (after == string.Empty)
                                        after = "   ";
                                    if (char.IsLower(after[0]))
                                        continue;
                                    //dx++;
                                    alreadyProcessed++;
                                    ////DebugLogger.Instance.WriteLine("dx", dx);
                                    ////DebugLogger.Instance.WriteLine("alreadyProcessed", alreadyProcessed);
                                    ////DebugLogger.Instance.WriteLine("dx-alreadyProcessed", dx - alreadyProcessed);
                                    if (dx > 1)
                                        dx--;
                                    //dx -= alreadyProcessed;
                                    if (d1)
                                    {
                                    ////DebugLogger.Instance.WriteLine("i", i);
                                    }

                                    s1 = s1.Substring(ddx);
                                    //after = delimitingChars + after;
                                    int beforeC, afterC;
                                    beforeC = before.Length;
                                    afterC = after.Length;
                                    s1C = s1.Length;
                                    alreadyTrimmed += beforeC;
                                    if (d1)
                                    {
                                    ////DebugLogger.Instance.WriteLine("beforeC", beforeC);
                                    ////DebugLogger.Instance.WriteLine("afterC", afterC);
                                    ////DebugLogger.Instance.WriteLine("s1C", s1C);
                                    }

                                    var sourceList = data[index];
                                    if (d1)
                                    {
                                        var bC = SH.OccurencesOfStringIn(before, "ThisParagraphIsLongerThan500Characters" + ".");
                                        var aC = SH.OccurencesOfStringIn(after, "ThisParagraphIsLongerThan500Characters" + ".");
                                    ////DebugLogger.Instance.WriteLine("bC", bC);
                                    ////DebugLogger.Instance.WriteLine("aC", aC);
                                    }

                                    if (dx < 0)
                                    {
                                        var sb2 = new StringBuilder();
                                        foreach (var item3 in sourceList)
                                            sb2.AppendLine(item3);
                                        var txt = sb2.ToString();
                                    }

                                    sourceList.AddOrSet(dx, before);
                                    dx++;
                                    sourceList.AddOrSet(dx, after);
                                    dx++;
                                    if (d1)
                                    {
                                    ////DebugLogger.Instance.WriteLine("dx1", dx - 1);
                                    ////DebugLogger.Instance.WriteLine("dx2", dx);
                                    }
                                }
                            }
                    }
                    else
                    {
                        var sourceList = data[index];
                        s1 = s1.Replace(sourceList.Last(), string.Empty).Trim();
                        if (s1 != string.Empty)
                            sourceList.AddOrSet(dx, s1);
                        break;
                    }
                }
            }
        }

        var stringBuilder = new StringBuilder();
        foreach (var item in data)
            foreach (var line in item)
            {
                stringBuilder.AppendLine(line);
                stringBuilder.AppendLine();
            }

        return stringBuilder.ToString();
    }

    /// <summary>
    ///     Pokud něco nebude číslo, program vyvolá výjimku, protože parsuje metodou int.Parse
    /// </summary>
    /// <param name = "stringToSplit"></param>
    /// <param name = "delimiter"></param>
    public static List<int> SplitToIntList(string stringToSplit, params string[] delimiter)
    {
        var f = Split(stringToSplit.RemoveInvisibleChars(), delimiter);
        var nt = new List<int>(f.Count);
        foreach (var item in f)
            nt.Add(int.Parse(item));
        return nt;
    }

    public static List<int> SplitToIntListNone(string stringToSplit, params string[] delimiter)
    {
        List<int> nt = null;
        stringToSplit = stringToSplit.Trim().RemoveInvisibleChars();
        if (stringToSplit != "")
        {
            var f = SplitNone(stringToSplit, delimiter);
            nt = new List<int>(f.Count);
            foreach (var item in f)
                nt.Add(int.Parse(item));
        }
        else
        {
            nt = new List<int>();
        }

        return nt;
    }

    /// <summary>
    ///     Get null if count of getted parts was under A2.
    ///     Automatically add empty padding items at end if got lower than A2
    ///     Automatically join overloaded items to last by A2.
    /// </summary>
    /// <param name = "p"></param>
    /// <param name = "p_2"></param>
    public static List<string> SplitToParts(string what, int parts, string deli)
    {
        var text = Split(what.RemoveInvisibleChars(), deli);
        if (text.Count < parts)
        {
            // Pokud je pocet ziskanych partu mensi, vlozim do zbytku prazdne retezce
            if (text.Count > 0)
            {
                var vr2 = new List<string>();
                for (var i = 0; i < parts; i++)
                    if (i < text.Count)
                        vr2.Add(text[i]);
                    else
                        vr2.Add("");
                return vr2;
            //return new string[] { text[0] };
            }

            return null;
        }

        if (text.Count == parts)
            // Pokud pocet ziskanych partu souhlasim presne, vratim jak je
            return text;
        // Pokud je pocet ziskanych partu vetsi nez kolik ma byt, pripojim ty co josu navic do zbytku
        parts--;
        var vr = new List<string>();
        for (var i = 0; i < text.Count; i++)
            if (i < parts)
                vr.Add(text[i]);
            else if (i == parts)
                vr.Add(text[i] + deli);
            else if (i != text.Count - 1)
                vr[parts] += text[i] + deli;
            else
                vr[parts] += text[i];
        return vr;
    }
}