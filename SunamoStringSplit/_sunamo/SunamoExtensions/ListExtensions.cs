// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoStringSplit._sunamo.SunamoExtensions;

internal static class ListExtensions
{
    internal static List<T> AddOrSet<T>(this IList<T> list, int dx, T item)
    {
        if (list.Count > dx)
            list[dx] = item;
        else
            list.Add(item);
        return list.ToList();
    }
}