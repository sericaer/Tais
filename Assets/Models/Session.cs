using DynamicData.Binding;

public class Session : ModelObject
{
    public bool isRunning { get; set; } = true;

    public int popNum { get; set; }

    public Date date { get; } = new Date();

    public Session()
    {
        Subcribe(date.WhenValueChanged(x => x.month), _ => messageBus.Publish(new MESSAGE_MONTH_INC(date.year, date.month)));
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
