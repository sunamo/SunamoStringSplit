namespace SunamoStringSplit._sunamo;

internal class CA
{
    internal static bool IsListStringWrappedInArray<T>(List<T> v2)
    {
        var first = v2.First().ToString();
        if (v2.Count == 1 && (first == "System.Collections.Generic.List`1[System.String]" ||
                              first == "System.Collections.Generic.List`1[System.Object]")) return true;
        return false;
    }

    internal static List<string> Trim(List<string> l)
    {
        for (var i = 0; i < l.Count; i++) l[i] = l[i].Trim();
        return l;
    }


    internal static void InitFillWith(List<string> datas, int pocet, string initWith = Consts.stringEmpty)
    {
        InitFillWith<string>(datas, pocet, initWith);
    }

    internal static void InitFillWith<T>(List<T> datas, int pocet, T initWith)
    {
        for (var i = 0; i < pocet; i++) datas.Add(initWith);
    }

    internal static void InitFillWith<T>(List<T> arr, int columns)
    {
        for (var i = 0; i < columns; i++) arr.Add(default);
    }
}