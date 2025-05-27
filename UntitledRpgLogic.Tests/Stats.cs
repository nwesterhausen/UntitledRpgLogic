using Microsoft.Extensions.Logging;
using Moq;
using UntitledRpgLogic.Stat;

namespace UntitledRpgLogic.Tests;

// Minimal concrete implementation for testing
public class TestStat(string name, int max = 100, int value = 0, int min = 0, ILogger<StatBase>? logger = null) :
    StatBase(StatVariation.Pseudo, name, max, value, min, logger);

[TestClass]
public class StatBaseTests
{
    [TestMethod]
    public void Constructor_SetsProperties()
    {
        var stat = new TestStat("Health", 200, 50, 10);
        Assert.AreEqual("Health", stat.Name);
        Assert.AreEqual(200, stat.MaxValue);
        Assert.AreEqual(10, stat.MinValue);
        Assert.AreEqual(50, stat.Value);
        Assert.AreEqual(StatVariation.Pseudo, stat.Variation);
    }

    [TestMethod]
    public void Value_Set_ClampsToMaxAndMin()
    {
        var stat = new TestStat("Mana", 100, 50, 10);
        stat.AddPoints(100);
        Assert.AreEqual(100, stat.Value);

        stat.SubtractPoints(100);
        Assert.AreEqual(10, stat.Value);
    }

    [TestMethod]
    public void AddPoints_IncreasesValue()
    {
        var stat = new TestStat("Stamina", 100, 10);
        Assert.AreEqual(10, stat.Value);
        stat.AddPoints(5);
        Assert.AreEqual(15, stat.Value);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void AddPoints_Negative_Throws()
    {
        var stat = new TestStat("Stamina", 100, 10);
        stat.AddPoints(-1);
    }

    [TestMethod]
    public void SubtractPoints_DecreasesValue()
    {
        var stat = new TestStat("Stamina", 100, 10);
        stat.SubtractPoints(5);
        Assert.AreEqual(5, stat.Value);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void SubtractPoints_Negative_Throws()
    {
        var stat = new TestStat("Stamina", 100, 10);
        stat.SubtractPoints(-1);
    }

    [TestMethod]
    public void ValueChanged_Event_Raised()
    {
        var stat = new TestStat("Agility", 100, 10);
        var eventRaised = false;
        stat.ValueChanged += (s, e) =>
        {
            eventRaised = true;
            Assert.AreEqual(10, e.OldValue);
            Assert.AreEqual(20, e.NewValue);
        };
        stat.AddPoints(10);
        Assert.IsTrue(eventRaised);
    }

    [TestMethod]
    public void CompareTo_Int_Works()
    {
        var stat = new TestStat("Luck", 100, 10);
        Assert.AreEqual(0, stat.CompareTo(10));
        Assert.AreEqual(-1, stat.CompareTo(20));
        Assert.AreEqual(1, stat.CompareTo(5));
    }

    [TestMethod]
    public void CompareTo_StatBase_Works()
    {
        var stat1 = new TestStat("Strength", 100, 10);
        var stat2 = new TestStat("Strength", 100, 10);
        var stat3 = new TestStat("Strength", 100, 20);
        var stat4 = new TestStat("Dexterity", 100, 10);

        Assert.AreEqual(0, stat1.CompareTo(stat2));
        Assert.AreNotEqual(0, stat1.CompareTo(stat3));
        Assert.AreNotEqual(0, stat1.CompareTo(stat4));
    }

    [TestMethod]
    public void Equality_Operators_Work()
    {
        var stat1 = new TestStat("Intelligence", 100, 10);
        var stat2 = new TestStat("Intelligence", 100, 20);
        var stat3 = new TestStat("Intelligence", 100, 30, 10);

        Assert.AreEqual(stat1, stat2);
        Assert.AreNotEqual(stat1, stat3);
        Assert.AreNotEqual(stat2, stat3);
    }

    [TestMethod]
    public void AddPoints_LogsStatChanged()
    {
        var loggerMock = new Mock<ILogger<StatBase>>();
        loggerMock.Setup(x => x.IsEnabled(LogLevel.Debug)).Returns(true);

        var stat = new TestStat("Test", 100, 10, 0, loggerMock.Object);

        stat.AddPoints(5);

        loggerMock.Verify(
            x => x.Log(
                LogLevel.Debug,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("+5 Test")),
                null,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.AtLeastOnce);
    }

    [TestMethod]
    public void ToString_And_ExplicitStringOperator_Work()
    {
        var stat = new TestStat("Wisdom", 100, 50);
        var str1 = stat.ToString();
        var str2 = (string)stat;
        Assert.IsTrue(str1.Contains("Wisdom"));
        Assert.AreEqual(str1, str2);
    }

    [TestMethod]
    public void ExplicitIntOperator_ReturnsValue()
    {
        var stat = new TestStat("Charisma", 100, 42);
        var value = (int)stat;
        Assert.AreEqual(42, value);
    }

    [TestMethod]
    public void Equality_Considers_Name_Max_Min_Variation()
    {
        var stat1 = new TestStat("Endurance", 100, 10);
        var stat2 = new TestStat("Endurance", 100, 20);
        var stat3 = new TestStat("Endurance", 100, 10, 5);
        var stat4 = new TestStat("Endurance", 100, 10);

        Assert.AreEqual(stat1, stat2); // Value ignored in equality
        Assert.AreNotEqual(stat1, stat3); // MinValue differs
        Assert.AreEqual(stat1, stat4); // Logger ignored
    }

    [TestMethod]
    public void GetHashCode_Consistent_For_EqualStats()
    {
        var stat1 = new TestStat("Luck", 100, 10);
        var stat2 = new TestStat("Luck", 100, 20);
        Assert.AreEqual(stat1.GetHashCode(), stat2.GetHashCode());
    }

    [TestMethod]
    public void Operators_Work_With_Null()
    {
        TestStat? stat = null;
        var stat2 = new TestStat("Dexterity", 100, 10);
        Assert.IsTrue(stat == null);
        Assert.IsTrue(null == stat);
        Assert.IsTrue(stat != stat2);
        Assert.IsTrue(stat2 != null);
    }

    [TestMethod]
    public void CompareTo_Handles_Null_And_Ordering()
    {
        var stat1 = new TestStat("A", 100, 10);
        var stat2 = new TestStat("B", 100, 10);
        Assert.IsTrue(stat1.CompareTo(stat2) < 0);
        Assert.IsTrue(stat2.CompareTo(stat1) > 0);
        Assert.AreEqual(0, stat1.CompareTo(stat1));
        Assert.IsTrue(stat1.CompareTo(null) > 0);
    }

    [TestMethod]
    public void Value_Set_To_Same_Does_Not_Raise_Event()
    {
        var stat = new TestStat("Spirit", 100, 10);
        var eventRaised = false;
        stat.ValueChanged += (s, e) => eventRaised = true;
        stat.ForceValue(10);
        Assert.IsFalse(eventRaised);
    }

    [TestMethod]
    public void Value_Set_Clamps_To_Min_And_Max()
    {
        var stat = new TestStat("Vitality", 100, 50, 10);
        stat.ForceValue(200);
        Assert.AreEqual(100, stat.Value);
        stat.ForceValue(-50);
        Assert.AreEqual(10, stat.Value);
    }
}