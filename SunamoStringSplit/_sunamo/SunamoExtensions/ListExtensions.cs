namespace SunamoStringSplit._sunamo.SunamoExtensions;

/// <summary>
/// Extension methods for list operations.
/// </summary>
internal static class ListExtensions
{
    /// <summary>
    /// Sets the value at the specified index, or adds it if the index is beyond the current count.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <param name="list">The list to modify.</param>
    /// <param name="index">The target index.</param>
    /// <param name="value">The value to set or add.</param>
    /// <returns>The modified list as a new List instance.</returns>
    internal static List<T> AddOrSet<T>(this IList<T> list, int index, T value)
    {
        if (list.Count > index)
            list[index] = value;
        else
            list.Add(value);
        return list.ToList();
    }
}
