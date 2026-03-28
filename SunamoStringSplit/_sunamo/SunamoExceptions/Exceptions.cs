namespace SunamoStringSplit._sunamo.SunamoExceptions;

/// <summary>
/// Provides exception message formatting utilities.
/// </summary>
internal sealed partial class Exceptions
{
    #region Other
    /// <summary>
    /// Checks and formats a prefix string for exception messages.
    /// </summary>
    /// <param name="before">The prefix text to check.</param>
    /// <returns>Empty string if null/whitespace, otherwise the prefix with colon separator.</returns>
    internal static string CheckBefore(string before)
    {
        return string.IsNullOrWhiteSpace(before) ? string.Empty : before + ": ";
    }

    /// <summary>
    /// Gets the type, method name, and stack trace of the place where an exception occurred.
    /// </summary>
    /// <param name="isFillAlsoFirstTwo">Whether to also fill the type and method name from the first non-ThrowEx frame.</param>
    /// <returns>A tuple containing the type name, method name, and formatted stack trace.</returns>
    internal static Tuple<string, string, string> PlaceOfException(bool isFillAlsoFirstTwo = true)
    {
        StackTrace stackTrace = new();
        var stackTraceText = stackTrace.ToString();
        var lines = stackTraceText.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
        lines.RemoveAt(0);
        string type = string.Empty;
        string methodName = string.Empty;
        for (var lineIndex = 0; lineIndex < lines.Count; lineIndex++)
        {
            var line = lines[lineIndex];
            if (isFillAlsoFirstTwo)
                if (!line.StartsWith("   at ThrowEx"))
                {
                    TypeAndMethodName(line, out type, out methodName);
                    isFillAlsoFirstTwo = false;
                }
            if (line.StartsWith("at System."))
            {
                lines.Add(string.Empty);
                lines.Add(string.Empty);
                break;
            }
        }
        return new Tuple<string, string, string>(type, methodName, string.Join(Environment.NewLine, lines));
    }

    /// <summary>
    /// Extracts the type and method name from a stack trace line.
    /// </summary>
    /// <param name="text">The stack trace line to parse.</param>
    /// <param name="type">The extracted type name.</param>
    /// <param name="methodName">The extracted method name.</param>
    internal static void TypeAndMethodName(string text, out string type, out string methodName)
    {
        var afterAt = text.Split("at ")[1].Trim();
        var fullMethodPath = afterAt.Split("(")[0];
        var parts = fullMethodPath.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        methodName = parts[^1];
        parts.RemoveAt(parts.Count - 1);
        type = string.Join(".", parts);
    }

    /// <summary>
    /// Gets the name of the calling method at the specified stack depth.
    /// </summary>
    /// <param name="depth">The stack frame depth to inspect.</param>
    /// <returns>The name of the method at the specified depth.</returns>
    internal static string CallingMethod(int depth = 1)
    {
        StackTrace stackTrace = new();
        var methodBase = stackTrace.GetFrame(depth)?.GetMethod();
        if (methodBase == null)
        {
            return "Method name cannot be get";
        }
        var methodName = methodBase.Name;
        return methodName;
    }
    #endregion

    #region OnlyReturnString
    /// <summary>
    /// Creates a custom exception message with an optional prefix.
    /// </summary>
    /// <param name="before">The prefix text.</param>
    /// <param name="message">The main error message.</param>
    /// <returns>The formatted exception message.</returns>
    internal static string? Custom(string before, string message)
    {
        return CheckBefore(before) + message;
    }

    /// <summary>
    /// Creates a "not implemented method" exception message with an optional prefix.
    /// </summary>
    /// <param name="before">The prefix text.</param>
    /// <returns>The formatted exception message.</returns>
    internal static string? NotImplementedMethod(string before)
    {
        return CheckBefore(before) + "Not implemented method.";
    }
    #endregion
}
