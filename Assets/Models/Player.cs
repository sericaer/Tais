using DynamicData;
using DynamicData.Aggregation;
using System.ComponentModel;
using System;
using System.Linq;

public class Player : Entity
{
    public EValue energy { get;}

    public SourceList<Task> tasks { get; } = new SourceList<Task>();

    public Player(ModelObject parent) : base(parent)
    {
        energy = new EValue(100, EffectType.Energy, this);

        Subscribe(tasks.Connect().Transform(x => x.energyEffect), changedSet => effectPool.OnChange(changedSet));
    }

    [OnMessage]
    void OnMESSAGE_ADD_TASK(MESSAGE_ADD_TASK msg)
    {
        tasks.Add(new Task(msg.def));
    }
}


public static class SourceListExtension
{
    public static void OnChange<T>(this SourceList<T> sourceList, IChangeSet<T> changeSet)
    {
        foreach (var changed in changeSet)
        {
            switch(changed.Reason)
            {
                case ListChangeReason.Add:
                    sourceList.Add(changed.Item.Current);
                    break;
                case ListChangeReason.Remove:
                    sourceList.Remove(changed.Item.Current);
                    break;
                case ListChangeReason.AddRange:
                    sourceList.AddRange(changed.Range);
                    break;
                case ListChangeReason.RemoveRange:
                    sourceList.RemoveMany(changed.Range);
                    break;
                default:
                    throw new Exception();
            }
        }
    }
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

public class TaskDef
{
    public double energy;
}

public class Entity : ModelObject
{
    public SourceList<Effect> effectPool = new SourceList<Effect>();

    public Entity(ModelObject parent) : base(parent)
    {

    }
}

public class EValue : ModelObject
{
    public double baseValue { get; private set; }
    public double currValue { get; private set; }
    public EffectType obsEffectType { get; }

    public IObservableList<Effect> effects;

    public EValue(double baseValue, EffectType effectType, Entity owner) : base(owner)
    {
        this.baseValue = baseValue;
        this.currValue = baseValue;

        this.effects = owner.effectPool.Connect().Filter(x => x.effectType == effectType).AsObservableList();

        Subscribe(effects.Connect(), _=> 
        {
            var percent = effects.Items.Where(x => x.valueType == ValueType.Percent).Sum(x => x.value);
            var Fixed = effects.Items.Where(x => x.valueType == ValueType.Fixed).Sum(x => x.value);

            currValue = baseValue * (1 + percent) + Fixed;
        });
    }
}

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