public enum EffectType
{
    Energy
}

public enum ValueType
{
    Percent,
    Fixed
}

public class Effect
{
    public EffectType effectType;
    public ValueType valueType;
    public double value;
}