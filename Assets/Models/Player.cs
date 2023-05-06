using DynamicData;
using System.ComponentModel;

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
