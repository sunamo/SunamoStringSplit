namespace SunamoStringSplit;

public partial class SHSplit
{
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

    public static Tuple<List<string>, List<string>> SplitFromReplaceManyFormatList(string text)
    {
        var formatResult = SplitFromReplaceManyFormat(text);
        return new Tuple<List<string>, List<string>>(SHGetLines.GetLines(formatResult.Item1), SHGetLines.GetLines(formatResult.Item2));
    }

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
                                if (IsEndOfSentence(adjustedDotIndex, currentText, out string? delimitingChars))
                                {
                                    var splitPosition = dotIndexes[dotCounter - 1] + 1;
                                    splitPosition -= alreadyTrimmed;
                                    var (before, after) = SH.GetPartsByLocationNoOutInt(currentText, splitPosition);
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

    public static List<int> SplitToIntList(string text, params string[] delimiters)
    {
        var parts = Split(text.RemoveInvisibleChars(), delimiters);
        var result = new List<int>(parts.Count);
        foreach (var item in parts)
            result.Add(int.Parse(item));
        return result;
    }

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
