using UntitledRpgLogic.Classes;

namespace UntitledRpgLogic.Tests.Classes;

[TestClass]
public class PluralNameTests
{
    [TestMethod]
    public void BestGuessPlural_NullOrEmpty_ReturnsEmpty()
    {
        Assert.AreEqual(string.Empty, PluralName.BestGuessPlural(null!));
        Assert.AreEqual(string.Empty, PluralName.BestGuessPlural(string.Empty));
    }

    [TestMethod]
    public void BestGuessPlural_EndsInY_ConsonantBeforeY_ReturnsIes()
    {
        Assert.AreEqual("babies", PluralName.BestGuessPlural("baby"));
        Assert.AreEqual("parties", PluralName.BestGuessPlural("party"));
        Assert.AreEqual("ladies", PluralName.BestGuessPlural("lady"));
    }

    [TestMethod]
    public void BestGuessPlural_EndsInY_VowelBeforeY_ReturnsYs()
    {
        Assert.AreEqual("boys", PluralName.BestGuessPlural("boy"));
        Assert.AreEqual("keys", PluralName.BestGuessPlural("key"));
        Assert.AreEqual("plays", PluralName.BestGuessPlural("play"));
        Assert.AreEqual("guys", PluralName.BestGuessPlural("guy"));
    }

    [TestMethod]
    public void BestGuessPlural_EndsInS_ReturnsEs()
    {
        Assert.AreEqual("buses", PluralName.BestGuessPlural("bus"));
        Assert.AreEqual("kisses", PluralName.BestGuessPlural("kiss"));
    }

    [TestMethod]
    public void BestGuessPlural_EndsInX_ReturnsEs()
    {
        Assert.AreEqual("boxes", PluralName.BestGuessPlural("box"));
        Assert.AreEqual("foxes", PluralName.BestGuessPlural("fox"));
    }

    [TestMethod]
    public void BestGuessPlural_EndsInZ_ReturnsEs()
    {
        Assert.AreEqual("quizzes", PluralName.BestGuessPlural("quiz"));
        Assert.AreEqual("buzzes", PluralName.BestGuessPlural("buzz"));
    }

    [TestMethod]
    public void BestGuessPlural_EndsInCh_ReturnsEs()
    {
        Assert.AreEqual("churches", PluralName.BestGuessPlural("church"));
        Assert.AreEqual("matches", PluralName.BestGuessPlural("match"));
    }

    [TestMethod]
    public void BestGuessPlural_EndsInSh_ReturnsEs()
    {
        Assert.AreEqual("bushes", PluralName.BestGuessPlural("bush"));
        Assert.AreEqual("wishes", PluralName.BestGuessPlural("wish"));
    }

    [TestMethod]
    public void BestGuessPlural_RegularNoun_ReturnsS()
    {
        Assert.AreEqual("cats", PluralName.BestGuessPlural("cat"));
        Assert.AreEqual("dogs", PluralName.BestGuessPlural("dog"));
        Assert.AreEqual("houses", PluralName.BestGuessPlural("house"));
    }
}
