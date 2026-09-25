namespace SunamoStringSplit._sunamo.SunamoStringGetLines;

internal class SHGetLines
{
    internal static List<string> GetLines(string text)
    {
        var parts = text.Split(new[] { "\r\n", "\n\r" }, StringSplitOptions.None).ToList();
        SplitByUnixNewline(parts);
        return parts;
    }

    private static void SplitByUnixNewline(List<string> list)
    {
        SplitBy(list, "\r");
        SplitBy(list, "\n");
    }

    private static void SplitBy(List<string> list, string delimiter)
    {
        for (var i = list.Count - 1; i >= 0; i--)
        {
            if (delimiter == "\r")
            {
                var carriageReturnNewline = list[i].Split(new[] { "\r\n" }, StringSplitOptions.None);
                var newlineCarriageReturn = list[i].Split(new[] { "\n\r" }, StringSplitOptions.None);

                if (carriageReturnNewline.Length > 1)
                    ThrowEx.Custom("cannot contain any \\r\\n, pass already split by this pattern");
                else if (newlineCarriageReturn.Length > 1) ThrowEx.Custom("cannot contain any \\n\\r, pass already split by this pattern");
            }

            var parts = list[i].Split(new[] { delimiter }, StringSplitOptions.None);

            if (parts.Length > 1) InsertOnIndex(list, parts.ToList(), i);
        }
    }

    private static void InsertOnIndex(List<string> list, List<string> elementsToInsert, int index)
    {
        elementsToInsert.Reverse();

        list.RemoveAt(index);

        foreach (var item in elementsToInsert) list.Insert(index, item);
    }
}
