using Microsoft.Extensions.Logging;
using Moq;
using UntitledRpgLogic.BaseImplementations;
using UntitledRpgLogic.Enums;
using UntitledRpgLogic.Options;

namespace UntitledRpgLogic.Tests;

// Minimal concrete implementation for testing
public class TestStat(string name, int max = 100, int value = 0, int min = 0, ILogger<StatBase>? logger = null) :
    StatBase(new StatOptions
    {
        Variation = StatVariation.Pseudo,
        Name = name,
        MaxValue = max,
        Value = value,
        MinValue = min,
        Logger = logger
    });

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
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("+5 Test")),
                null,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.AtLeastOnce);
    }

    [TestMethod]
    public void ToString_Works()
    {
        var stat = new TestStat("Wisdom", 100, 50);
        var str1 = stat.ToString();
        Assert.IsTrue(str1.Contains("Wisdom"));
    }
}