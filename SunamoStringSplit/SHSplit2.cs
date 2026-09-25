namespace SunamoStringSplit;

public partial class SHSplit
{
    public static void SplitToParts2(string text, string delimiter, ref string before, ref string after)
    {
        var parts = Split(text.RemoveInvisibleChars(), delimiter);
        before = parts[0];
        after = parts[1];
    }

    // Works but can be slow, try to use as little as possible.
    // If there are multiple consecutive delimiters, only the last one in the sequence is kept.
    public static List<string> SplitToPartsFromEnd(string text, int parts, params char[] delimiters)
    {
        SplitCustom(text, out List<char> characters, out List<bool> isNotDelimiterFlags, out List<int> delimiterIndexes, delimiters);
        var reversedParts = new List<string>(parts);
        var stringBuilder = new StringBuilder();
        for (var i = characters.Count - 1; i >= 0; i--)
            if (!isNotDelimiterFlags[i])
            {
                while (i != 0 && !isNotDelimiterFlags[i - 1])
                    i--;
                var segment = stringBuilder.ToString();
                stringBuilder.Clear();
                if (segment != "")
                    reversedParts.Add(segment);
            }
            else
            {
                stringBuilder.Insert(0, characters[i]);
            }

        var remainingText = stringBuilder.ToString();
        stringBuilder.Clear();
        if (remainingText != "")
            reversedParts.Add(remainingText);
        var result = new List<string>(parts);
        for (var i = 0; i < reversedParts.Count; i++)
            if (result.Count != parts)
            {
                result.Insert(0, reversedParts[i]);
            }
            else
            {
                var delimiterString = text[delimiterIndexes[i - 1]].ToString();
                result[0] = reversedParts[i] + delimiterString + result[0];
            }

        return result;
    }

    // Currently does not work 100% correctly. Use SplitToPartsFromEnd instead.
    // Returns null if the string is empty.
    // If fewer parts than requested, pads with empty strings.
    public static List<string>? SplitToPartsFromEnd2(string text, int parts, params char[] delimiters)
    {
        var delimiterIndexes = new List<int>();
        foreach (var item in delimiters)
            delimiterIndexes.AddRange(SH.ReturnOccurencesOfString(text, item.ToString()));
        delimiterIndexes.Sort();
        var splitParts = SplitChar(text, delimiters);
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
        var excessParts = splitParts.Count - parts - 1;
        if (parts < splitParts.Count - 1)
            parts++;
        var result = new List<string>(parts);
        for (; parts > excessParts; parts--)
            result.Insert(0, splitParts[parts]);
        parts++;
        for (var i = 1; i < parts; i++)
            result[0] = splitParts[i] + text[delimiterIndexes[i]] + result[0];
        result[0] = splitParts[0] + text[delimiterIndexes[0]] + result[0];
        return result;
    }

    private static bool IsEndOfSentence(int dotIndex, string text, out string? delimitingChars)
    {
        delimitingChars = null;
        var isEndOfSentence = false;
        var substring = text.Substring(dotIndex);
        var firstChar = substring[0];
        char secondChar = '@';
        char thirdChar = '@';
        if (substring.Length > 1)
        {
            secondChar = substring[1];
        }
        else
        {
            delimitingChars = substring.Substring(0);
            isEndOfSentence = true;
        }

        if (substring.Length > 2)
        {
            thirdChar = substring[2];
        }
        else
        {
            delimitingChars = substring.Substring(1);
            isEndOfSentence = true;
        }

        if (secondChar == ' ' && char.IsUpper(thirdChar))
        {
            delimitingChars = string.Join(string.Empty, firstChar, secondChar, thirdChar);
            isEndOfSentence = true;
        }

        if (char.IsUpper(secondChar))
        {
            delimitingChars = string.Join(string.Empty, firstChar, secondChar);
            isEndOfSentence = true;
        }

        return isEndOfSentence;
    }
}
