using DynamicData;

public class Entity : ModelObject
{
    public SourceList<Effect> effectPool = new SourceList<Effect>();

    public Entity(ModelObject parent) : base(parent)
    {

    }
}
