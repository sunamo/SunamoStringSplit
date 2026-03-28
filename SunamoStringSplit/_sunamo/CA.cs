namespace SunamoStringSplit._sunamo;

/// <summary>
/// Collection analysis utility class.
/// </summary>
internal class CA
{
    /// <summary>
    /// Trims whitespace from all elements in a list.
    /// </summary>
    /// <param name="list">The list of strings to trim.</param>
    /// <returns>The same list with all elements trimmed.</returns>
    internal static List<string> Trim(List<string> list)
    {
        for (var i = 0; i < list.Count; i++) list[i] = list[i].Trim();
        return list;
    }
}
