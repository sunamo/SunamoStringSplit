namespace SunamoStringSplit;

/// <summary>
/// Provides additional string splitting methods (partial class continuation).
/// </summary>
public partial class SHSplit
{
    /// <summary>
    /// Splits a string into individual characters with boolean flags indicating whether each character is a delimiter,
    /// and returns the delimiter indexes in reverse order.
    /// </summary>
    /// <param name="text">The text to split into characters.</param>
    /// <param name="characters">The list of individual characters from the text.</param>
    /// <param name="isNotDelimiterFlags">Boolean flags for each character: true if not a delimiter, false if delimiter.</param>
    /// <param name="delimiterIndexes">The indexes of delimiter characters, in reverse order.</param>
    /// <param name="delimiters">The delimiter characters to detect.</param>
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
    /// Parses a "replace many" format input string (lines with "->" separators) into search and replacement parts.
    /// </summary>
    /// <param name="text">The input in "search->replacement" format, one pair per line.</param>
    /// <returns>A tuple where Item1 is the search text and Item2 is the replacement text.</returns>
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
    /// Parses a "replace many" format input string into lists of search and replacement strings.
    /// </summary>
    /// <param name="text">The input in "search->replacement" format, one pair per line.</param>
    /// <returns>A tuple where Item1 is the list of search strings and Item2 is the list of replacement strings.</returns>
    public static Tuple<List<string>, List<string>> SplitFromReplaceManyFormatList(string text)
    {
        var formatResult = SplitFromReplaceManyFormat(text);
        return new Tuple<List<string>, List<string>>(SHGetLines.GetLines(formatResult.Item1), SHGetLines.GetLines(formatResult.Item2));
    }

    /// <summary>
    /// Splits text into paragraphs and further splits paragraphs that exceed the maximum character count at sentence boundaries.
    /// </summary>
    /// <param name="text">The text to split.</param>
    /// <param name="maxChars">The maximum number of characters per paragraph.</param>
    /// <returns>The text with long paragraphs split at sentence boundaries.</returns>
    public static string SplitParagraphToMaxChars(string text, int maxChars)
    {
        var parts = Split(text, Environment.NewLine);
        var paragraphs = new List<List<string>>();
        foreach (var item in parts)
            ThrowEx.NotImplementedMethod();
        var paragraphIndex = -1;
        foreach (var item in paragraphs)
        {
            paragraphIndex++;
            var currentText = item[0];
            var textLength = currentText.Length;
            if (textLength > maxChars)
            {
                var dotIndexes = SH.ReturnOccurencesOfString(currentText, ".");
                var dotCounter = 0;
                var partIndex = 0;
                var alreadyTrimmed = 0;
                foreach (var dotPosition in dotIndexes)
                {
                    dotCounter++;
                    var adjustedDotIndex = dotPosition - alreadyTrimmed;
                    var currentTextLength = currentText.Length;
                    if (currentTextLength > maxChars)
                    {
                        if (dotCounter > 1)
                            if (adjustedDotIndex > maxChars)
                            {
                                string? delimitingChars = null;
                                if (IsEndOfSentence(adjustedDotIndex, currentText, out delimitingChars))
                                {
                                    string before, after;
                                    var splitPosition = dotIndexes[dotCounter - 1] + 1;
                                    splitPosition -= alreadyTrimmed;
                                    (before, after) = SH.GetPartsByLocationNoOutInt(currentText, splitPosition);
                                    after = after.Trim();
                                    if (after == string.Empty)
                                        after = "   ";
                                    if (char.IsLower(after[0]))
                                        continue;

                                    if (partIndex > 1)
                                        partIndex--;

                                    currentText = currentText.Substring(splitPosition);
                                    var beforeLength = before.Length;
                                    alreadyTrimmed += beforeLength;

                                    var sourceList = paragraphs[paragraphIndex];

                                    sourceList.AddOrSet(partIndex, before);
                                    partIndex++;
                                    sourceList.AddOrSet(partIndex, after);
                                    partIndex++;
                                }
                            }
                    }
                    else
                    {
                        var sourceList = paragraphs[paragraphIndex];
                        currentText = currentText.Replace(sourceList.Last(), string.Empty).Trim();
                        if (currentText != string.Empty)
                            sourceList.AddOrSet(partIndex, currentText);
                        break;
                    }
                }
            }
        }

        var stringBuilder = new StringBuilder();
        foreach (var item in paragraphs)
            foreach (var line in item)
            {
                stringBuilder.AppendLine(line);
                stringBuilder.AppendLine();
            }

        return stringBuilder.ToString();
    }

    /// <summary>
    /// Splits a string by the specified delimiters and parses each part as an integer.
    /// Throws if any part is not a valid integer.
    /// </summary>
    /// <param name="text">The string to split and parse.</param>
    /// <param name="delimiters">The delimiters to split by.</param>
    /// <returns>A list of parsed integers.</returns>
    public static List<int> SplitToIntList(string text, params string[] delimiters)
    {
        var parts = Split(text.RemoveInvisibleChars(), delimiters);
        var result = new List<int>(parts.Count);
        foreach (var item in parts)
            result.Add(int.Parse(item));
        return result;
    }

    /// <summary>
    /// Splits a string by the specified delimiters without removing empty entries and parses each part as an integer.
    /// Returns an empty list if the input is empty or whitespace.
    /// </summary>
    /// <param name="text">The string to split and parse.</param>
    /// <param name="delimiters">The delimiters to split by.</param>
    /// <returns>A list of parsed integers.</returns>
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
    /// Splits a string into a specific number of parts.
    /// Returns null if the split produces no parts.
    /// Pads with empty strings if fewer parts than requested.
    /// Joins excess parts into the last element.
    /// </summary>
    /// <param name="text">The text to split.</param>
    /// <param name="parts">The desired number of parts.</param>
    /// <param name="delimiter">The delimiter to split by.</param>
    /// <returns>A list with exactly the requested number of parts, or null if no parts found.</returns>
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
