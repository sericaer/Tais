public class TaskDef
{
    public double energy;
}

public class Task
{
    public Effect energyEffect;

    public Task(TaskDef def)
    {
        energyEffect = new Effect()
        {
            effectType = EffectType.Energy,
            valueType = ValueType.Fixed,
            value = def.energy * -1
        };
    }
}
