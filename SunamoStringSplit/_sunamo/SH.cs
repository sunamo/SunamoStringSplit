namespace SunamoStringSplit._sunamo;

internal class SH
{
    internal static int OccurencesOfStringIn(string source, string searchText)
    {
        return source.Split(new[] { searchText }, StringSplitOptions.None).Length - 1;
    }

    internal static (string, string) GetPartsByLocationNoOutInt(string text, int position)
    {
        GetPartsByLocation(out string before, out string after, text, position);
        return (before, after);
    }

    internal static void GetPartsByLocation(out string before, out string after, string text, int position)
    {
        if (position == -1)
        {
            before = text;
            after = "";
        }
        else
        {
            before = text.Substring(0, position);
            if (text.Length > position + 1)
                after = text.Substring(position + 1);
            else
                after = string.Empty;
        }
    }

    internal static List<int> ReturnOccurencesOfString(string text, string searchText)
    {
        var results = new List<int>();
        for (var index = 0; index < text.Length - searchText.Length + 1; index++)
        {
            var substring = text.Substring(index, searchText.Length);

            if (substring == searchText)
                results.Add(index);
        }

        return results;
    }
}
