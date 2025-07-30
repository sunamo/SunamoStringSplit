namespace SunamoStringSplit.Tests;

public class SHSplitTests
{
    [Fact]
    public void SplitByWhiteSpacesTest()
    {
        var d = @"SrealityUri		https://www.sreality.cz//detail/pronajem/dum/rodinny/bohdanec-replice-/960152140	https://www.sreality.cz//detail/pronajem/dum/rodinny/beloky-beloky-/556712524	https://www.sreality.cz//detail/pronajem/dum/rodinny/kacice-kacice-masarykova/3465400908	https://www.sreality.cz//detail/pronajem/dum/rodinny/cesky-brod-cesky-brod-jatecka/3015361100	https://www.sreality.cz//detail/pronajem/dum/rodinny/melnicke-vtelno-vysoka-liben-/316199500	https://www.sreality.cz//detail/pronajem/dum/rodinny/otvovice-otvovice-/2051330636	https://www.sreality.cz//detail/pronajem/dum/rodinny/bojanovice-bojanovice-/20763212	https://www.sreality.cz//detail/pronajem/dum/chata/postupice-postupice-/3613577804	https://www.sreality.cz//detail/pronajem/dum/chata/hvozdany-vacikov-/2525753932	https://www.sreality.cz//detail/pronajem/dum/rodinny/bystrice-zahorany-/1500566860	https://www.sreality.cz//detail/pronajem/dum/rodinny/skvorec-trebohostice-/405869644													";

        var d2 = SHSplit.SplitByWhiteSpaces(d);
    }

    [Fact]
    public void SplitTest()
    {
        var ch = " "[0];
        var actual = SHSplit.Split(" 63�m� ", "�");
    }
}