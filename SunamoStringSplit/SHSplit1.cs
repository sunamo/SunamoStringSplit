namespace SunamoStringSplit;

public partial class SHSplit
{
    /// <summary>
    /// Split custom.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="characters">The characters.</param>
    /// <param name="isNotDelimiterFlags">The is not delimiter flags.</param>
    /// <param name="delimiterIndexes">The delimiter indexes.</param>
    /// <param name="delimiters">The delimiters.</param>
    public static void SplitCustom(string text, out List<char> characters, out List<bool> isNotDelimiterFlags, out List<int> delimiterIndexes, params char[] delimiters)
    {
        characters = new List<char>(text.Length);
        isNotDelimiterFlags = new List<bool>(text.Length);
        delimiterIndexes = new List<int>(text.Length / 6);
        for (var i = 0; i < text.Length; i++)
        {
            var isNotDelimiter = true;
            var character = text[i];
            foreach (var item in delimiters)
                if (item == character)
                {
                    delimiterIndexes.Add(i);
                    isNotDelimiter = false;
                    break;
                }

            characters.Add(character);
            isNotDelimiterFlags.Add(isNotDelimiter);
        }

        delimiterIndexes.Reverse();
    }

    /// <summary>
    /// Split from replace many format.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The split from replace many format result.</returns>
    public static Tuple<string, string> SplitFromReplaceManyFormat(string text)
    {
        var replacementBuilder = new StringBuilder();
        var searchBuilder = new StringBuilder();
        if (text.Contains("->"))
        {
            var lines = SHGetLines.GetLines(text);
            lines = lines.ConvertAll(element => element.Trim());
            foreach (var item in lines)
            {
                var parts = Split(item, "->");
                searchBuilder.AppendLine(parts[0]);
                replacementBuilder.AppendLine(parts[1]);
            }
        }
        else
        {
            searchBuilder.AppendLine(text);
        }

        return new Tuple<string, string>(searchBuilder.ToString(), replacementBuilder.ToString());
    }

    /// <summary>
    /// Split from replace many format list.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The split from replace many format list result.</returns>
    public static Tuple<List<string>, List<string>> SplitFromReplaceManyFormatList(string text)
    {
        var formatResult = SplitFromReplaceManyFormat(text);
        return new Tuple<List<string>, List<string>>(SHGetLines.GetLines(formatResult.Item1), SHGetLines.GetLines(formatResult.Item2));
    }

    /// <summary>
    /// Breaks every line longer than the limit at the last space that still fits; a line without such a space stays whole.
    /// </summary>
    /// <param name="text">The text whose lines are wrapped.</param>
    /// <param name="maxChars">The maximal length of a line.</param>
    /// <returns>The wrapped text, lines joined by the platform newline.</returns>
    public static string SplitParagraphToMaxChars(string text, int maxChars)
    {
        var wrapped = new List<string>();
        foreach (var line in text.Split(["\r\n", "\n", "\r"], StringSplitOptions.None))
        {
            var rest = line;
            while (rest.Length > maxChars)
            {
                var spaceIndex = rest.LastIndexOf(' ', maxChars);
                if (spaceIndex < 1)
                    break;
                wrapped.Add(rest.Substring(0, spaceIndex));
                rest = rest.Substring(spaceIndex + 1);
            }

            wrapped.Add(rest);
        }

        return string.Join(Environment.NewLine, wrapped);
    }

    /// <summary>
    /// Split to int list.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="delimiters">The delimiters.</param>
    /// <returns>The split to int list result.</returns>
    public static List<int> SplitToIntList(string text, params string[] delimiters)
    {
        var parts = Split(text.RemoveInvisibleChars(), delimiters);
        var result = new List<int>(parts.Count);
        foreach (var item in parts)
            result.Add(int.Parse(item));
        return result;
    }

    /// <summary>
    /// Split to int list none.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="delimiters">The delimiters.</param>
    /// <returns>The split to int list none result.</returns>
    public static List<int> SplitToIntListNone(string text, params string[] delimiters)
    {
        List<int> result;
        text = text.Trim().RemoveInvisibleChars();
        if (text != "")
        {
            var parts = SplitNone(text, delimiters);
            result = new List<int>(parts.Count);
            foreach (var item in parts)
                result.Add(int.Parse(item));
        }
        else
        {
            result = new List<int>();
        }

        return result;
    }

    /// <summary>
    /// Split to parts.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="parts">The parts.</param>
    /// <param name="delimiter">The delimiter.</param>
    /// <returns>The split to parts result.</returns>
    public static List<string>? SplitToParts(string text, int parts, string delimiter)
    {
        var splitParts = Split(text.RemoveInvisibleChars(), delimiter);
        if (splitParts.Count < parts)
        {
            if (splitParts.Count > 0)
            {
                var paddedResult = new List<string>();
                for (var i = 0; i < parts; i++)
                    if (i < splitParts.Count)
                        paddedResult.Add(splitParts[i]);
                    else
                        paddedResult.Add("");
                return paddedResult;
            }

            return null;
        }

        if (splitParts.Count == parts)
            return splitParts;
        parts--;
        var result = new List<string>();
        for (var i = 0; i < splitParts.Count; i++)
            if (i < parts)
                result.Add(splitParts[i]);
            else if (i == parts)
                result.Add(splitParts[i] + delimiter);
            else if (i != splitParts.Count - 1)
                result[parts] += splitParts[i] + delimiter;
            else
                result[parts] += splitParts[i];
        return result;
    }
}
