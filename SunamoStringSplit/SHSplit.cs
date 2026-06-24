namespace SunamoStringSplit;

public partial class SHSplit
{
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

    public static void RemoveWhichHaveWhitespaceAtBothSides(string text, List<int> indexes)
    {
        for (var i = indexes.Count - 1; i >= 0; i--)
            if (char.IsWhiteSpace(text[indexes[i] - 1]) && char.IsWhiteSpace(text[indexes[i] + 1]))
                indexes.RemoveAt(i);
    }

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

    public static List<string> SplitAndKeepDelimiters(string text, List<string> list)
    {
        var result = Regex.Split(text.RemoveInvisibleChars(), @"(?<=[" + string.Join("", list) + "])");
        return result.ToList();
    }

    public static List<string> SplitAndReturnRegexMatches(string text, Regex regex, params char[] delimiters)
    {
        var result = new List<string>();
        var list = SplitChar(text, delimiters);
        foreach (var item in list)
            if (regex.IsMatch(item))
                result.Add(item);
        return result;
    }

    public static void SplitByIndex(string text, int index, out string before, out string after)
    {
        before = text.Substring(0, index);
        after = text.Substring(index + 1);
    }

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

    public static List<string> SplitByNewLines(string text)
    {
        return Split(text, "\n", "\r");
    }

    public static List<string> SplitBySpaceAndPunctuationChars(string text)
    {
        return SplitChar(text.RemoveInvisibleChars(), SpaceAndPunctuationChars);
    }

    public static List<string> SplitBySpaceAndPunctuationCharsAndWhiteSpaces(string text)
    {
        throw new NotImplementedException();
    }

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
