namespace SunamoStringSplit;

//namespace SunamoStringSplit;
public class SHSH
{
    public static int OccurencesOfStringIn(string source, string p_2)
    {
        return source.Split(new string[] { p_2 }, StringSplitOptions.None).Length - 1;
    }

    //    public static Func<string, string, List<int>> ReturnOccurencesOfString;
    //    //public static Func<string, char, (string, string)> GetPartsByLocationNoOut;
    //    public static Func<string, int, (string, string)> GetPartsByLocationNoOutInt;
    //    //public static Func<string, string, string, string> ReplaceOnce;
    //    public static Func<string, string, int> OccurencesOfStringIn;
    //    //public static Func<string, int, string> SubstringIfAvailableStart;

    public static void GetPartsByLocation(out string pred, out string za, string text, int pozice)
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

    public static (string, string) GetPartsByLocationNoOutInt(string text, int pozice)
    {
        string pred, za;
        GetPartsByLocation(out pred, out za, text, pozice);
        return (pred, za);
    }
    public static List<int> ReturnOccurencesOfString(string vcem, string co)
    {
        //vcem = NormalizeString(vcem);
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
