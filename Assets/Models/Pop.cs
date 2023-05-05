public class Pop : ModelObject
{
    public int popNum { get; set; }

    public Pop(PopInit popInit, ModelObject parent) : base(parent)
    {
        popNum = popInit.num;
    }

    [OnMessage]
    public void OnMESSAGE_DAY_INC(MESSAGE_DAY_INC msg)
    {
        popNum++;
    }
}