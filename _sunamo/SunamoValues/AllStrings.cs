namespace SunamoStringSplit;


internal class AllStrings
{
    internal const string dot = ".";
    internal const string comma = ",";
    internal static List<string> whiteSpacesChars = null;
    /// <summary>
    ///     space wrapped equal sign
    /// </summary>
    internal const string swes = " = ";
    /// <summary>
    ///     space wrapped dash
    /// </summary>
    internal const string swda = " - ";
    internal const string dash = "-";
    /// <summary>
    ///     Question mark
    /// </summary>
    internal const string q = "?";
    /// <summary>
    ///     double dots
    /// </summary>
    internal const string dd = "..";
    /// <summary>
    ///     double dots slash
    /// </summary>
    internal const string dds = "../";
    internal const string ds = "./";
    internal const string dotSpace = ". ";
    internal const string slashAsterisk = "/*";
    internal const string asteriskSlash = "*/";
    internal const string apostrophe = "'";
    internal const string us = "_";
    internal const string space = " ";
    internal const string emDash = "�";
    /// <summary>
    ///     ORDINAL BRACKET
    /// </summary>
    internal const string lb = "(";
    internal const string rb = ")";
    internal const string tab = "\t";
    internal const string nl = "\n";
    internal const string cr = "\r";
    internal const string bs = @"\";
    internal const string slash = "/";
    internal const string asterisk = "*";
    /// <summary>
    ///     semicolon
    /// </summary>
    internal const string sc = ";";
    /// <summary>
    ///     should be quot
    /// </summary>
    internal const string qm = "\"";
    internal const string doubleSpace = "  ";
    internal const string bs2 = "\b";
    internal const string lq = "�";
    internal const string rq = "�";
    internal const string la = "�";
    internal const string ra = "�";
    internal const string st = "\0";
    internal const string euro = "�";
    /// <summary>
    ///     " - "
    /// </summary>
    internal static string swd = " - ";
    /// <summary>
    ///     comma space
    /// </summary>
    internal static string cs = ", ";
    /// <summary>
    ///     colon space
    /// </summary>
    internal static string cs2 = ": ";
    internal static string doubleSpace32160 = space + space160;
    internal static string doubleSpace16032 = space160 + space;
    internal static string space160 = AllChars.space160.ToString();
    internal static string doubleBs = bs + bs;
    internal static string DoubleSpace32160
    {
        get => doubleSpace32160;
        set => doubleSpace32160 = value;
    }
    ///// <summary>
    ///// space wrapped dash
    ///// </summary>
    //internal const string swda = " - ";
    //internal const string lowbar = "_";
    //internal const string colon = ":";
    //internal const string dash = "-";
    //internal const string space = " ";
    //internal const string bs = "\\";
    //internal static string comma = ",";
    //internal static string sc = ";";
    //internal const string lcub = "{";
    //internal const string rcub = "}";
    //internal const string lt = "<";
    //internal const string dot = ".";
    #region Generated with SunamoFramework.HtmlEntitiesForNonDigitsOrLetterChars
    internal const string period = ".";
    internal const string colon = ":";
    internal const string excl = "!";
    internal const string apos = "'";
    internal const string quest = "?";
    internal const string rpar = ")";
    internal const string lpar = "(";
    internal const string sol = "/";
    internal const string lowbar = "_";
    internal const string lt = "<";
    internal const string equals = "=";
    internal const string gt = ">";
    internal const string amp = "&";
    internal const string lcub = "{";
    internal const string rcub = "}";
    internal const string lsqb = "[";
    internal const string verbar = "|";
    internal const string semi = ";";
    internal const string commat = "@";
    internal const string ast = "*";
    internal const string plus = "+";
    internal const string rsqb = "]";
    internal const string num = "#";
    internal const string percnt = "%";
    internal const string dollar = "$";
    internal const string Hat = "^";
    internal const string ndash = "�";
    internal const string copy = "�";
    #endregion
}