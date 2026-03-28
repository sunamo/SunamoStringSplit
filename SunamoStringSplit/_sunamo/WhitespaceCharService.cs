namespace SunamoStringSplit._sunamo;

/// <summary>
/// Service for managing whitespace character codes and their conversions.
/// </summary>
internal class WhitespaceCharService
{
    /// <summary>
    /// Unicode code points for various whitespace characters.
    /// </summary>
    internal readonly List<int> WhiteSpacesCodes = new(new[]
    {
        9, 10, 11, 12, 13, 32, 133, 160, 5760, 6158, 8192, 8193, 8194, 8195, 8196, 8197, 8198, 8199, 8200, 8201, 8202,
        8232, 8233, 8239, 8287, 12288
    });

    /// <summary>
    /// Cached list of whitespace characters converted from code points.
    /// </summary>
    internal List<char>? WhiteSpaceChars;

    /// <summary>
    /// Converts whitespace code points to their corresponding char representations.
    /// </summary>
    internal void ConvertWhiteSpaceCodesToChars()
    {
        if (WhiteSpaceChars != null)
        {
            return;
        }
        WhiteSpaceChars = new List<char>(WhiteSpacesCodes.Count);
        foreach (var item in WhiteSpacesCodes)
        {
            var text = char.ConvertFromUtf32(item);
            var character = Convert.ToChar(text);
            WhiteSpaceChars.Add(character);
        }
    }
}
