using DynamicData;
using DynamicData.Binding;

public class Session : ModelObject
{
    public bool isRunning { get; set; } = false;

    public int popNum { get; set; }

    public SourceList<Province> provinces { get; } = new SourceList<Province>();

    public Date date { get; } = new Date();

    public Player player { get; } = new Player();

    public Session()
    {
        provinces.Add(new Province(new ProvinceInit() 
        { 
            name = "P1", 
            popInit = new PopInit[] 
            {
                new PopInit()
                {
                    num = 1000,
                }
            }  
        }, 
        this));

        provinces.Add(new Province(new ProvinceInit()
        {
            name = "P2",
            popInit = new PopInit[]
            {
                new PopInit()
                {
                    num = 1000,
                },
                new PopInit()
                {
                    num = 2000,
                }
            }
        },
        this));

        Subscribe(date.WhenValueChanged(x => x.month, false), _ => messageBus.Publish(new MESSAGE_MONTH_INC(date.year, date.month)));
        Subscribe(date.WhenValueChanged(x => x.day, false), _ => messageBus.Publish(new MESSAGE_DAY_INC(date.year, date.month, date.day)));
    }

    public void Run()
    {
        if(isRunning)
        {
            date.DaysInc();
        }
    }

    [OnMessage]
    public void OnMESSAGE_MONTH_INC(MESSAGE_MONTH_INC msg)
    {
        isRunning = false;
    }
}
