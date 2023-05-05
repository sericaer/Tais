using DynamicData;
using DynamicData.Aggregation;
using System.ComponentModel;
using System;

public class Player : Entity
{
    public EValue energy { get;}

    public SourceList<Task> task = new SourceList<Task>();

    public Player()
    {
        energy = new EValue(15, EffectType.Energy, this);

        Subscribe(task.Connect().OnItemAdded(item =>
        {
            effectPool.Add(item.energyEffect);
        }), _=> { });

        Subscribe(task.Connect().OnItemRemoved(item =>
        {
            effectPool.Remove(item.energyEffect);
        }), _ => { });
    }
}

public class Task
{
    public Effect energyEffect;
}

public class Entity : ModelObject
{
    public SourceList<Effect> effectPool = new SourceList<Effect>();
}

public class EValue : ModelObject
{
    public double baseValue { get; private set; }
    public double currValue { get; }
    public EffectType obsEffectType { get; }

    public IObservableList<Effect> effects;

    public EValue(double baseValue, EffectType effectType, Entity owner) : base(owner)
    {
        this.baseValue = baseValue;
        this.currValue = baseValue;

        this.effects = owner.effectPool.Connect().Filter(x => x.type == effectType).AsObservableList();

        Subscribe(effects.Connect().Sum(x => x.value), sum => baseValue = baseValue * (1 + sum));
    }
}

public enum EffectType
{
    Energy
}

public class Effect
{
    public EffectType type;
    public double value;
}