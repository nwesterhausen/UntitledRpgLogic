using UntitledRpgLogic.Core.Classes;

namespace UntitledRpgLogic.UnitTests.Core;

[TestClass]
public class NameTests
{
	[TestMethod]
	public void Constructor_SingleArgument_SetsDefaultsAndGuessesPlural()
	{
		var name = new Name("Sword");

		Assert.AreEqual("Sword", name.Singular);
		Assert.AreEqual("Swords", name.Plural);
		Assert.AreEqual("Sword", name.Adjective);
	}

	[TestMethod]
	public void Constructor_ExplicitArguments_PreservesSuppliedValues()
	{
		var name = new Name("Wolf", "Wolves", "Lupine");

		Assert.AreEqual("Wolf", name.Singular);
		Assert.AreEqual("Wolves", name.Plural);
		Assert.AreEqual("Lupine", name.Adjective);
	}

	[TestMethod]
	public void Empty_ReturnsEmptyNameInstance()
	{
		var empty = Name.Empty;

		Assert.AreEqual(string.Empty, empty.Singular);
		Assert.AreEqual(string.Empty, empty.Plural);
		Assert.AreEqual(string.Empty, empty.Adjective);
	}

	[TestMethod]
	[DataRow("Sword", "Swords")]
	[DataRow("Box", "Boxes")]
	[DataRow("Bus", "Buses")]
	[DataRow("Buzz", "Buzzes")]
	[DataRow("Topaz", "Topazzes")]
	[DataRow("Match", "Matches")]
	[DataRow("Bush", "Bushes")]
	[DataRow("City", "Cities")]
	[DataRow("Day", "Days")]
	[DataRow("Key", "Keys")]
	[DataRow("Boy", "Boys")]
	[DataRow("Guy", "Guys")]
	[DataRow("", "")]
	public void BestGuessPlural_HandlesEnglishRules(string singular, string expectedPlural)
	{
		var actual = Name.BestGuessPlural(singular);

		Assert.AreEqual(expectedPlural, actual);
	}

	[TestMethod]
	public void GetName_ReturnsSingularWhenCountIsOne()
	{
		var name = new Name("Potion");

		var result = name.GetName(1);

		Assert.AreEqual("Potion", result);
	}

	[TestMethod]
	[DataRow(0)]
	[DataRow(2)]
	[DataRow(10)]
	public void GetName_ReturnsPluralWhenCountIsNotOne(int count)
	{
		var name = new Name("Potion");

		var result = name.GetName(count);

		Assert.AreEqual("Potions", result);
	}

	[TestMethod]
	public void Serialize_WhenSingularMatchesAdjective_UsesTwoPartFormat()
	{
		var name = new Name("Hero");

		var serialized = name.Serialize();

		StringAssert.StartsWith(serialized, "Hero", StringComparison.InvariantCulture);
		StringAssert.EndsWith(serialized, "Heroes", StringComparison.InvariantCulture);
	}

	[TestMethod]
	public void Serialize_WhenAdjectiveDiffers_UsesThreePartFormat()
	{
		var name = new Name("Gold", "Golds", "Golden");

		var serialized = name.Serialize();

		Assert.AreEqual("Gold;Golds;Golden", serialized);
	}

	[TestMethod]
	[DataRow("Hero;Heros", "Hero", "Heros", "Hero")]
	[DataRow("Wolf;Wolves;Lupine", "Wolf", "Wolves", "Lupine")]
	[DataRow("Sword", "Sword", "Swords", "Sword")]
	public void Deserialize_ValidString_ReconstructsName(string serialized, string expectedSingular, string expectedPlural, string expectedAdjective)
	{
		var name = Name.Deserialize(serialized);

		Assert.AreEqual(expectedSingular, name.Singular);
		Assert.AreEqual(expectedPlural, name.Plural);
		Assert.AreEqual(expectedAdjective, name.Adjective);
	}

	[TestMethod]
	[DataRow("Hero")]
	[DataRow("Wolf", "Wolves", "Lupine")]
	[DataRow("Iron", "Irons", "Iron")]
	public void SerializationRoundTrip_PreservesAllFields(string singular, string? plural = null, string? adjective = null)
	{
		var original = new Name(singular, plural, adjective);

		var serialized = original.Serialize();
		var reconstructed = Name.Deserialize(serialized);

		Assert.AreEqual(original.Singular, reconstructed.Singular);
		Assert.AreEqual(original.Plural, reconstructed.Plural);
		Assert.AreEqual(original.Adjective, reconstructed.Adjective);
	}

	[TestMethod]
	public void Deserialize_NullInput_ThrowsArgumentNullException()
	{
		Assert.Throws<ArgumentNullException>(() => Name.Deserialize(null!));
	}
}
