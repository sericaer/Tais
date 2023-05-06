using DynamicData;
using DynamicData.Aggregation;
using System.Linq;

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
