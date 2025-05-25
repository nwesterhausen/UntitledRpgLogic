namespace UntitledRpgLogic.Stat;

/// <summary>
/// A stat meant to represent something abstract that doesn't behave like a normal stat.
/// </summary>
/// <param name="name"></param>
/// <param name="maxValue"></param>
public class PseudoStat(string name, int maxValue = 100): StatBase(StatVariation.Pseudo,name, maxValue) {

}
