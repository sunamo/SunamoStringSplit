namespace SunamoStringSplit._sunamo.SunamoExceptions;

/// <summary>
/// Provides methods for throwing exceptions with detailed context information.
/// </summary>
internal partial class ThrowEx
{
    /// <summary>
    /// Throws a custom exception with the specified message.
    /// </summary>
    /// <param name="message">The primary error message.</param>
    /// <param name="isReallyThrowing">Whether to actually throw the exception or just return true.</param>
    /// <param name="secondMessage">An optional secondary message to append.</param>
    /// <returns>True if the exception would be thrown, false otherwise.</returns>
    internal static bool Custom(string message, bool isReallyThrowing = true, string secondMessage = "")
    {
        string joined = string.Join(" ", message, secondMessage);
        string? exception = Exceptions.Custom(FullNameOfExecutedCode(), joined);
        return ThrowIsNotNull(exception, isReallyThrowing);
    }

    /// <summary>
    /// Throws a "not implemented method" exception.
    /// </summary>
    /// <returns>True if the exception was thrown.</returns>
    internal static bool NotImplementedMethod() { return ThrowIsNotNull(Exceptions.NotImplementedMethod); }

    #region Other
    /// <summary>
    /// Gets the full name (type + method) of the currently executed code.
    /// </summary>
    /// <returns>The fully qualified name of the executing code.</returns>
    internal static string FullNameOfExecutedCode()
    {
        Tuple<string, string, string> placeOfException = Exceptions.PlaceOfException();
        string result = FullNameOfExecutedCode(placeOfException.Item1, placeOfException.Item2, true);
        return result;
    }

    private static string FullNameOfExecutedCode(object type, string methodName, bool isFromThrowEx = false)
    {
        if (methodName == null)
        {
            int depth = 2;
            if (isFromThrowEx)
            {
                depth++;
            }

            methodName = Exceptions.CallingMethod(depth);
        }
        string typeFullName;
        if (type is Type typeAsType)
        {
            typeFullName = typeAsType.FullName ?? "Type cannot be get via type is Type type2";
        }
        else if (type is MethodBase method)
        {
            typeFullName = method.ReflectedType?.FullName ?? "Type cannot be get via type is MethodBase method";
            methodName = method.Name;
        }
        else if (type is string)
        {
            typeFullName = type.ToString() ?? "Type cannot be get via type is string";
        }
        else
        {
            Type objectType = type.GetType();
            typeFullName = objectType.FullName ?? "Type cannot be get via type.GetType()";
        }
        return string.Concat(typeFullName, ".", methodName);
    }

    /// <summary>
    /// Throws an exception if the exception message is not null.
    /// </summary>
    /// <param name="exception">The exception message to check.</param>
    /// <param name="isReallyThrowing">Whether to actually throw or just return the result.</param>
    /// <returns>True if the exception message was not null.</returns>
    internal static bool ThrowIsNotNull(string? exception, bool isReallyThrowing = true)
    {
        if (exception != null)
        {
            Debugger.Break();
            if (isReallyThrowing)
            {
                throw new Exception(exception);
            }
            return true;
        }
        return false;
    }

    #region For avoid FullNameOfExecutedCode
    /// <summary>
    /// Throws an exception if the function returns a non-null exception message.
    /// </summary>
    /// <param name="function">The function that generates the exception message.</param>
    /// <returns>True if the exception was thrown.</returns>
    internal static bool ThrowIsNotNull(Func<string, string?> function)
    {
        string? exception = function(FullNameOfExecutedCode());
        return ThrowIsNotNull(exception);
    }
    #endregion
    #endregion
}
