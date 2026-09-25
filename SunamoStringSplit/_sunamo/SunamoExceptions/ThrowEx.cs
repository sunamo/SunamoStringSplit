namespace SunamoStringSplit._sunamo.SunamoExceptions;

internal partial class ThrowEx
{
    internal static bool Custom(string message, bool isReallyThrowing = true, string secondMessage = "")
    {
        string joined = string.Join(" ", message, secondMessage);
        string? exception = Exceptions.Custom(FullNameOfExecutedCode(), joined);
        return ThrowIsNotNull(exception, isReallyThrowing);
    }

    internal static bool NotImplementedMethod() { return ThrowIsNotNull(Exceptions.NotImplementedMethod); }

    #region Other
    internal static string FullNameOfExecutedCode()
    {
        Tuple<string, string, string> placeOfException = Exceptions.PlaceOfException();
        string result = FullNameOfExecutedCode(placeOfException.Item1, placeOfException.Item2, true);
        return result;
    }

    private static string FullNameOfExecutedCode(object type, string methodName, bool isFromThrowEx = false)
    {
        if (methodName is null)
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

    internal static bool ThrowIsNotNull(string? exception, bool isReallyThrowing = true)
    {
        if (exception is not null)
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
    internal static bool ThrowIsNotNull(Func<string, string?> function)
    {
        string? exception = function(FullNameOfExecutedCode());
        return ThrowIsNotNull(exception);
    }
    #endregion
    #endregion
}
