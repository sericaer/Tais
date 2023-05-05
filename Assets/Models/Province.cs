using DynamicData;

public class Province : ModelObject
{
    public string name { get; set; }

    public int popNum { get; private set; }

    public SourceList<Pop> pops { get; } = new SourceList<Pop>();

    public Province(ProvinceInit init, ModelObject parent) : base(parent)
    {
        this.name = init.name;
        foreach(var popInit in init.popInit)
        {
            pops.Add(new Pop(popInit, this));
        }

        Subscribe(pops.Sum(x => x.popNum), num => popNum = num);
    }
}
