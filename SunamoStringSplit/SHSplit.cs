namespace SunamoStringSplit;

/// <summary>
/// Represents the SH split type.
/// </summary>
public partial class SHSplit
{
    /// <summary>
    /// The space and punctuation chars.
    /// </summary>
    public static char[] SpaceAndPunctuationChars =
    {
        ' ',
        '-',
        '.',
        ',',
        ';',
        ':',
        '!',
        '?',
        '–',
        '—',
        '‐',
        '…',
        '„',
        '“',
        '‚',
        '‘',
        '»',
        '«',
        '’',
        '\\',
        '(',
        ')',
        ']',
        '[',
        '{',
        '}',
        '〈',
        '〉',
        '<',
        '>',
        '/',
        '\\',
        '|',
        '”',
        '"',
        '~',
        '°',
        '+',
        '@',
        '#',
        '$',
        '%',
        '^',
        '&',
        '*',
        '=',
        '_',
        'ˇ',
        '¨',
        '¤',
        '÷',
        '×',
        '˝'
    };

    /// <summary>
    /// Remove which have whitespace at both sides.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="indexes">The indexes.</param>
    /// <returns>The remove which have whitespace at both sides result.</returns>
    public static void RemoveWhichHaveWhitespaceAtBothSides(string text, List<int> indexes)
    {
        for (var i = indexes.Count - 1; i >= 0; i--)
            if (char.IsWhiteSpace(text[indexes[i] - 1]) && char.IsWhiteSpace(text[indexes[i] + 1]))
                indexes.RemoveAt(i);
    }

    /// <summary>
    /// Initializes a new instance of the split class.
    /// </summary>
    /// <param name="i">The i.</param>
    public static List<string> Split(StringSplitOptions stringSplitOptions, string text, params string[] delimiters)
    {
        if (delimiters == null || delimiters.Length == 0)
            throw new Exception("NoDelimiterDetermined");
        var result = text.RemoveInvisibleChars().Split(delimiters, stringSplitOptions).ToList();
        CA.Trim(result);
        if (stringSplitOptions == StringSplitOptions.RemoveEmptyEntries)
            result = result.Where(element => element.Trim() != string.Empty).ToList();
        return result;
    }

    /// <summary>
    /// Split and keep delimiters.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="list">The list.</param>
    /// <returns>The split and keep delimiters result.</returns>
    public static List<string> SplitAndKeepDelimiters(string text, List<string> list)
    {
        var result = Regex.Split(text.RemoveInvisibleChars(), @"(?<=[" + string.Join("", list) + "])");
        return result.ToList();
    }

    /// <summary>
    /// Split and return regex matches.
    /// </summary>
    /// <returns>The split and return regex matches result.</returns>
    public static List<string> SplitAndReturnRegexMatches(string text, Regex regex, params char[] delimiters)
    {
        var result = new List<string>();
        var list = SplitChar(text, delimiters);
        foreach (var item in list)
            if (regex.IsMatch(item))
                result.Add(item);
        return result;
    }

    /// <summary>
    /// Split by index.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="index">The index.</param>
    /// <param name="before">The before.</param>
    /// <param name="after">The after.</param>
    /// <returns>The split by index result.</returns>
    public static void SplitByIndex(string text, int index, out string before, out string after)
    {
        before = text.Substring(0, index);
        after = text.Substring(index + 1);
    }

    /// <summary>
    /// Split by indexes.
    /// </summary>
    /// <param name="1">The 1.</param>
    /// <returns>The split by indexes result.</returns>
    public static List<string> SplitByIndexes(string text, List<int> indexes)
    {
        var result = new List<string>(indexes.Count + 1);
        indexes.Sort();
        string before, after;
        before = text;
        for (var i = indexes.Count - 1; i >= 0; i--)
        {
            (before, after) = SH.GetPartsByLocationNoOutInt(before, indexes[i]);
            result.Insert(0, after);
        }

        (before, after) = SH.GetPartsByLocationNoOutInt(before, indexes[0]);
        result.Insert(0, before);
        result.Reverse();
        return result;
    }

    /// <summary>
    /// Split by last char to two parts.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="before">The before.</param>
    /// <param name="after">The after.</param>
    /// <param name="delimiter">The delimiter.</param>
    /// <returns>The split by last char to two parts result.</returns>
    public static void SplitByLastCharToTwoParts(string text, out string? before, out string? after, char delimiter)
    {
        var lastIndex = text.LastIndexOf(delimiter);
        if (lastIndex != -1)
        {
            SplitByIndex(text.RemoveInvisibleChars(), lastIndex, out before, out after);
        }
        else
        {
            before = null;
            after = null;
        }
    }

    /// <summary>
    /// Split by letter count.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="count">The count.</param>
    /// <returns>The split by letter count result.</returns>
    public static List<string> SplitByLetterCount(string text, int count)
    {
        text = text.RemoveInvisibleChars();
        var textLength = text.Length;
        var remainder = textLength % count;
        if (remainder != 0)
            throw new Exception("NumbersOfLetters" + " " + text + " is not dividable with " + count);
        var result = new List<string>(count);
        var startIndex = 0;
        while (text.Length > startIndex + count - 2)
        {
            result.Add(text.Substring(startIndex, count));
            startIndex += count;
            if (startIndex == textLength)
                break;
        }

        return result;
    }

    /// <summary>
    /// Split by new lines.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The split by new lines result.</returns>
    public static List<string> SplitByNewLines(string text)
    {
        return Split(text, "\n", "\r");
    }

    /// <summary>
    /// Split by space and punctuation chars.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name=""\n"">The n.</param>
    /// <param name=""\r"">The r.</param>
    /// <returns>The split by space and punctuation chars result.</returns>
    public static List<string> SplitBySpaceAndPunctuationChars(string text)
    {
        return SplitChar(text.RemoveInvisibleChars(), SpaceAndPunctuationChars);
    }

    /// <summary>
    /// Split by space and punctuation chars and white spaces.
    /// </summary>
    /// <param name="text.RemoveInvisibleChars()">The text remove invisible chars.</param>
    /// <param name="SpaceAndPunctuationChars">The space and punctuation chars.</param>
    /// <returns>The split by space and punctuation chars and white spaces result.</returns>
    public static List<string> SplitBySpaceAndPunctuationCharsAndWhiteSpaces(string text)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Split by space and punctuation chars leave.
    /// </summary>
    /// <returns>The split by space and punctuation chars leave result.</returns>
    public static List<string> SplitBySpaceAndPunctuationCharsLeave(string text)
    {
        var result = new List<string>();
        result.Add("");
        foreach (var item in text.RemoveInvisibleChars())
        {
            var isSpaceOrPunctuation = false;
            foreach (var punctuationChar in SpaceAndPunctuationChars)
                if (item == punctuationChar)
                {
                    isSpaceOrPunctuation = true;
                    break;
                }

            if (isSpaceOrPunctuation)
            {
                if (result[result.Count - 1] == "")
                    result[result.Count - 1] += item.ToString();
                else
                    result.Add(item.ToString());
                result.Add("");
            }
            else
            {
                result[result.Count - 1] += item.ToString();
            }
        }

        return result;
    }

    /// <summary>
    /// Split by white spaces.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="isRemovingEmpty">The is removing empty.</param>
    /// <returns>The split by white spaces result.</returns>
    public static List<string> SplitByWhiteSpaces(string text, bool isRemovingEmpty = false)
    {
        WhitespaceCharService whitespaceCharService = new();
        whitespaceCharService.ConvertWhiteSpaceCodesToChars();
        if (whitespaceCharService.WhiteSpaceChars is null)
        {
            ThrowEx.Custom("whitespaceCharService.WhiteSpaceChars is not initialized");
        }

        var whiteSpaceChars = whitespaceCharService.WhiteSpaceChars!;
        text = text.RemoveInvisibleChars();
        List<string> result;
        if (isRemovingEmpty)
        {
            result = SplitChar(text, whiteSpaceChars.ToArray()).ToList();
        }
        else
        {
            result = SplitNone(text, whiteSpaceChars.ConvertAll(element => element.ToString()).ToArray()).ToList();
        }
        return result;
    }
}
