namespace SunamoStringSplit;

/// <summary>
/// Provides methods for splitting strings using various delimiters and strategies.
/// </summary>
public partial class SHSplit
{
    /// <summary>
    /// Characters considered as space and punctuation for splitting operations.
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
        '\u2013',
        '\u2014',
        '\u2010',
        '\u2026',
        '\u201E',
        '\u201C',
        '\u201A',
        '\u2018',
        '\u00BB',
        '\u00AB',
        '\u2019',
        '\\',
        '(',
        ')',
        ']',
        '[',
        '{',
        '}',
        '\u3008',
        '\u3009',
        '<',
        '>',
        '/',
        '\\',
        '|',
        '\u201D',
        '"',
        '~',
        '\u00B0',
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
        '\u02C7',
        '\u00A8',
        '\u00A4',
        '\u00F7',
        '\u00D7',
        '\u02DD'
    };

    /// <summary>
    /// Removes indexes from the list where the character at that position has whitespace on both sides.
    /// </summary>
    /// <param name="text">The text to check character positions in.</param>
    /// <param name="indexes">The list of character indexes to filter.</param>
    public static void RemoveWhichHaveWhitespaceAtBothSides(string text, List<int> indexes)
    {
        for (var i = indexes.Count - 1; i >= 0; i--)
            if (char.IsWhiteSpace(text[indexes[i] - 1]) && char.IsWhiteSpace(text[indexes[i] + 1]))
                indexes.RemoveAt(i);
    }

    /// <summary>
    /// Splits a string by the specified delimiters with the given split options.
    /// </summary>
    /// <param name="stringSplitOptions">Options for controlling the split behavior.</param>
    /// <param name="text">The text to split.</param>
    /// <param name="delimiters">The string delimiters to split by.</param>
    /// <returns>A list of substrings.</returns>
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
    /// Splits a string while keeping the delimiters attached to the preceding segments.
    /// </summary>
    /// <param name="text">The text to split.</param>
    /// <param name="list">The list of delimiter characters (as strings) to split by.</param>
    /// <returns>A list of substrings with delimiters preserved.</returns>
    public static List<string> SplitAndKeepDelimiters(string text, List<string> list)
    {
        var result = Regex.Split(text.RemoveInvisibleChars(), @"(?<=[" + string.Join("", list) + "])");
        return result.ToList();
    }

    /// <summary>
    /// Splits a string by character delimiters and returns only those parts matching the specified regex.
    /// </summary>
    /// <param name="text">The text to split and filter.</param>
    /// <param name="regex">The regex pattern to match parts against.</param>
    /// <param name="delimiters">The character delimiters to split by.</param>
    /// <returns>A list of parts that match the regex.</returns>
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
    /// Splits a string into two parts at the specified index position.
    /// Ensure that the index is not at the very end of the string before calling.
    /// </summary>
    /// <param name="text">The text to split.</param>
    /// <param name="index">The index position at which to split.</param>
    /// <param name="before">The part before the index.</param>
    /// <param name="after">The part after the index.</param>
    public static void SplitByIndex(string text, int index, out string before, out string after)
    {
        before = text.Substring(0, index);
        after = text.Substring(index + 1);
    }

    /// <summary>
    /// Splits a string into multiple parts at the specified index positions.
    /// </summary>
    /// <param name="text">The text to split.</param>
    /// <param name="indexes">The list of index positions at which to split.</param>
    /// <returns>A list of substrings.</returns>
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
    /// Splits a string into two parts at the last occurrence of the specified delimiter character.
    /// Sets both out parameters to null if the delimiter is not found.
    /// </summary>
    /// <param name="text">The text to split.</param>
    /// <param name="before">The part before the last delimiter occurrence, or null if not found.</param>
    /// <param name="after">The part after the last delimiter occurrence, or null if not found.</param>
    /// <param name="delimiter">The character delimiter to find.</param>
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
    /// Splits a string into segments of equal character length.
    /// Throws if the text length is not evenly divisible by the count.
    /// </summary>
    /// <param name="text">The text to split.</param>
    /// <param name="count">The number of characters per segment.</param>
    /// <returns>A list of equal-length substrings.</returns>
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
    /// Splits a string by newline characters (\n and \r).
    /// </summary>
    /// <param name="text">The text to split into lines.</param>
    /// <returns>A list of lines.</returns>
    public static List<string> SplitByNewLines(string text)
    {
        return Split(text, "\n", "\r");
    }

    /// <summary>
    /// Splits a string by space and punctuation characters.
    /// </summary>
    /// <param name="text">The text to split.</param>
    /// <returns>A list of substrings split by space and punctuation.</returns>
    public static List<string> SplitBySpaceAndPunctuationChars(string text)
    {
        return SplitChar(text.RemoveInvisibleChars(), SpaceAndPunctuationChars);
    }

    /// <summary>
    /// Splits a string by space, punctuation characters and whitespace characters.
    /// </summary>
    /// <param name="text">The text to split.</param>
    /// <returns>A list of substrings.</returns>
    public static List<string> SplitBySpaceAndPunctuationCharsAndWhiteSpaces(string text)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Splits a string by space and punctuation characters while keeping the delimiters in the result.
    /// </summary>
    /// <param name="text">The text to split.</param>
    /// <returns>A list of substrings with delimiters preserved as separate entries.</returns>
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
    /// Splits a string by whitespace characters.
    /// </summary>
    /// <param name="text">The text to split.</param>
    /// <param name="isRemovingEmpty">Whether to remove empty entries from the result.</param>
    /// <returns>A list of substrings split by whitespace.</returns>
    public static List<string> SplitByWhiteSpaces(string text, bool isRemovingEmpty = false)
    {
        WhitespaceCharService whitespaceCharService = new();
        whitespaceCharService.ConvertWhiteSpaceCodesToChars();
        if (whitespaceCharService.WhiteSpaceChars == null)
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
