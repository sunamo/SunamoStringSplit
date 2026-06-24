namespace SunamoStringSplit._sunamo.SunamoExtensions;

internal static class ListExtensions
{
    internal static List<T> AddOrSet<T>(this IList<T> list, int index, T value)
    {
        if (list.Count > index)
            list[index] = value;
        else
            list.Add(value);
        return list.ToList();
    }
}
